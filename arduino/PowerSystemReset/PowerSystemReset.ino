
#define enablePIN 3

void setup() {
  
pinMode(enablePIN, OUTPUT);
digitalWrite(enablePIN, HIGH);
}

void loop() {
 delay(15 * 60 * 1000);
 digitalWrite(enablePIN, LOW);
 delay(4 * 1000);
 digitalWrite(enablePIN, HIGH);
}
