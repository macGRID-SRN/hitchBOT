#include "HT1632.h"
#include "Servo.h"

#define DATA 2
#define WR   3
#define CS   4
#define CS2  5
#define CS3  6
#define CS4  7
#define servoPin 9


const int servoFwd  = 180; 
const int servoRev = 100;
Servo myServo;


HT1632LEDMatrix matrix = HT1632LEDMatrix(DATA, WR, CS, CS2, CS3, CS4);


void setup()
{ 
  myServo.attach(servoPin);
  Serial.begin(9600);
  pinMode(DIGITAL_OUT_PIN, OUTPUT);
  pinMode(DIGITAL_IN_PIN, INPUT);
  matrix.begin(HT1632_COMMON_16NMOS);  
  delay(500);

  matrix.clearScreen(); 
  
}

unsigned char buf[16] = {0};


void loop()
{


}


void modifyPanels(int leds[], int panel)
{
 int x;
 int y = 0;
 
 for(int i = 0; i < sizeof(leds); i++)
 {
   //This keeps track of the x position [0,23].
   x = i % 24;
   
   //This keeps track of y position [0,15].
   if(i > 0 && x == 0)
   {
     y++;
   }

 matrix.drawPixel(x + panel * 24, y, leds[i]);
 }
   matrix.writeScreen();
}



