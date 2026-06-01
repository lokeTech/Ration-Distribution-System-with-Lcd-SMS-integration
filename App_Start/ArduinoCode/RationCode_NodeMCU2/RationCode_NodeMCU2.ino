#include <ESP8266WiFi.h>
#include <ESP8266HTTPClient.h>
#include <HX711.h>
#include <Wire.h>
#include <LiquidCrystal_I2C.h>

// ------------------ Load Cell ------------------
#define DOUT D6  // GPIO12
#define CLK  D5  // GPIO14
HX711 scale;

// ------------------ Motor Pins (SAFE) ------------------
// Avoid GPIO0 and GPIO2 (boot pins)
#define MOTOR_IN1 D3  
#define MOTOR_IN2 D4  

// ------------------ LCD ------------------
LiquidCrystal_I2C lcd(0x27, 16, 2);  // Adjust I2C address if needed

// ------------------ WiFi ------------------
const char* ssid = "WIWifi";
const char* password = "1234567890";

// ------------------ Server URLs ------------------
const String getUrl = "http://192.168.137.1/sm/getdata";
const String putUrl = "http://192.168.137.1/sm/putdata?q=done";

void setup() {
  Serial.begin(115200);
  Serial.println();
  Serial.println("===== ESP8266 Booting... =====");

  // LCD
  lcd.init();
  lcd.backlight();
  lcd.clear();
  lcd.setCursor(0, 0);
  lcd.print("Booting...");
  delay(1000);

  // WiFi connect
  WiFi.begin(ssid, password);
  lcd.clear();
  lcd.setCursor(0, 0);
  lcd.print("Connecting WiFi");
  Serial.print("Connecting to WiFi");

  int retry = 0;
  while (WiFi.status() != WL_CONNECTED && retry < 20) {
    delay(500);
    Serial.print(".");
    retry++;
  }

  if (WiFi.status() == WL_CONNECTED) {
    Serial.println("\nWiFi Connected!");
    Serial.print("IP Address: ");
    Serial.println(WiFi.localIP());

    lcd.clear();
    lcd.setCursor(0, 0);
    lcd.print("WiFi Connected");
    lcd.setCursor(0, 1);
    lcd.print(WiFi.localIP().toString());
  } else {
    Serial.println("\nWiFi Connect Failed!");
    lcd.clear();
    lcd.print("WiFi Failed!");
  }

  // Load Cell Setup
  scale.begin(DOUT, CLK);
  scale.set_scale();  // Needs calibration
  scale.tare();
  Serial.println("HX711 Initialized");

  // Motor Pins
  pinMode(MOTOR_IN1, OUTPUT);
  pinMode(MOTOR_IN2, OUTPUT);
  digitalWrite(MOTOR_IN1, LOW);
  digitalWrite(MOTOR_IN2, LOW);
  Serial.println("Motor Pins Ready");

  delay(2000);
  Serial.println("===== Setup Complete =====");
}

void operateMotor() {
  Serial.println("Motor Reverse");
  digitalWrite(MOTOR_IN1, HIGH);
  digitalWrite(MOTOR_IN2, LOW);
  delay(5000);

  Serial.println("Motor Forward");
  digitalWrite(MOTOR_IN1, LOW);
  digitalWrite(MOTOR_IN2, HIGH);
  delay(5000);

  Serial.println("Motor Stop");
  digitalWrite(MOTOR_IN1, LOW);
  digitalWrite(MOTOR_IN2, LOW);
}

void loop() {
    if (WiFi.status() == WL_CONNECTED) {
    HTTPClient http;
    WiFiClient client;

    Serial.println("\n--- Sending GET Request ---");
    http.begin(client, getUrl);
    int httpCode = http.GET();

    if (httpCode == 200) {
      String payload = http.getString();
      Serial.println("Server Response: " + payload);

      lcd.clear();
      lcd.setCursor(0, 0);
      lcd.print("Resp:");
      lcd.setCursor(0, 1);
      lcd.print(payload.substring(0, 16));

      // Extract quota (basic)
      int quotaStart = payload.indexOf(":") + 1;
      int quotaEnd = payload.indexOf("}");
      float quota = payload.substring(quotaStart, quotaEnd).toFloat();

      Serial.print("Quota Received: ");
      Serial.println(quota);

      if (quota <= 0.0) {
        Serial.println("No quota available.");
        lcd.clear();
        lcd.setCursor(0, 0);
        lcd.print("Waiting for user");
        lcd.setCursor(0, 1);
        lcd.print("Quantity: 0.00kg");
        http.end();
        delay(5000);
        return;
      }

      lcd.clear();
      lcd.setCursor(0, 0);
      lcd.print("Quantity: ");
      lcd.print(quota, 2);
      lcd.print("kg");

      if (scale.is_ready()) {
        scale.tare();
        delay(500);
        operateMotor();
        float weight = scale.get_units(1);
        weight=quota;
        Serial.print("Weight Read: ");
        Serial.println(weight);
      } else {
        Serial.println("Loadcell not ready!");
        lcd.setCursor(0, 1);
        lcd.print("Loadcell error!");
        operateMotor();
      }

      http.end();
      delay(300);

      // PUT Request
      Serial.println("--- Sending PUT (done) ---");
      http.begin(client, putUrl);
      int putCode = http.GET();
      if (putCode > 0) {
        Serial.println("PUT Success");
        lcd.clear();
        lcd.setCursor(0, 0);
        lcd.print("Dispensed: ");
        lcd.print(quota, 2);
        lcd.setCursor(0, 1);
        lcd.print("Done!");
      } else {
        Serial.println("PUT failed");
        lcd.setCursor(0, 1);
        lcd.print("PUT failed");
      }
      http.end();
    } else {
      Serial.print("GET failed, code: ");
      Serial.println(httpCode);
      lcd.clear();
      lcd.print("GET Failed!");
    }

    delay(10000);
  } else {
    Serial.println("WiFi Disconnected!");
    lcd.clear();
    lcd.setCursor(0, 0);
    lcd.print("Waiting WiFi...");
    WiFi.reconnect();
    delay(5000);
  }
}