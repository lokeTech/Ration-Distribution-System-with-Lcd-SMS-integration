#include <ESP8266WiFi.h>
#include <ESP8266HTTPClient.h>
#include <HX711.h>
#include <Wire.h>
#include <LiquidCrystal_I2C.h>

// Load cell
#define DOUT D6  // GPIO12
#define CLK D5   // GPIO14
HX711 scale;

// Motor
#define MOTOR_IN1 D3  // GPIO0
#define MOTOR_IN2 D4  // GPIO2

// LCD
LiquidCrystal_I2C lcd(0x27, 16, 2);  // Adjust address if needed

// WiFi credentials
const char* ssid = "WIWifi";
const char* password = "1234567890";

// Server URLs
const String getUrl = "http://192.168.137.1/sm/getdata";
const String putUrl = "http://192.168.137.1/sm/putdata?q=done";

void setup() {
  Serial.begin(115200);
  Serial.println("Booting...");
  WiFi.begin(ssid, password);

  // LCD
  lcd.init();
  lcd.backlight();
  lcd.setCursor(0, 0);
  lcd.print("Connecting WiFi");

  while (WiFi.status() != WL_CONNECTED) {
    delay(500);
    Serial.print(".");
  }

  lcd.clear();
  lcd.print("WiFi Connected");

  // Load cell
  scale.begin(DOUT, CLK);
  scale.set_scale();  // Needs calibration!
  scale.tare();       // Reset scale to 0

  // Motor pins
  pinMode(MOTOR_IN1, OUTPUT);
  pinMode(MOTOR_IN2, OUTPUT);
  digitalWrite(MOTOR_IN1, LOW);
  digitalWrite(MOTOR_IN2, LOW);

  delay(2000);
}

void operateMotor() {
  // Reverse
  digitalWrite(MOTOR_IN1, HIGH);
  digitalWrite(MOTOR_IN2, LOW);
  delay(3000);

  // Forward
  digitalWrite(MOTOR_IN1, LOW);
  digitalWrite(MOTOR_IN2, HIGH);
  delay(3000);

  // Stop
  digitalWrite(MOTOR_IN1, LOW);
  digitalWrite(MOTOR_IN2, LOW);
}

void loop() {
  if (WiFi.status() == WL_CONNECTED) {
    HTTPClient http;
    WiFiClient client;

    // --- GET quota ---
    http.begin(client, getUrl);
    int httpCode = http.GET();

    if (httpCode == 200) {
      String payload = http.getString();
      Serial.println("Server response: " + payload);

      // Display full response on LCD for debug
      lcd.clear();
      lcd.setCursor(0, 0);
      lcd.print("Resp:");
      lcd.setCursor(0, 1);
      lcd.print(payload.substring(0, 16));  // show part of response

      // Extract quota from JSON (basic method)
      int quotaStart = payload.indexOf(":") + 1;
      int quotaEnd = payload.indexOf("}");
      float quota = payload.substring(quotaStart, quotaEnd).toFloat();

      Serial.print("Quota received: ");
      Serial.println(quota);

      if (quota <= 0.0) {
        Serial.println("No quota. Waiting...");
        lcd.clear();
        lcd.setCursor(0, 0);
        lcd.print("Waiting for user");
        lcd.setCursor(0, 1);
        lcd.print("Quota: 0 kg");
        http.end();
        delay(5000);  // Wait before retry
        return;
      }

      // Show valid quota
      lcd.clear();
      lcd.setCursor(0, 0);
      lcd.print("Quota: ");
      lcd.print(quota, 2);
      lcd.print("kg");

      // Try to read weight
      if (scale.is_ready()) {
        scale.tare();
        delay(500);
        float weight = scale.get_units(10);
        Serial.print("Weight read: ");
        Serial.println(weight);
      } else {
        lcd.setCursor(0, 1);
        lcd.print("Loadcell error!");
        Serial.println("Loadcell not ready");

        // Operate motor
        operateMotor();
      }

      http.end();
      delay(500);

      // --- PUT "done" ---
      http.begin(client, putUrl);
      int putCode = http.GET();  // Replace with POST if needed
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
      Serial.println("GET failed, code: " + String(httpCode));
      lcd.clear();
      lcd.setCursor(0, 0);
      lcd.print("GET Failed!");
    }

    delay(10000);  // Wait before next loop
  } else {
    Serial.println("No quota. Waiting.....");
    lcd.clear();
    lcd.setCursor(0, 0);
    lcd.print("Waiting for user");
    lcd.setCursor(0, 1);
    lcd.print("Quota1: 0 kg");
  }
}
