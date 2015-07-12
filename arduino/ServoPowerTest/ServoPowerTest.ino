#include "Servo.h"

#define servoPin 8
#define servoEnable 13

const int servoFwd  = 180; 
const int servoRev = 100;
Servo myServo;

void setup() {
  myServo.attach(servoPin);
  pinMode(servoEnable, HIGH);
  myServo.write(5);
  delay(100);
  pinMode(servoEnable, LOW);
  

}

void loop() {
  delay(5000);
  moveServoForward();
  delay(5000);
  moveServoBackward();

}

void moveServoForward(){
  pinMode(servoEnable, HIGH);
  myServo.write(servoFwd);
  delay(100);
  pinMode(servoEnable, LOW);
}

void moveServoBackward(){
  pinMode(servoEnable, HIGH);
  myServo.write(servoRev);
  delay(100);
  pinMode(servoEnable, LOW);
}

