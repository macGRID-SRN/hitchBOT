#include "Servo.h"

#define servoPin 8
#define servoEnable 13

const int servoFwd  = 180; 
const int servoRev = 100;
Servo myServo;

void setup() {
  pinMode(servoEnable, OUTPUT);
  
  myServo.attach(servoPin);
  
  
  digitalWrite(servoEnable, HIGH);
  myServo.write(5);
  delay(2000);
  digitalWrite(servoEnable, LOW);
}

void loop() {
  delay(5000);
  moveServoForward();
  delay(5000);
  moveServoBackward();

}

void moveServoForward(){
  digitalWrite(servoEnable, HIGH);
  delay(50);
  myServo.write(servoFwd);
  delay(2000);
  digitalWrite(servoEnable, LOW);
}

void moveServoBackward(){
  digitalWrite(servoEnable, HIGH);
  delay(50);
  myServo.write(servoRev);
  delay(2000);
  digitalWrite(servoEnable, LOW);
}

