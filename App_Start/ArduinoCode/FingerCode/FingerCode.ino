#include <Adafruit_Fingerprint.h>
#include <SoftwareSerial.h>
SoftwareSerial mySerial(2, 3); // RX, TX
Adafruit_Fingerprint finger = Adafruit_Fingerprint(&mySerial);
int x=0;
void setup() {
  Serial.begin(115200);
  while (!Serial); // Wait for serial monitor
  delay(100);
  
  //Serial.println("Initializing fingerprint sensor...");

  finger.begin(57600);
  if (finger.verifyPassword()) {
    //Serial.println("Sensor connected.");
  } else {
    //Serial.println("Sensor not found. Check wiring.");
    while (1);
  }
}

void loop() {
  // Try to get the image (i.e., detect finger)
  uint8_t p = finger.getImage();
  
  if (p == FINGERPRINT_NOFINGER) {
    //Serial.println("No finger detected.");
    x--;
  } else if (p == FINGERPRINT_OK) {
    //Serial.println("Finger detected!");
    x++;
  } else {
    //Serial.print("Sensor error: "); Serial.println(p);
  }
  if(x>=10)
  {
    Serial.println("11111");
    x=0;
  } 
  delay(500); 
}
