#include "Servo.h"
#define servoPin 9

const int servoStop  = 90;
const int servoFwd  = 45; 
const int servoRev = 135;
Servo myServo;

void setup()
{
  myServo.write(servoStop);
  myServo.attach(servoPin);
  Serial.begin(9600);

}

void loop()
{
  myServo.write(90);
  delay(2000);
  myServo.write(180);
  delay(2000);
  myServo.write(0);
  delay(2000);

  //myServo.write(45);
}

