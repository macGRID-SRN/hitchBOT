const int waitTime = 1050000;
const int offTime = 2000;
const int outPin = 2;

void setup() {
  // put your setup code here, to run once:
  pinMode(outPin, OUTPUT);
}

void loop() {
  // put your main code here, to run repeatedly:
  digitalWrite(outPin, HIGH);   // sets the LED on
  delay(waitTime);                  // waits for a second
  digitalWrite(outPin, LOW);    // sets the LED off
  delay(offTime);    
}
