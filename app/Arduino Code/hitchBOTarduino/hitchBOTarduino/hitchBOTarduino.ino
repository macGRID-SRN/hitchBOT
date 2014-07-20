#include "HT1632.h"
#include <ble_mini.h>
#include "Servo.h"

#define DIGITAL_OUT_PIN    0
#define DIGITAL_IN_PIN     1
#define DATA 2
#define WR   3
#define CS   5
#define CS2  4
#define CS3  6
#define CS4  7
#define servoPin 9


const int servoFwd  = 180; 
const int servoRev = 100;
Servo myServo;
//For two matrices
//HT1632LEDMatrix matrix = HT1632LEDMatrix(DATA, WR, CS, CS2);
//For one matrix
HT1632LEDMatrix matrix = HT1632LEDMatrix(DATA, WR, CS, CS2, CS3, CS4);


void setup()
{ 
  myServo.write(servoFwd);
  myServo.attach(servoPin);
  Serial.begin(9600);
  BLEMini_begin(57600); 
  pinMode(DIGITAL_OUT_PIN, OUTPUT);
  pinMode(DIGITAL_IN_PIN, INPUT);
  matrix.begin(HT1632_COMMON_16NMOS);  
  matrix.fillScreen();
  delay(500);

  matrix.clearScreen(); 
  makeEyes(0, true);
  makeMouth(0);
 // drawText("Hi I'm hitchBOT", 0);
 // drawText("Headed to Victoria", 8);
 // canadaFlag(24*2);
 // canadaMap(24);
  
}

unsigned char buf[16] = {0};
unsigned char len = 0;

void loop()
{
 if ( BLEMini_available())
  {
    // read out command and data
    byte data0 = BLEMini_read();
    byte data1 = BLEMini_read();
    byte data2 = BLEMini_read();
    Serial.println("heyllo");

    Serial.println(data0);

    if (data0 == 0x01)  // Command is to control digital out pin
    {
    moveMouth(0);
    }
    if(data0 == 0x00)
  {
  changeMouthBack(0);
  }  

}
else
{

  chooseRandomGesture(0);
flagHandHeartAnimation();
}


}

void makeEyes(int offSet, boolean fill)
{
    matrix.drawCircle(5 + offSet,4,2, 1);
    matrix.drawCircle(18 + offSet,4,2, 1);
    if(fill)
    {
    matrix.fillCircle(5 + offSet,4,2, 1);
    matrix.fillCircle(18 + offSet,4,2, 1);
    }
    matrix.writeScreen();
}

void makeMouth(int offSet)
{
  int xStart = 4 + offSet;
  int yStart = 13;
  matrix.drawLine(xStart, yStart, xStart + 15, yStart, 1);
  
  //add smirk
  
  matrix.drawPixel(xStart - 1, yStart - 1, 1);
  matrix.drawPixel(xStart + 1 + 15, yStart - 1, 1);
  
  matrix.drawPixel(xStart - 2, yStart - 2, 1);
  matrix.drawPixel(xStart + 2 + 15, yStart - 2, 1);

  matrix.writeScreen();
}

void makeSquareMouth(int offSet)
{
    int xStart = 3 + offSet;
  int yStart = 13;
  matrix.drawLine(xStart, yStart, xStart + 17, yStart, 1);
  
  //add smirk
  
  matrix.drawPixel(xStart, yStart - 1, 1);
  matrix.drawPixel(xStart + 17, yStart - 1, 1);
  
  matrix.drawPixel(xStart, yStart - 2, 1);
  matrix.drawPixel(xStart + 17, yStart - 2, 1);

  matrix.writeScreen();
}


void makeBorder()
{
  //horizontal
  matrix.drawLine(0, 0, 23, 0, 1);
  matrix.drawLine(0, 0, 0, 15, 1);
  //vertical
  matrix.drawLine(0, 15, 23, 15, 1);
  matrix.drawLine(23, 0, 23, 15, 1);
  
  matrix.writeScreen();
}



void makeBlink(int offSet, int timeDelay)
{
  makeEyes(offSet, true);
  delay (timeDelay);
  
  clrLine(4 + offSet, 2, 6 + offSet, 2 );
  clrLine(4 + offSet, 7, 6 + offSet, 7 );
  
  clrLine(4 + offSet + 13, 2, 6 + offSet + 13, 2 );
  clrLine(4 + offSet + 13, 7, 6 + offSet + 13, 7 );
  
  delay(timeDelay);
  
  clrLine(3 + offSet, 3, 7 + offSet, 3 );
  clrLine(3 + offSet, 6, 7 + offSet, 6 );
  
  clrLine(3 + offSet + 13, 3, 7 + offSet + 13, 3 );
  clrLine(3 + offSet + 13, 6, 7 + offSet + 13, 6 );

  delay(timeDelay);

  clrLine(3 + offSet, 4, 7 + offSet, 4 );
  clrLine(3 + offSet, 5, 7 + offSet, 5 );
  
  clrLine(3 + offSet + 13, 4, 7 + offSet + 13, 4 );
  clrLine(3 + offSet + 13, 5, 7 + offSet + 13, 5 );
  
  delay(timeDelay);
  
  matrix.drawLine(3 + offSet, 4, 7 + offSet, 4 , 1);
  matrix.drawLine(3 + offSet, 5, 7 + offSet, 5 , 1);
  
  matrix.drawLine(3 + offSet + 13, 4, 7 + offSet + 13, 4, 1 );
  matrix.drawLine(3 + offSet + 13, 5, 7 + offSet + 13, 5, 1 );
  
  matrix.writeScreen();
  
  delay(timeDelay);
  
  matrix.drawLine(3 + offSet, 3, 7 + offSet, 3, 1 );
  matrix.drawLine(3 + offSet, 6, 7 + offSet, 6, 1 );
  
  matrix.drawLine(3 + offSet + 13, 3, 7 + offSet + 13, 3, 1 );
  matrix.drawLine(3 + offSet + 13, 6, 7 + offSet + 13, 6, 1 );
  
  delay(timeDelay);
  
  matrix.writeScreen();
  
  delay(timeDelay);
  
  makeEyes(offSet, true);
}

void makeWink(int offSet, int timeDelay)
{
  makeEyes(offSet, true);
  delay (timeDelay + 100);
  
  clrLine(4 + offSet, 2, 6 + offSet, 2 );
  clrLine(4 + offSet, 7, 6 + offSet, 7 );
  
  delay(timeDelay + 100);
  
  clrLine(3 + offSet, 3, 7 + offSet, 3 );
  clrLine(3 + offSet, 6, 7 + offSet, 6 );

  delay(timeDelay + 100);

  clrLine(3 + offSet, 4, 7 + offSet, 4 );
  clrLine(3 + offSet, 5, 7 + offSet, 5 );
  
  delay(timeDelay + 200);
  
  matrix.drawLine(3 + offSet, 4, 7 + offSet, 4, 1);
  matrix.drawLine(3 + offSet, 5, 7 + offSet, 5, 1 );
  
  delay(timeDelay + 100);
  
  matrix.drawLine(3 + offSet, 3, 7 + offSet, 3, 1 );
  matrix.drawLine(3 + offSet, 6, 7 + offSet, 6, 1 );
  
  delay(timeDelay + 100);
  
  makeEyes(offSet, true);
  
}

void clrLine(int startX, int startY, int endX, int endY)
{
  for(int i = startX; i < endX + 1 ; i++)
  {
    for(int j = startY; j < endY + 1; j++)
    {
      matrix.clrPixel(i, j);
    }
    
  }
  matrix.writeScreen();
}

void transitionToMouth(int offSet, int timeDelay)
{
  matrix.drawPixel(11 + offSet,13, 1);
  matrix.writeScreen();
  delay(timeDelay);
  matrix.drawLine(9 + offSet, 13, 14 + offSet, 13, 1);
  matrix.writeScreen();
  delay(timeDelay);
  matrix.drawLine(7 + offSet, 13, 15 + offSet, 13, 1);
  matrix.writeScreen();
  delay(timeDelay);
  matrix.drawLine(5 + offSet, 13, 17 + offSet, 13, 1);
  matrix.writeScreen();
  delay(timeDelay);
  matrix.drawLine(4 + offSet, 13, 19 + offSet, 13, 1);
  matrix.writeScreen();
  delay(timeDelay);
  makeMouth(offSet);
}

void retractMouth(int offSet, int timeDelay)
{
  int xStart = 4 + offSet;
  int yStart = 13;
  matrix.clrPixel(xStart - 1, yStart - 1);
  matrix.clrPixel(xStart + 1 + 15, yStart - 1);
  matrix.writeScreen();
  delay(timeDelay);
  matrix.clrPixel(xStart - 2, yStart - 2);
  matrix.clrPixel(xStart + 2 + 15, yStart - 2);
  matrix.writeScreen();
  delay(timeDelay);
  int j = 0;
  for(int i = 4 + offSet ; i < 8 + offSet + 1 ; i++)
  {
    //because i starts at 4
    matrix.clrPixel(i, 13);
    matrix.clrPixel(19 - j + offSet, 13);
    matrix.writeScreen();
    j +=1;
    delay(25);   
  }
  
}

void makeOmouth(int offSet, int timeDelay)
{
  matrix.drawCircle(11 + offSet, 13, 3, 1);
  matrix.writeScreen();
  delay(timeDelay);
  matrix.drawCircle(11 + offSet, 13, 3, 0);
  matrix.writeScreen();
  delay(timeDelay);
  matrix.drawCircle(11 + offSet, 13, 2, 1);
  matrix.writeScreen();
  delay(timeDelay);
  matrix.drawCircle(11 + offSet, 13, 2, 0);
  matrix.writeScreen();

}

int getRandomNumber()
{
  return random(1, 5) * 1000;
}

void chooseRandomGesture(int offSet)
{
  int timeDelay = random(20, 35);
  int chooser = random(0, 11);
  if (chooser == 0)
  {
    makeWink(offSet, timeDelay);
  }
  if(chooser == 1)
  {
    makeBlink(offSet, timeDelay);
  }
  if (chooser == 3)
  {
    transitionToMouth(0, timeDelay);
    delay(getRandomNumber());
    retractMouth(0, timeDelay);
    //makeOmouth(0, timeDelay);
    transitionToMouth(0, timeDelay);
  }
  if(chooser > 0)
  {
     myServo.write(0);
    delay(2000);
        myServo.write(160);
     delay(2000);
     myServo.write(0);
     delay(1000);

  }

  if(chooser > 4)
  {
  // delay(1000);
  }
}

void transitionToSquareMouth(int offSet)
{
  int xStart = 4 + offSet;
  int yStart = 13;
  matrix.clrPixel(xStart - 1, yStart - 1);
  matrix.clrPixel(xStart + 1 + 15, yStart - 1);
  matrix.writeScreen();
  matrix.clrPixel(xStart - 2, yStart - 2);
  matrix.clrPixel(xStart + 2 + 15, yStart - 2);
  matrix.writeScreen();
  int j = 0;
  for(int i = 4 + offSet ; i < 8 + offSet + 1 ; i++)
  {
    //because i starts at 4
    matrix.clrPixel(i, 13);
    matrix.clrPixel(19 - j + offSet, 13);
    matrix.writeScreen();
    j +=1;
  }
  makeSquareMouth(0);
}

void changeMouthBack(int offSet)
{
    matrix.drawLine(3 ,11, 20, 11, 0);
    matrix.clrPixel(19,12);
    matrix.clrPixel(4,12);
    matrix.clrPixel(3,13);
    matrix.clrPixel(20,13);
    matrix.writeScreen();
    makeMouth(0);

}

void moveMouth(int offSet)
{
  transitionToSquareMouth(0);
  matrix.drawLine(3 ,11, 20, 11, 1);
  matrix.writeScreen();
  delay(10);

  matrix.drawLine(3,11, 20, 11, 0);
  matrix.drawLine(3, 12, 20, 12, 1);
  matrix.writeScreen();
  delay(10);
  
  matrix.drawLine(4,12,19,12,0);
  matrix.drawLine(3 ,11, 20, 11, 1);
  delay(10);
  
}

void drawText(char words[], int offSet)
{
  matrix.setTextSize(1);
  matrix.setTextColor(1);
  matrix.setCursor(24 ,offSet);
  matrix.print(words);
  matrix.writeScreen();
}

void canadaFlag(int offSet)
{matrix.drawPixel(0 + offSet, 0, 1);
matrix.drawPixel(1 + offSet, 0, 1);
matrix.drawPixel(2 + offSet, 0, 1);
matrix.drawPixel(3 + offSet, 0, 1);
matrix.drawPixel(4 + offSet, 0, 1);
matrix.drawPixel(5 + offSet, 0, 1);
matrix.drawPixel(18 + offSet, 0, 1);
matrix.drawPixel(19 + offSet, 0, 1);
matrix.drawPixel(20 + offSet, 0, 1);
matrix.drawPixel(21 + offSet, 0, 1);
matrix.drawPixel(22 + offSet, 0, 1);
matrix.drawPixel(23 + offSet, 0, 1);
matrix.drawPixel(0 + offSet, 1, 1);
matrix.drawPixel(1 + offSet, 1, 1);
matrix.drawPixel(2 + offSet, 1, 1);
matrix.drawPixel(3 + offSet, 1, 1);
matrix.drawPixel(4 + offSet, 1, 1);
matrix.drawPixel(5 + offSet, 1, 1);
matrix.drawPixel(18 + offSet, 1, 1);
matrix.drawPixel(19 + offSet, 1, 1);
matrix.drawPixel(20 + offSet, 1, 1);
matrix.drawPixel(21 + offSet, 1, 1);
matrix.drawPixel(22 + offSet, 1, 1);
matrix.drawPixel(23 + offSet, 1, 1);
matrix.drawPixel(0 + offSet, 2, 1);
matrix.drawPixel(1 + offSet, 2, 1);
matrix.drawPixel(2 + offSet, 2, 1);
matrix.drawPixel(3 + offSet, 2, 1);
matrix.drawPixel(4 + offSet, 2, 1);
matrix.drawPixel(5 + offSet, 2, 1);
matrix.drawPixel(12 + offSet, 2, 1);
matrix.drawPixel(18 + offSet, 2, 1);
matrix.drawPixel(19 + offSet, 2, 1);
matrix.drawPixel(20 + offSet, 2, 1);
matrix.drawPixel(21 + offSet, 2, 1);
matrix.drawPixel(22 + offSet, 2, 1);
matrix.drawPixel(23 + offSet, 2, 1);
matrix.drawPixel(0 + offSet, 3, 1);
matrix.drawPixel(1 + offSet, 3, 1);
matrix.drawPixel(2 + offSet, 3, 1);
matrix.drawPixel(3 + offSet, 3, 1);
matrix.drawPixel(4 + offSet, 3, 1);
matrix.drawPixel(5 + offSet, 3, 1);
matrix.drawPixel(12 + offSet, 3, 1);
matrix.drawPixel(18 + offSet, 3, 1);
matrix.drawPixel(19 + offSet, 3, 1);
matrix.drawPixel(20 + offSet, 3, 1);
matrix.drawPixel(21 + offSet, 3, 1);
matrix.drawPixel(22 + offSet, 3, 1);
matrix.drawPixel(23 + offSet, 3, 1);
matrix.drawPixel(0 + offSet, 4, 1);
matrix.drawPixel(1 + offSet, 4, 1);
matrix.drawPixel(2 + offSet, 4, 1);
matrix.drawPixel(3 + offSet, 4, 1);
matrix.drawPixel(4 + offSet, 4, 1);
matrix.drawPixel(5 + offSet, 4, 1);
matrix.drawPixel(11 + offSet, 4, 1);
matrix.drawPixel(12 + offSet, 4, 1);
matrix.drawPixel(13 + offSet, 4, 1);
matrix.drawPixel(18 + offSet, 4, 1);
matrix.drawPixel(19 + offSet, 4, 1);
matrix.drawPixel(20 + offSet, 4, 1);
matrix.drawPixel(21 + offSet, 4, 1);
matrix.drawPixel(22 + offSet, 4, 1);
matrix.drawPixel(23 + offSet, 4, 1);
matrix.drawPixel(0 + offSet, 5, 1);
matrix.drawPixel(1 + offSet, 5, 1);
matrix.drawPixel(2 + offSet, 5, 1);
matrix.drawPixel(3 + offSet, 5, 1);
matrix.drawPixel(4 + offSet, 5, 1);
matrix.drawPixel(5 + offSet, 5, 1);
matrix.drawPixel(11 + offSet, 5, 1);
matrix.drawPixel(12 + offSet, 5, 1);
matrix.drawPixel(13 + offSet, 5, 1);
matrix.drawPixel(18 + offSet, 5, 1);
matrix.drawPixel(19 + offSet, 5, 1);
matrix.drawPixel(20 + offSet, 5, 1);
matrix.drawPixel(21 + offSet, 5, 1);
matrix.drawPixel(22 + offSet, 5, 1);
matrix.drawPixel(23 + offSet, 5, 1);
matrix.drawPixel(0 + offSet, 6, 1);
matrix.drawPixel(1 + offSet, 6, 1);
matrix.drawPixel(2 + offSet, 6, 1);
matrix.drawPixel(3 + offSet, 6, 1);
matrix.drawPixel(4 + offSet, 6, 1);
matrix.drawPixel(5 + offSet, 6, 1);
matrix.drawPixel(8 + offSet, 6, 1);
matrix.drawPixel(9 + offSet, 6, 1);
matrix.drawPixel(11 + offSet, 6, 1);
matrix.drawPixel(12 + offSet, 6, 1);
matrix.drawPixel(13 + offSet, 6, 1);
matrix.drawPixel(15 + offSet, 6, 1);
matrix.drawPixel(16 + offSet, 6, 1);
matrix.drawPixel(18 + offSet, 6, 1);
matrix.drawPixel(19 + offSet, 6, 1);
matrix.drawPixel(20 + offSet, 6, 1);
matrix.drawPixel(21 + offSet, 6, 1);
matrix.drawPixel(22 + offSet, 6, 1);
matrix.drawPixel(23 + offSet, 6, 1);
matrix.drawPixel(0 + offSet, 7, 1);
matrix.drawPixel(1 + offSet, 7, 1);
matrix.drawPixel(2 + offSet, 7, 1);
matrix.drawPixel(3 + offSet, 7, 1);
matrix.drawPixel(4 + offSet, 7, 1);
matrix.drawPixel(5 + offSet, 7, 1);
matrix.drawPixel(8 + offSet, 7, 1);
matrix.drawPixel(9 + offSet, 7, 1);
matrix.drawPixel(10 + offSet, 7, 1);
matrix.drawPixel(11 + offSet, 7, 1);
matrix.drawPixel(12 + offSet, 7, 1);
matrix.drawPixel(13 + offSet, 7, 1);
matrix.drawPixel(14 + offSet, 7, 1);
matrix.drawPixel(15 + offSet, 7, 1);
matrix.drawPixel(16 + offSet, 7, 1);
matrix.drawPixel(18 + offSet, 7, 1);
matrix.drawPixel(19 + offSet, 7, 1);
matrix.drawPixel(20 + offSet, 7, 1);
matrix.drawPixel(21 + offSet, 7, 1);
matrix.drawPixel(22 + offSet, 7, 1);
matrix.drawPixel(23 + offSet, 7, 1);
matrix.drawPixel(0 + offSet, 8, 1);
matrix.drawPixel(1 + offSet, 8, 1);
matrix.drawPixel(2 + offSet, 8, 1);
matrix.drawPixel(3 + offSet, 8, 1);
matrix.drawPixel(4 + offSet, 8, 1);
matrix.drawPixel(5 + offSet, 8, 1);
matrix.drawPixel(8 + offSet, 8, 1);
matrix.drawPixel(9 + offSet, 8, 1);
matrix.drawPixel(10 + offSet, 8, 1);
matrix.drawPixel(11 + offSet, 8, 1);
matrix.drawPixel(12 + offSet, 8, 1);
matrix.drawPixel(13 + offSet, 8, 1);
matrix.drawPixel(14 + offSet, 8, 1);
matrix.drawPixel(15 + offSet, 8, 1);
matrix.drawPixel(16 + offSet, 8, 1);
matrix.drawPixel(18 + offSet, 8, 1);
matrix.drawPixel(19 + offSet, 8, 1);
matrix.drawPixel(20 + offSet, 8, 1);
matrix.drawPixel(21 + offSet, 8, 1);
matrix.drawPixel(22 + offSet, 8, 1);
matrix.drawPixel(23 + offSet, 8, 1);
matrix.drawPixel(0 + offSet, 9, 1);
matrix.drawPixel(1 + offSet, 9, 1);
matrix.drawPixel(2 + offSet, 9, 1);
matrix.drawPixel(3 + offSet, 9, 1);
matrix.drawPixel(4 + offSet, 9, 1);
matrix.drawPixel(5 + offSet, 9, 1);
matrix.drawPixel(8 + offSet, 9, 1);
matrix.drawPixel(9 + offSet, 9, 1);
matrix.drawPixel(10 + offSet, 9, 1);
matrix.drawPixel(11 + offSet, 9, 1);
matrix.drawPixel(12 + offSet, 9, 1);
matrix.drawPixel(13 + offSet, 9, 1);
matrix.drawPixel(14 + offSet, 9, 1);
matrix.drawPixel(15 + offSet, 9, 1);
matrix.drawPixel(18 + offSet, 9, 1);
matrix.drawPixel(19 + offSet, 9, 1);
matrix.drawPixel(20 + offSet, 9, 1);
matrix.drawPixel(21 + offSet, 9, 1);
matrix.drawPixel(22 + offSet, 9, 1);
matrix.drawPixel(23 + offSet, 9, 1);
matrix.drawPixel(0 + offSet, 10, 1);
matrix.drawPixel(1 + offSet, 10, 1);
matrix.drawPixel(2 + offSet, 10, 1);
matrix.drawPixel(3 + offSet, 10, 1);
matrix.drawPixel(4 + offSet, 10, 1);
matrix.drawPixel(5 + offSet, 10, 1);
matrix.drawPixel(9 + offSet, 10, 1);
matrix.drawPixel(10 + offSet, 10, 1);
matrix.drawPixel(11 + offSet, 10, 1);
matrix.drawPixel(12 + offSet, 10, 1);
matrix.drawPixel(13 + offSet, 10, 1);
matrix.drawPixel(14 + offSet, 10, 1);
matrix.drawPixel(15 + offSet, 10, 1);
matrix.drawPixel(18 + offSet, 10, 1);
matrix.drawPixel(19 + offSet, 10, 1);
matrix.drawPixel(20 + offSet, 10, 1);
matrix.drawPixel(21 + offSet, 10, 1);
matrix.drawPixel(22 + offSet, 10, 1);
matrix.drawPixel(23 + offSet, 10, 1);
matrix.drawPixel(0 + offSet, 11, 1);
matrix.drawPixel(1 + offSet, 11, 1);
matrix.drawPixel(2 + offSet, 11, 1);
matrix.drawPixel(3 + offSet, 11, 1);
matrix.drawPixel(4 + offSet, 11, 1);
matrix.drawPixel(5 + offSet, 11, 1);
matrix.drawPixel(10 + offSet, 11, 1);
matrix.drawPixel(11 + offSet, 11, 1);
matrix.drawPixel(12 + offSet, 11, 1);
matrix.drawPixel(13 + offSet, 11, 1);
matrix.drawPixel(14 + offSet, 11, 1);
matrix.drawPixel(18 + offSet, 11, 1);
matrix.drawPixel(19 + offSet, 11, 1);
matrix.drawPixel(20 + offSet, 11, 1);
matrix.drawPixel(21 + offSet, 11, 1);
matrix.drawPixel(22 + offSet, 11, 1);
matrix.drawPixel(23 + offSet, 11, 1);
matrix.drawPixel(0 + offSet, 12, 1);
matrix.drawPixel(1 + offSet, 12, 1);
matrix.drawPixel(2 + offSet, 12, 1);
matrix.drawPixel(3 + offSet, 12, 1);
matrix.drawPixel(4 + offSet, 12, 1);
matrix.drawPixel(5 + offSet, 12, 1);
matrix.drawPixel(12 + offSet, 12, 1);
matrix.drawPixel(18 + offSet, 12, 1);
matrix.drawPixel(19 + offSet, 12, 1);
matrix.drawPixel(20 + offSet, 12, 1);
matrix.drawPixel(21 + offSet, 12, 1);
matrix.drawPixel(22 + offSet, 12, 1);
matrix.drawPixel(23 + offSet, 12, 1);
matrix.drawPixel(0 + offSet, 13, 1);
matrix.drawPixel(1 + offSet, 13, 1);
matrix.drawPixel(2 + offSet, 13, 1);
matrix.drawPixel(3 + offSet, 13, 1);
matrix.drawPixel(4 + offSet, 13, 1);
matrix.drawPixel(5 + offSet, 13, 1);
matrix.drawPixel(12 + offSet, 13, 1);
matrix.drawPixel(18 + offSet, 13, 1);
matrix.drawPixel(19 + offSet, 13, 1);
matrix.drawPixel(20 + offSet, 13, 1);
matrix.drawPixel(21 + offSet, 13, 1);
matrix.drawPixel(22 + offSet, 13, 1);
matrix.drawPixel(23 + offSet, 13, 1);
matrix.drawPixel(0 + offSet, 14, 1);
matrix.drawPixel(1 + offSet, 14, 1);
matrix.drawPixel(2 + offSet, 14, 1);
matrix.drawPixel(3 + offSet, 14, 1);
matrix.drawPixel(4 + offSet, 14, 1);
matrix.drawPixel(5 + offSet, 14, 1);
matrix.drawPixel(12 + offSet, 14, 1);
matrix.drawPixel(18 + offSet, 14, 1);
matrix.drawPixel(19 + offSet, 14, 1);
matrix.drawPixel(20 + offSet, 14, 1);
matrix.drawPixel(21 + offSet, 14, 1);
matrix.drawPixel(22 + offSet, 14, 1);
matrix.drawPixel(23 + offSet, 14, 1);
matrix.drawPixel(0 + offSet, 15, 1);
matrix.drawPixel(1 + offSet, 15, 1);
matrix.drawPixel(2 + offSet, 15, 1);
matrix.drawPixel(3 + offSet, 15, 1);
matrix.drawPixel(4 + offSet, 15, 1);
matrix.drawPixel(5 + offSet, 15, 1);
matrix.drawPixel(18 + offSet, 15, 1);
matrix.drawPixel(19 + offSet, 15, 1);
matrix.drawPixel(20 + offSet, 15, 1);
matrix.drawPixel(21 + offSet, 15, 1);
matrix.drawPixel(22 + offSet, 15, 1);
matrix.drawPixel(23 + offSet, 15, 1);

 matrix.writeScreen();
}

void canadaMap(int offSet)
{matrix.drawPixel(10 + offSet, 0, 1);
matrix.drawPixel(11 + offSet, 0, 1);
matrix.drawPixel(12 + offSet, 0, 1);
matrix.drawPixel(8 + offSet, 1, 1);
matrix.drawPixel(9 + offSet, 1, 1);
matrix.drawPixel(10 + offSet, 1, 1);
matrix.drawPixel(11 + offSet, 1, 1);
matrix.drawPixel(12 + offSet, 1, 1);
matrix.drawPixel(7 + offSet, 2, 1);
matrix.drawPixel(8 + offSet, 2, 1);
matrix.drawPixel(9 + offSet, 2, 1);
matrix.drawPixel(10 + offSet, 2, 1);
matrix.drawPixel(11 + offSet, 2, 1);
matrix.drawPixel(12 + offSet, 2, 1);
matrix.drawPixel(2 + offSet, 3, 1);
matrix.drawPixel(3 + offSet, 3, 1);
matrix.drawPixel(5 + offSet, 3, 1);
matrix.drawPixel(6 + offSet, 3, 1);
matrix.drawPixel(7 + offSet, 3, 1);
matrix.drawPixel(8 + offSet, 3, 1);
matrix.drawPixel(9 + offSet, 3, 1);
matrix.drawPixel(10 + offSet, 3, 1);
matrix.drawPixel(11 + offSet, 3, 1);
matrix.drawPixel(12 + offSet, 3, 1);
matrix.drawPixel(1 + offSet, 4, 1);
matrix.drawPixel(2 + offSet, 4, 1);
matrix.drawPixel(3 + offSet, 4, 1);
matrix.drawPixel(4 + offSet, 4, 1);
matrix.drawPixel(5 + offSet, 4, 1);
matrix.drawPixel(6 + offSet, 4, 1);
matrix.drawPixel(7 + offSet, 4, 1);
matrix.drawPixel(8 + offSet, 4, 1);
matrix.drawPixel(9 + offSet, 4, 1);
matrix.drawPixel(10 + offSet, 4, 1);
matrix.drawPixel(11 + offSet, 4, 1);
matrix.drawPixel(12 + offSet, 4, 1);
matrix.drawPixel(13 + offSet, 4, 1);
matrix.drawPixel(14 + offSet, 4, 1);
matrix.drawPixel(0 + offSet, 5, 1);
matrix.drawPixel(1 + offSet, 5, 1);
matrix.drawPixel(2 + offSet, 5, 1);
matrix.drawPixel(3 + offSet, 5, 1);
matrix.drawPixel(4 + offSet, 5, 1);
matrix.drawPixel(5 + offSet, 5, 1);
matrix.drawPixel(6 + offSet, 5, 1);
matrix.drawPixel(7 + offSet, 5, 1);
matrix.drawPixel(8 + offSet, 5, 1);
matrix.drawPixel(9 + offSet, 5, 1);
matrix.drawPixel(10 + offSet, 5, 1);
matrix.drawPixel(11 + offSet, 5, 1);
matrix.drawPixel(12 + offSet, 5, 1);
matrix.drawPixel(13 + offSet, 5, 1);
matrix.drawPixel(14 + offSet, 5, 1);
matrix.drawPixel(15 + offSet, 5, 1);
matrix.drawPixel(16 + offSet, 5, 1);
matrix.drawPixel(0 + offSet, 6, 1);
matrix.drawPixel(1 + offSet, 6, 1);
matrix.drawPixel(2 + offSet, 6, 1);
matrix.drawPixel(3 + offSet, 6, 1);
matrix.drawPixel(4 + offSet, 6, 1);
matrix.drawPixel(5 + offSet, 6, 1);
matrix.drawPixel(6 + offSet, 6, 1);
matrix.drawPixel(7 + offSet, 6, 1);
matrix.drawPixel(8 + offSet, 6, 1);
matrix.drawPixel(9 + offSet, 6, 1);
matrix.drawPixel(10 + offSet, 6, 1);
matrix.drawPixel(11 + offSet, 6, 1);
matrix.drawPixel(12 + offSet, 6, 1);
matrix.drawPixel(13 + offSet, 6, 1);
matrix.drawPixel(14 + offSet, 6, 1);
matrix.drawPixel(15 + offSet, 6, 1);
matrix.drawPixel(16 + offSet, 6, 1);
matrix.drawPixel(0 + offSet, 7, 1);
matrix.drawPixel(1 + offSet, 7, 1);
matrix.drawPixel(2 + offSet, 7, 1);
matrix.drawPixel(3 + offSet, 7, 1);
matrix.drawPixel(4 + offSet, 7, 1);
matrix.drawPixel(5 + offSet, 7, 1);
matrix.drawPixel(6 + offSet, 7, 1);
matrix.drawPixel(7 + offSet, 7, 1);
matrix.drawPixel(8 + offSet, 7, 1);
matrix.drawPixel(9 + offSet, 7, 1);
matrix.drawPixel(10 + offSet, 7, 1);
matrix.drawPixel(11 + offSet, 7, 1);
matrix.drawPixel(12 + offSet, 7, 1);
matrix.drawPixel(13 + offSet, 7, 1);
matrix.drawPixel(14 + offSet, 7, 1);
matrix.drawPixel(15 + offSet, 7, 1);
matrix.drawPixel(16 + offSet, 7, 1);
matrix.drawPixel(17 + offSet, 7, 1);
matrix.drawPixel(0 + offSet, 8, 1);
matrix.drawPixel(1 + offSet, 8, 1);
matrix.drawPixel(2 + offSet, 8, 1);
matrix.drawPixel(3 + offSet, 8, 1);
matrix.drawPixel(4 + offSet, 8, 1);
matrix.drawPixel(5 + offSet, 8, 1);
matrix.drawPixel(6 + offSet, 8, 1);
matrix.drawPixel(7 + offSet, 8, 1);
matrix.drawPixel(8 + offSet, 8, 1);
matrix.drawPixel(9 + offSet, 8, 1);
matrix.drawPixel(10 + offSet, 8, 1);
matrix.drawPixel(12 + offSet, 8, 1);
matrix.drawPixel(13 + offSet, 8, 1);
matrix.drawPixel(14 + offSet, 8, 1);
matrix.drawPixel(15 + offSet, 8, 1);
matrix.drawPixel(16 + offSet, 8, 1);
matrix.drawPixel(17 + offSet, 8, 1);
matrix.drawPixel(18 + offSet, 8, 1);
matrix.drawPixel(19 + offSet, 8, 1);
matrix.drawPixel(20 + offSet, 8, 1);
matrix.drawPixel(0 + offSet, 9, 1);
matrix.drawPixel(1 + offSet, 9, 1);
matrix.drawPixel(2 + offSet, 9, 1);
matrix.drawPixel(3 + offSet, 9, 1);
matrix.drawPixel(4 + offSet, 9, 1);
matrix.drawPixel(5 + offSet, 9, 1);
matrix.drawPixel(6 + offSet, 9, 1);
matrix.drawPixel(7 + offSet, 9, 1);
matrix.drawPixel(8 + offSet, 9, 1);
matrix.drawPixel(9 + offSet, 9, 1);
matrix.drawPixel(10 + offSet, 9, 1);
matrix.drawPixel(14 + offSet, 9, 1);
matrix.drawPixel(15 + offSet, 9, 1);
matrix.drawPixel(16 + offSet, 9, 1);
matrix.drawPixel(17 + offSet, 9, 1);
matrix.drawPixel(18 + offSet, 9, 1);
matrix.drawPixel(19 + offSet, 9, 1);
matrix.drawPixel(20 + offSet, 9, 1);
matrix.drawPixel(21 + offSet, 9, 1);
matrix.drawPixel(0 + offSet, 10, 1);
matrix.drawPixel(1 + offSet, 10, 1);
matrix.drawPixel(2 + offSet, 10, 1);
matrix.drawPixel(3 + offSet, 10, 1);
matrix.drawPixel(4 + offSet, 10, 1);
matrix.drawPixel(5 + offSet, 10, 1);
matrix.drawPixel(6 + offSet, 10, 1);
matrix.drawPixel(7 + offSet, 10, 1);
matrix.drawPixel(8 + offSet, 10, 1);
matrix.drawPixel(9 + offSet, 10, 1);
matrix.drawPixel(10 + offSet, 10, 1);
matrix.drawPixel(11 + offSet, 10, 1);
matrix.drawPixel(12 + offSet, 10, 1);
matrix.drawPixel(13 + offSet, 10, 1);
matrix.drawPixel(14 + offSet, 10, 1);
matrix.drawPixel(15 + offSet, 10, 1);
matrix.drawPixel(16 + offSet, 10, 1);
matrix.drawPixel(17 + offSet, 10, 1);
matrix.drawPixel(18 + offSet, 10, 1);
matrix.drawPixel(19 + offSet, 10, 1);
matrix.drawPixel(20 + offSet, 10, 1);
matrix.drawPixel(21 + offSet, 10, 1);
matrix.drawPixel(22 + offSet, 10, 1);
matrix.drawPixel(23 + offSet, 10, 1);
matrix.drawPixel(0 + offSet, 11, 1);
matrix.drawPixel(1 + offSet, 11, 1);
matrix.drawPixel(2 + offSet, 11, 1);
matrix.drawPixel(3 + offSet, 11, 1);
matrix.drawPixel(4 + offSet, 11, 1);
matrix.drawPixel(5 + offSet, 11, 1);
matrix.drawPixel(6 + offSet, 11, 1);
matrix.drawPixel(7 + offSet, 11, 1);
matrix.drawPixel(8 + offSet, 11, 1);
matrix.drawPixel(9 + offSet, 11, 1);
matrix.drawPixel(10 + offSet, 11, 1);
matrix.drawPixel(11 + offSet, 11, 1);
matrix.drawPixel(12 + offSet, 11, 1);
matrix.drawPixel(13 + offSet, 11, 1);
matrix.drawPixel(14 + offSet, 11, 1);
matrix.drawPixel(15 + offSet, 11, 1);
matrix.drawPixel(16 + offSet, 11, 1);
matrix.drawPixel(17 + offSet, 11, 1);
matrix.drawPixel(18 + offSet, 11, 1);
matrix.drawPixel(19 + offSet, 11, 1);
matrix.drawPixel(20 + offSet, 11, 1);
matrix.drawPixel(21 + offSet, 11, 1);
matrix.drawPixel(22 + offSet, 11, 1);
matrix.drawPixel(1 + offSet, 12, 1);
matrix.drawPixel(2 + offSet, 12, 1);
matrix.drawPixel(3 + offSet, 12, 1);
matrix.drawPixel(4 + offSet, 12, 1);
matrix.drawPixel(5 + offSet, 12, 1);
matrix.drawPixel(6 + offSet, 12, 1);
matrix.drawPixel(7 + offSet, 12, 1);
matrix.drawPixel(8 + offSet, 12, 1);
matrix.drawPixel(9 + offSet, 12, 1);
matrix.drawPixel(10 + offSet, 12, 1);
matrix.drawPixel(11 + offSet, 12, 1);
matrix.drawPixel(12 + offSet, 12, 1);
matrix.drawPixel(13 + offSet, 12, 1);
matrix.drawPixel(14 + offSet, 12, 1);
matrix.drawPixel(15 + offSet, 12, 1);
matrix.drawPixel(16 + offSet, 12, 1);
matrix.drawPixel(17 + offSet, 12, 1);
matrix.drawPixel(18 + offSet, 12, 1);
matrix.drawPixel(19 + offSet, 12, 1);
matrix.drawPixel(20 + offSet, 12, 1);
matrix.drawPixel(21 + offSet, 12, 1);
matrix.drawPixel(22 + offSet, 12, 1);
matrix.drawPixel(5 + offSet, 13, 1);
matrix.drawPixel(6 + offSet, 13, 1);
matrix.drawPixel(7 + offSet, 13, 1);
matrix.drawPixel(8 + offSet, 13, 1);
matrix.drawPixel(9 + offSet, 13, 1);
matrix.drawPixel(10 + offSet, 13, 1);
matrix.drawPixel(11 + offSet, 13, 1);
matrix.drawPixel(12 + offSet, 13, 1);
matrix.drawPixel(13 + offSet, 13, 1);
matrix.drawPixel(14 + offSet, 13, 1);
matrix.drawPixel(15 + offSet, 13, 1);
matrix.drawPixel(16 + offSet, 13, 1);
matrix.drawPixel(17 + offSet, 13, 1);
matrix.drawPixel(18 + offSet, 13, 1);
matrix.drawPixel(19 + offSet, 13, 1);
matrix.drawPixel(20 + offSet, 13, 1);
matrix.drawPixel(21 + offSet, 13, 1);
matrix.drawPixel(22 + offSet, 13, 1);
matrix.drawPixel(13 + offSet, 14, 1);
matrix.drawPixel(14 + offSet, 14, 1);
matrix.drawPixel(15 + offSet, 14, 1);
matrix.drawPixel(16 + offSet, 14, 1);
matrix.drawPixel(17 + offSet, 14, 1);
matrix.drawPixel(15 + offSet, 15, 1);
matrix.drawPixel(16 + offSet, 15, 1);

 matrix.writeScreen();
}

void flagOne(int offSet, int onOff)
{
matrix.drawPixel(3 + offSet, 0, onOff);
matrix.drawPixel(4 + offSet, 0, onOff);
matrix.drawPixel(5 + offSet, 0, onOff);
matrix.drawPixel(6 + offSet, 0, onOff);
matrix.drawPixel(7 + offSet, 0, onOff);
matrix.drawPixel(8 + offSet, 0, onOff);
matrix.drawPixel(9 + offSet, 0, onOff);
matrix.drawPixel(22 + offSet, 0, onOff);
matrix.drawPixel(23 + offSet, 0, onOff);
matrix.drawPixel(1 + offSet, 1, onOff);
matrix.drawPixel(2 + offSet, 1, onOff);
matrix.drawPixel(3 + offSet, 1, onOff);
matrix.drawPixel(4 + offSet, 1, onOff);
matrix.drawPixel(10 + offSet, 1, onOff);
matrix.drawPixel(11 + offSet, 1, onOff);
matrix.drawPixel(12 + offSet, 1, onOff);
matrix.drawPixel(20 + offSet, 1, onOff);
matrix.drawPixel(21 + offSet, 1, onOff);
matrix.drawPixel(22 + offSet, 1, onOff);
matrix.drawPixel(23 + offSet, 1, onOff);
matrix.drawPixel(0 + offSet, 2, onOff);
matrix.drawPixel(1 + offSet, 2, onOff);
matrix.drawPixel(2 + offSet, 2, onOff);
matrix.drawPixel(3 + offSet, 2, onOff);
matrix.drawPixel(4 + offSet, 2, onOff);
matrix.drawPixel(13 + offSet, 2, onOff);
matrix.drawPixel(14 + offSet, 2, onOff);
matrix.drawPixel(15 + offSet, 2, onOff);
matrix.drawPixel(16 + offSet, 2, onOff);
matrix.drawPixel(17 + offSet, 2, onOff);
matrix.drawPixel(18 + offSet, 2, onOff);
matrix.drawPixel(19 + offSet, 2, onOff);
matrix.drawPixel(20 + offSet, 2, onOff);
matrix.drawPixel(21 + offSet, 2, onOff);
matrix.drawPixel(22 + offSet, 2, onOff);
matrix.drawPixel(23 + offSet, 2, onOff);
matrix.drawPixel(0 + offSet, 3, onOff);
matrix.drawPixel(1 + offSet, 3, onOff);
matrix.drawPixel(2 + offSet, 3, onOff);
matrix.drawPixel(3 + offSet, 3, onOff);
matrix.drawPixel(4 + offSet, 3, onOff);
matrix.drawPixel(19 + offSet, 3, onOff);
matrix.drawPixel(20 + offSet, 3, onOff);
matrix.drawPixel(21 + offSet, 3, onOff);
matrix.drawPixel(22 + offSet, 3, onOff);
matrix.drawPixel(23 + offSet, 3, onOff);
matrix.drawPixel(0 + offSet, 4, onOff);
matrix.drawPixel(1 + offSet, 4, onOff);
matrix.drawPixel(2 + offSet, 4, onOff);
matrix.drawPixel(3 + offSet, 4, onOff);
matrix.drawPixel(4 + offSet, 4, onOff);
matrix.drawPixel(12 + offSet, 4, onOff);
matrix.drawPixel(19 + offSet, 4, onOff);
matrix.drawPixel(20 + offSet, 4, onOff);
matrix.drawPixel(21 + offSet, 4, onOff);
matrix.drawPixel(22 + offSet, 4, onOff);
matrix.drawPixel(23 + offSet, 4, onOff);
matrix.drawPixel(0 + offSet, 5, onOff);
matrix.drawPixel(1 + offSet, 5, onOff);
matrix.drawPixel(2 + offSet, 5, onOff);
matrix.drawPixel(3 + offSet, 5, onOff);
matrix.drawPixel(4 + offSet, 5, onOff);
matrix.drawPixel(11 + offSet, 5, onOff);
matrix.drawPixel(12 + offSet, 5, onOff);
matrix.drawPixel(19 + offSet, 5, onOff);
matrix.drawPixel(20 + offSet, 5, onOff);
matrix.drawPixel(21 + offSet, 5, onOff);
matrix.drawPixel(22 + offSet, 5, onOff);
matrix.drawPixel(23 + offSet, 5, onOff);
matrix.drawPixel(0 + offSet, 6, onOff);
matrix.drawPixel(1 + offSet, 6, onOff);
matrix.drawPixel(2 + offSet, 6, onOff);
matrix.drawPixel(3 + offSet, 6, onOff);
matrix.drawPixel(4 + offSet, 6, onOff);
matrix.drawPixel(9 + offSet, 6, onOff);
matrix.drawPixel(11 + offSet, 6, onOff);
matrix.drawPixel(12 + offSet, 6, onOff);
matrix.drawPixel(13 + offSet, 6, onOff);
matrix.drawPixel(19 + offSet, 6, onOff);
matrix.drawPixel(20 + offSet, 6, onOff);
matrix.drawPixel(21 + offSet, 6, onOff);
matrix.drawPixel(22 + offSet, 6, onOff);
matrix.drawPixel(23 + offSet, 6, onOff);
matrix.drawPixel(0 + offSet, 7, onOff);
matrix.drawPixel(1 + offSet, 7, onOff);
matrix.drawPixel(2 + offSet, 7, onOff);
matrix.drawPixel(3 + offSet, 7, onOff);
matrix.drawPixel(4 + offSet, 7, onOff);
matrix.drawPixel(8 + offSet, 7, onOff);
matrix.drawPixel(9 + offSet, 7, onOff);
matrix.drawPixel(10 + offSet, 7, onOff);
matrix.drawPixel(11 + offSet, 7, onOff);
matrix.drawPixel(12 + offSet, 7, onOff);
matrix.drawPixel(13 + offSet, 7, onOff);
matrix.drawPixel(15 + offSet, 7, onOff);
matrix.drawPixel(19 + offSet, 7, onOff);
matrix.drawPixel(20 + offSet, 7, onOff);
matrix.drawPixel(21 + offSet, 7, onOff);
matrix.drawPixel(22 + offSet, 7, onOff);
matrix.drawPixel(23 + offSet, 7, onOff);
matrix.drawPixel(0 + offSet, 8, onOff);
matrix.drawPixel(1 + offSet, 8, onOff);
matrix.drawPixel(2 + offSet, 8, onOff);
matrix.drawPixel(3 + offSet, 8, onOff);
matrix.drawPixel(4 + offSet, 8, onOff);
matrix.drawPixel(9 + offSet, 8, onOff);
matrix.drawPixel(10 + offSet, 8, onOff);
matrix.drawPixel(11 + offSet, 8, onOff);
matrix.drawPixel(12 + offSet, 8, onOff);
matrix.drawPixel(13 + offSet, 8, onOff);
matrix.drawPixel(14 + offSet, 8, onOff);
matrix.drawPixel(15 + offSet, 8, onOff);
matrix.drawPixel(16 + offSet, 8, onOff);
matrix.drawPixel(19 + offSet, 8, onOff);
matrix.drawPixel(20 + offSet, 8, onOff);
matrix.drawPixel(21 + offSet, 8, onOff);
matrix.drawPixel(22 + offSet, 8, onOff);
matrix.drawPixel(23 + offSet, 8, onOff);
matrix.drawPixel(0 + offSet, 9, onOff);
matrix.drawPixel(1 + offSet, 9, onOff);
matrix.drawPixel(2 + offSet, 9, onOff);
matrix.drawPixel(3 + offSet, 9, onOff);
matrix.drawPixel(4 + offSet, 9, onOff);
matrix.drawPixel(10 + offSet, 9, onOff);
matrix.drawPixel(11 + offSet, 9, onOff);
matrix.drawPixel(12 + offSet, 9, onOff);
matrix.drawPixel(13 + offSet, 9, onOff);
matrix.drawPixel(14 + offSet, 9, onOff);
matrix.drawPixel(15 + offSet, 9, onOff);
matrix.drawPixel(19 + offSet, 9, onOff);
matrix.drawPixel(20 + offSet, 9, onOff);
matrix.drawPixel(21 + offSet, 9, onOff);
matrix.drawPixel(22 + offSet, 9, onOff);
matrix.drawPixel(23 + offSet, 9, onOff);
matrix.drawPixel(0 + offSet, 10, onOff);
matrix.drawPixel(1 + offSet, 10, onOff);
matrix.drawPixel(2 + offSet, 10, onOff);
matrix.drawPixel(3 + offSet, 10, onOff);
matrix.drawPixel(4 + offSet, 10, onOff);
matrix.drawPixel(9 + offSet, 10, onOff);
matrix.drawPixel(10 + offSet, 10, onOff);
matrix.drawPixel(11 + offSet, 10, onOff);
matrix.drawPixel(12 + offSet, 10, onOff);
matrix.drawPixel(13 + offSet, 10, onOff);
matrix.drawPixel(19 + offSet, 10, onOff);
matrix.drawPixel(20 + offSet, 10, onOff);
matrix.drawPixel(21 + offSet, 10, onOff);
matrix.drawPixel(22 + offSet, 10, onOff);
matrix.drawPixel(23 + offSet, 10, onOff);
matrix.drawPixel(0 + offSet, 11, onOff);
matrix.drawPixel(1 + offSet, 11, onOff);
matrix.drawPixel(2 + offSet, 11, onOff);
matrix.drawPixel(3 + offSet, 11, onOff);
matrix.drawPixel(4 + offSet, 11, onOff);
matrix.drawPixel(11 + offSet, 11, onOff);
matrix.drawPixel(13 + offSet, 11, onOff);
matrix.drawPixel(14 + offSet, 11, onOff);
matrix.drawPixel(19 + offSet, 11, onOff);
matrix.drawPixel(20 + offSet, 11, onOff);
matrix.drawPixel(21 + offSet, 11, onOff);
matrix.drawPixel(22 + offSet, 11, onOff);
matrix.drawPixel(23 + offSet, 11, onOff);
matrix.drawPixel(0 + offSet, 12, onOff);
matrix.drawPixel(1 + offSet, 12, onOff);
matrix.drawPixel(2 + offSet, 12, onOff);
matrix.drawPixel(3 + offSet, 12, onOff);
matrix.drawPixel(4 + offSet, 12, onOff);
matrix.drawPixel(11 + offSet, 12, onOff);
matrix.drawPixel(19 + offSet, 12, onOff);
matrix.drawPixel(20 + offSet, 12, onOff);
matrix.drawPixel(21 + offSet, 12, onOff);
matrix.drawPixel(22 + offSet, 12, onOff);
matrix.drawPixel(23 + offSet, 12, onOff);
matrix.drawPixel(0 + offSet, 13, onOff);
matrix.drawPixel(1 + offSet, 13, onOff);
matrix.drawPixel(2 + offSet, 13, onOff);
matrix.drawPixel(3 + offSet, 13, onOff);
matrix.drawPixel(4 + offSet, 13, onOff);
matrix.drawPixel(5 + offSet, 13, onOff);
matrix.drawPixel(6 + offSet, 13, onOff);
matrix.drawPixel(7 + offSet, 13, onOff);
matrix.drawPixel(8 + offSet, 13, onOff);
matrix.drawPixel(9 + offSet, 13, onOff);
matrix.drawPixel(10 + offSet, 13, onOff);
matrix.drawPixel(19 + offSet, 13, onOff);
matrix.drawPixel(20 + offSet, 13, onOff);
matrix.drawPixel(21 + offSet, 13, onOff);
matrix.drawPixel(22 + offSet, 13, onOff);
matrix.drawPixel(23 + offSet, 13, onOff);
matrix.drawPixel(0 + offSet, 14, onOff);
matrix.drawPixel(1 + offSet, 14, onOff);
matrix.drawPixel(2 + offSet, 14, onOff);
matrix.drawPixel(3 + offSet, 14, onOff);
matrix.drawPixel(11 + offSet, 14, onOff);
matrix.drawPixel(12 + offSet, 14, onOff);
matrix.drawPixel(13 + offSet, 14, onOff);
matrix.drawPixel(19 + offSet, 14, onOff);
matrix.drawPixel(20 + offSet, 14, onOff);
matrix.drawPixel(21 + offSet, 14, onOff);
matrix.drawPixel(22 + offSet, 14, onOff);
matrix.drawPixel(0 + offSet, 15, onOff);
matrix.drawPixel(1 + offSet, 15, onOff);
matrix.drawPixel(14 + offSet, 15, onOff);
matrix.drawPixel(15 + offSet, 15, onOff);
matrix.drawPixel(16 + offSet, 15, onOff);
matrix.drawPixel(17 + offSet, 15, onOff);
matrix.drawPixel(18 + offSet, 15, onOff);
matrix.drawPixel(19 + offSet, 15, onOff);
matrix.drawPixel(20 + offSet, 15, onOff);

 matrix.writeScreen();
}

void flagTwo(int offSet, int onOff)
{
matrix.drawPixel(0 + offSet, 0, onOff);
matrix.drawPixel(1 + offSet, 0, onOff);
matrix.drawPixel(14 + offSet, 0, onOff);
matrix.drawPixel(15 + offSet, 0, onOff);
matrix.drawPixel(16 + offSet, 0, onOff);
matrix.drawPixel(17 + offSet, 0, onOff);
matrix.drawPixel(18 + offSet, 0, onOff);
matrix.drawPixel(19 + offSet, 0, onOff);
matrix.drawPixel(20 + offSet, 0, onOff);
matrix.drawPixel(0 + offSet, 1, onOff);
matrix.drawPixel(1 + offSet, 1, onOff);
matrix.drawPixel(2 + offSet, 1, onOff);
matrix.drawPixel(3 + offSet, 1, onOff);
matrix.drawPixel(11 + offSet, 1, onOff);
matrix.drawPixel(12 + offSet, 1, onOff);
matrix.drawPixel(13 + offSet, 1, onOff);
matrix.drawPixel(19 + offSet, 1, onOff);
matrix.drawPixel(20 + offSet, 1, onOff);
matrix.drawPixel(21 + offSet, 1, onOff);
matrix.drawPixel(22 + offSet, 1, onOff);
matrix.drawPixel(0 + offSet, 2, onOff);
matrix.drawPixel(1 + offSet, 2, onOff);
matrix.drawPixel(2 + offSet, 2, onOff);
matrix.drawPixel(3 + offSet, 2, onOff);
matrix.drawPixel(4 + offSet, 2, onOff);
matrix.drawPixel(5 + offSet, 2, onOff);
matrix.drawPixel(6 + offSet, 2, onOff);
matrix.drawPixel(7 + offSet, 2, onOff);
matrix.drawPixel(8 + offSet, 2, onOff);
matrix.drawPixel(9 + offSet, 2, onOff);
matrix.drawPixel(10 + offSet, 2, onOff);
matrix.drawPixel(19 + offSet, 2, onOff);
matrix.drawPixel(20 + offSet, 2, onOff);
matrix.drawPixel(21 + offSet, 2, onOff);
matrix.drawPixel(22 + offSet, 2, onOff);
matrix.drawPixel(23 + offSet, 2, onOff);
matrix.drawPixel(0 + offSet, 3, onOff);
matrix.drawPixel(1 + offSet, 3, onOff);
matrix.drawPixel(2 + offSet, 3, onOff);
matrix.drawPixel(3 + offSet, 3, onOff);
matrix.drawPixel(4 + offSet, 3, onOff);
matrix.drawPixel(19 + offSet, 3, onOff);
matrix.drawPixel(20 + offSet, 3, onOff);
matrix.drawPixel(21 + offSet, 3, onOff);
matrix.drawPixel(22 + offSet, 3, onOff);
matrix.drawPixel(23 + offSet, 3, onOff);
matrix.drawPixel(0 + offSet, 4, onOff);
matrix.drawPixel(1 + offSet, 4, onOff);
matrix.drawPixel(2 + offSet, 4, onOff);
matrix.drawPixel(3 + offSet, 4, onOff);
matrix.drawPixel(4 + offSet, 4, onOff);
matrix.drawPixel(11 + offSet, 4, onOff);
matrix.drawPixel(19 + offSet, 4, onOff);
matrix.drawPixel(20 + offSet, 4, onOff);
matrix.drawPixel(21 + offSet, 4, onOff);
matrix.drawPixel(22 + offSet, 4, onOff);
matrix.drawPixel(23 + offSet, 4, onOff);
matrix.drawPixel(0 + offSet, 5, onOff);
matrix.drawPixel(1 + offSet, 5, onOff);
matrix.drawPixel(2 + offSet, 5, onOff);
matrix.drawPixel(3 + offSet, 5, onOff);
matrix.drawPixel(4 + offSet, 5, onOff);
matrix.drawPixel(11 + offSet, 5, onOff);
matrix.drawPixel(12 + offSet, 5, onOff);
matrix.drawPixel(19 + offSet, 5, onOff);
matrix.drawPixel(20 + offSet, 5, onOff);
matrix.drawPixel(21 + offSet, 5, onOff);
matrix.drawPixel(22 + offSet, 5, onOff);
matrix.drawPixel(23 + offSet, 5, onOff);
matrix.drawPixel(0 + offSet, 6, onOff);
matrix.drawPixel(1 + offSet, 6, onOff);
matrix.drawPixel(2 + offSet, 6, onOff);
matrix.drawPixel(3 + offSet, 6, onOff);
matrix.drawPixel(4 + offSet, 6, onOff);
matrix.drawPixel(10 + offSet, 6, onOff);
matrix.drawPixel(11 + offSet, 6, onOff);
matrix.drawPixel(12 + offSet, 6, onOff);
matrix.drawPixel(14 + offSet, 6, onOff);
matrix.drawPixel(19 + offSet, 6, onOff);
matrix.drawPixel(20 + offSet, 6, onOff);
matrix.drawPixel(21 + offSet, 6, onOff);
matrix.drawPixel(22 + offSet, 6, onOff);
matrix.drawPixel(23 + offSet, 6, onOff);
matrix.drawPixel(0 + offSet, 7, onOff);
matrix.drawPixel(1 + offSet, 7, onOff);
matrix.drawPixel(2 + offSet, 7, onOff);
matrix.drawPixel(3 + offSet, 7, onOff);
matrix.drawPixel(4 + offSet, 7, onOff);
matrix.drawPixel(8 + offSet, 7, onOff);
matrix.drawPixel(10 + offSet, 7, onOff);
matrix.drawPixel(11 + offSet, 7, onOff);
matrix.drawPixel(12 + offSet, 7, onOff);
matrix.drawPixel(13 + offSet, 7, onOff);
matrix.drawPixel(14 + offSet, 7, onOff);
matrix.drawPixel(15 + offSet, 7, onOff);
matrix.drawPixel(19 + offSet, 7, onOff);
matrix.drawPixel(20 + offSet, 7, onOff);
matrix.drawPixel(21 + offSet, 7, onOff);
matrix.drawPixel(22 + offSet, 7, onOff);
matrix.drawPixel(23 + offSet, 7, onOff);
matrix.drawPixel(0 + offSet, 8, onOff);
matrix.drawPixel(1 + offSet, 8, onOff);
matrix.drawPixel(2 + offSet, 8, onOff);
matrix.drawPixel(3 + offSet, 8, onOff);
matrix.drawPixel(4 + offSet, 8, onOff);
matrix.drawPixel(7 + offSet, 8, onOff);
matrix.drawPixel(8 + offSet, 8, onOff);
matrix.drawPixel(9 + offSet, 8, onOff);
matrix.drawPixel(10 + offSet, 8, onOff);
matrix.drawPixel(11 + offSet, 8, onOff);
matrix.drawPixel(12 + offSet, 8, onOff);
matrix.drawPixel(13 + offSet, 8, onOff);
matrix.drawPixel(14 + offSet, 8, onOff);
matrix.drawPixel(19 + offSet, 8, onOff);
matrix.drawPixel(20 + offSet, 8, onOff);
matrix.drawPixel(21 + offSet, 8, onOff);
matrix.drawPixel(22 + offSet, 8, onOff);
matrix.drawPixel(23 + offSet, 8, onOff);
matrix.drawPixel(0 + offSet, 9, onOff);
matrix.drawPixel(1 + offSet, 9, onOff);
matrix.drawPixel(2 + offSet, 9, onOff);
matrix.drawPixel(3 + offSet, 9, onOff);
matrix.drawPixel(4 + offSet, 9, onOff);
matrix.drawPixel(8 + offSet, 9, onOff);
matrix.drawPixel(9 + offSet, 9, onOff);
matrix.drawPixel(10 + offSet, 9, onOff);
matrix.drawPixel(11 + offSet, 9, onOff);
matrix.drawPixel(12 + offSet, 9, onOff);
matrix.drawPixel(13 + offSet, 9, onOff);
matrix.drawPixel(19 + offSet, 9, onOff);
matrix.drawPixel(20 + offSet, 9, onOff);
matrix.drawPixel(21 + offSet, 9, onOff);
matrix.drawPixel(22 + offSet, 9, onOff);
matrix.drawPixel(23 + offSet, 9, onOff);
matrix.drawPixel(0 + offSet, 10, onOff);
matrix.drawPixel(1 + offSet, 10, onOff);
matrix.drawPixel(2 + offSet, 10, onOff);
matrix.drawPixel(3 + offSet, 10, onOff);
matrix.drawPixel(4 + offSet, 10, onOff);
matrix.drawPixel(10 + offSet, 10, onOff);
matrix.drawPixel(11 + offSet, 10, onOff);
matrix.drawPixel(12 + offSet, 10, onOff);
matrix.drawPixel(13 + offSet, 10, onOff);
matrix.drawPixel(14 + offSet, 10, onOff);
matrix.drawPixel(19 + offSet, 10, onOff);
matrix.drawPixel(20 + offSet, 10, onOff);
matrix.drawPixel(21 + offSet, 10, onOff);
matrix.drawPixel(22 + offSet, 10, onOff);
matrix.drawPixel(23 + offSet, 10, onOff);
matrix.drawPixel(0 + offSet, 11, onOff);
matrix.drawPixel(1 + offSet, 11, onOff);
matrix.drawPixel(2 + offSet, 11, onOff);
matrix.drawPixel(3 + offSet, 11, onOff);
matrix.drawPixel(4 + offSet, 11, onOff);
matrix.drawPixel(9 + offSet, 11, onOff);
matrix.drawPixel(10 + offSet, 11, onOff);
matrix.drawPixel(11 + offSet, 11, onOff);
matrix.drawPixel(19 + offSet, 11, onOff);
matrix.drawPixel(20 + offSet, 11, onOff);
matrix.drawPixel(21 + offSet, 11, onOff);
matrix.drawPixel(22 + offSet, 11, onOff);
matrix.drawPixel(23 + offSet, 11, onOff);
matrix.drawPixel(0 + offSet, 12, onOff);
matrix.drawPixel(1 + offSet, 12, onOff);
matrix.drawPixel(2 + offSet, 12, onOff);
matrix.drawPixel(3 + offSet, 12, onOff);
matrix.drawPixel(4 + offSet, 12, onOff);
matrix.drawPixel(11 + offSet, 12, onOff);
matrix.drawPixel(19 + offSet, 12, onOff);
matrix.drawPixel(20 + offSet, 12, onOff);
matrix.drawPixel(21 + offSet, 12, onOff);
matrix.drawPixel(22 + offSet, 12, onOff);
matrix.drawPixel(23 + offSet, 12, onOff);
matrix.drawPixel(0 + offSet, 13, onOff);
matrix.drawPixel(1 + offSet, 13, onOff);
matrix.drawPixel(2 + offSet, 13, onOff);
matrix.drawPixel(3 + offSet, 13, onOff);
matrix.drawPixel(4 + offSet, 13, onOff);
matrix.drawPixel(13 + offSet, 13, onOff);
matrix.drawPixel(14 + offSet, 13, onOff);
matrix.drawPixel(15 + offSet, 13, onOff);
matrix.drawPixel(16 + offSet, 13, onOff);
matrix.drawPixel(17 + offSet, 13, onOff);
matrix.drawPixel(18 + offSet, 13, onOff);
matrix.drawPixel(19 + offSet, 13, onOff);
matrix.drawPixel(20 + offSet, 13, onOff);
matrix.drawPixel(21 + offSet, 13, onOff);
matrix.drawPixel(22 + offSet, 13, onOff);
matrix.drawPixel(23 + offSet, 13, onOff);
matrix.drawPixel(1 + offSet, 14, onOff);
matrix.drawPixel(2 + offSet, 14, onOff);
matrix.drawPixel(3 + offSet, 14, onOff);
matrix.drawPixel(4 + offSet, 14, onOff);
matrix.drawPixel(10 + offSet, 14, onOff);
matrix.drawPixel(11 + offSet, 14, onOff);
matrix.drawPixel(12 + offSet, 14, onOff);
matrix.drawPixel(20 + offSet, 14, onOff);
matrix.drawPixel(21 + offSet, 14, onOff);
matrix.drawPixel(22 + offSet, 14, onOff);
matrix.drawPixel(23 + offSet, 14, onOff);
matrix.drawPixel(3 + offSet, 15, onOff);
matrix.drawPixel(4 + offSet, 15, onOff);
matrix.drawPixel(5 + offSet, 15, onOff);
matrix.drawPixel(6 + offSet, 15, onOff);
matrix.drawPixel(7 + offSet, 15, onOff);
matrix.drawPixel(8 + offSet, 15, onOff);
matrix.drawPixel(9 + offSet, 15, onOff);
matrix.drawPixel(22 + offSet, 15, onOff);
matrix.drawPixel(23 + offSet, 15, onOff);

 matrix.writeScreen();
}

void heartOne(int offSet, int onOff)
{
matrix.drawPixel(1 + offSet, 1, onOff);
matrix.drawPixel(22 + offSet, 1, onOff);
matrix.drawPixel(2 + offSet, 2, onOff);
matrix.drawPixel(6 + offSet, 2, onOff);
matrix.drawPixel(7 + offSet, 2, onOff);
matrix.drawPixel(8 + offSet, 2, onOff);
matrix.drawPixel(9 + offSet, 2, onOff);
matrix.drawPixel(14 + offSet, 2, onOff);
matrix.drawPixel(15 + offSet, 2, onOff);
matrix.drawPixel(16 + offSet, 2, onOff);
matrix.drawPixel(17 + offSet, 2, onOff);
matrix.drawPixel(21 + offSet, 2, onOff);
matrix.drawPixel(5 + offSet, 3, onOff);
matrix.drawPixel(6 + offSet, 3, onOff);
matrix.drawPixel(7 + offSet, 3, onOff);
matrix.drawPixel(8 + offSet, 3, onOff);
matrix.drawPixel(9 + offSet, 3, onOff);
matrix.drawPixel(10 + offSet, 3, onOff);
matrix.drawPixel(13 + offSet, 3, onOff);
matrix.drawPixel(14 + offSet, 3, onOff);
matrix.drawPixel(15 + offSet, 3, onOff);
matrix.drawPixel(16 + offSet, 3, onOff);
matrix.drawPixel(17 + offSet, 3, onOff);
matrix.drawPixel(18 + offSet, 3, onOff);
matrix.drawPixel(4 + offSet, 4, onOff);
matrix.drawPixel(5 + offSet, 4, onOff);
matrix.drawPixel(6 + offSet, 4, onOff);
matrix.drawPixel(7 + offSet, 4, onOff);
matrix.drawPixel(8 + offSet, 4, onOff);
matrix.drawPixel(9 + offSet, 4, onOff);
matrix.drawPixel(10 + offSet, 4, onOff);
matrix.drawPixel(11 + offSet, 4, onOff);
matrix.drawPixel(12 + offSet, 4, onOff);
matrix.drawPixel(13 + offSet, 4, onOff);
matrix.drawPixel(14 + offSet, 4, onOff);
matrix.drawPixel(15 + offSet, 4, onOff);
matrix.drawPixel(16 + offSet, 4, onOff);
matrix.drawPixel(17 + offSet, 4, onOff);
matrix.drawPixel(18 + offSet, 4, onOff);
matrix.drawPixel(19 + offSet, 4, onOff);
matrix.drawPixel(4 + offSet, 5, onOff);
matrix.drawPixel(5 + offSet, 5, onOff);
matrix.drawPixel(6 + offSet, 5, onOff);
matrix.drawPixel(7 + offSet, 5, onOff);
matrix.drawPixel(8 + offSet, 5, onOff);
matrix.drawPixel(9 + offSet, 5, onOff);
matrix.drawPixel(10 + offSet, 5, onOff);
matrix.drawPixel(11 + offSet, 5, onOff);
matrix.drawPixel(12 + offSet, 5, onOff);
matrix.drawPixel(13 + offSet, 5, onOff);
matrix.drawPixel(14 + offSet, 5, onOff);
matrix.drawPixel(15 + offSet, 5, onOff);
matrix.drawPixel(16 + offSet, 5, onOff);
matrix.drawPixel(17 + offSet, 5, onOff);
matrix.drawPixel(18 + offSet, 5, onOff);
matrix.drawPixel(19 + offSet, 5, onOff);
matrix.drawPixel(4 + offSet, 6, onOff);
matrix.drawPixel(5 + offSet, 6, onOff);
matrix.drawPixel(6 + offSet, 6, onOff);
matrix.drawPixel(7 + offSet, 6, onOff);
matrix.drawPixel(8 + offSet, 6, onOff);
matrix.drawPixel(9 + offSet, 6, onOff);
matrix.drawPixel(10 + offSet, 6, onOff);
matrix.drawPixel(11 + offSet, 6, onOff);
matrix.drawPixel(12 + offSet, 6, onOff);
matrix.drawPixel(13 + offSet, 6, onOff);
matrix.drawPixel(14 + offSet, 6, onOff);
matrix.drawPixel(15 + offSet, 6, onOff);
matrix.drawPixel(16 + offSet, 6, onOff);
matrix.drawPixel(17 + offSet, 6, onOff);
matrix.drawPixel(18 + offSet, 6, onOff);
matrix.drawPixel(19 + offSet, 6, onOff);
matrix.drawPixel(0 + offSet, 7, onOff);
matrix.drawPixel(1 + offSet, 7, onOff);
matrix.drawPixel(4 + offSet, 7, onOff);
matrix.drawPixel(5 + offSet, 7, onOff);
matrix.drawPixel(6 + offSet, 7, onOff);
matrix.drawPixel(7 + offSet, 7, onOff);
matrix.drawPixel(8 + offSet, 7, onOff);
matrix.drawPixel(9 + offSet, 7, onOff);
matrix.drawPixel(10 + offSet, 7, onOff);
matrix.drawPixel(11 + offSet, 7, onOff);
matrix.drawPixel(12 + offSet, 7, onOff);
matrix.drawPixel(13 + offSet, 7, onOff);
matrix.drawPixel(14 + offSet, 7, onOff);
matrix.drawPixel(15 + offSet, 7, onOff);
matrix.drawPixel(16 + offSet, 7, onOff);
matrix.drawPixel(17 + offSet, 7, onOff);
matrix.drawPixel(18 + offSet, 7, onOff);
matrix.drawPixel(19 + offSet, 7, onOff);
matrix.drawPixel(22 + offSet, 7, onOff);
matrix.drawPixel(23 + offSet, 7, onOff);
matrix.drawPixel(5 + offSet, 8, onOff);
matrix.drawPixel(6 + offSet, 8, onOff);
matrix.drawPixel(7 + offSet, 8, onOff);
matrix.drawPixel(8 + offSet, 8, onOff);
matrix.drawPixel(9 + offSet, 8, onOff);
matrix.drawPixel(10 + offSet, 8, onOff);
matrix.drawPixel(11 + offSet, 8, onOff);
matrix.drawPixel(12 + offSet, 8, onOff);
matrix.drawPixel(13 + offSet, 8, onOff);
matrix.drawPixel(14 + offSet, 8, onOff);
matrix.drawPixel(15 + offSet, 8, onOff);
matrix.drawPixel(16 + offSet, 8, onOff);
matrix.drawPixel(17 + offSet, 8, onOff);
matrix.drawPixel(18 + offSet, 8, onOff);
matrix.drawPixel(6 + offSet, 9, onOff);
matrix.drawPixel(7 + offSet, 9, onOff);
matrix.drawPixel(8 + offSet, 9, onOff);
matrix.drawPixel(9 + offSet, 9, onOff);
matrix.drawPixel(10 + offSet, 9, onOff);
matrix.drawPixel(11 + offSet, 9, onOff);
matrix.drawPixel(12 + offSet, 9, onOff);
matrix.drawPixel(13 + offSet, 9, onOff);
matrix.drawPixel(14 + offSet, 9, onOff);
matrix.drawPixel(15 + offSet, 9, onOff);
matrix.drawPixel(16 + offSet, 9, onOff);
matrix.drawPixel(17 + offSet, 9, onOff);
matrix.drawPixel(7 + offSet, 10, onOff);
matrix.drawPixel(8 + offSet, 10, onOff);
matrix.drawPixel(9 + offSet, 10, onOff);
matrix.drawPixel(10 + offSet, 10, onOff);
matrix.drawPixel(11 + offSet, 10, onOff);
matrix.drawPixel(12 + offSet, 10, onOff);
matrix.drawPixel(13 + offSet, 10, onOff);
matrix.drawPixel(14 + offSet, 10, onOff);
matrix.drawPixel(15 + offSet, 10, onOff);
matrix.drawPixel(16 + offSet, 10, onOff);
matrix.drawPixel(8 + offSet, 11, onOff);
matrix.drawPixel(9 + offSet, 11, onOff);
matrix.drawPixel(10 + offSet, 11, onOff);
matrix.drawPixel(11 + offSet, 11, onOff);
matrix.drawPixel(12 + offSet, 11, onOff);
matrix.drawPixel(13 + offSet, 11, onOff);
matrix.drawPixel(14 + offSet, 11, onOff);
matrix.drawPixel(15 + offSet, 11, onOff);
matrix.drawPixel(3 + offSet, 12, onOff);
matrix.drawPixel(9 + offSet, 12, onOff);
matrix.drawPixel(10 + offSet, 12, onOff);
matrix.drawPixel(11 + offSet, 12, onOff);
matrix.drawPixel(12 + offSet, 12, onOff);
matrix.drawPixel(13 + offSet, 12, onOff);
matrix.drawPixel(14 + offSet, 12, onOff);
matrix.drawPixel(20 + offSet, 12, onOff);
matrix.drawPixel(2 + offSet, 13, onOff);
matrix.drawPixel(10 + offSet, 13, onOff);
matrix.drawPixel(11 + offSet, 13, onOff);
matrix.drawPixel(12 + offSet, 13, onOff);
matrix.drawPixel(13 + offSet, 13, onOff);
matrix.drawPixel(21 + offSet, 13, onOff);
matrix.drawPixel(11 + offSet, 14, onOff);
matrix.drawPixel(12 + offSet, 14, onOff);

 matrix.writeScreen();
}

void heartTwo(int offSet, int onOff)
{
matrix.drawPixel(2 + offSet, 2, onOff);
matrix.drawPixel(21 + offSet, 2, onOff);
matrix.drawPixel(3 + offSet, 3, onOff);
matrix.drawPixel(6 + offSet, 3, onOff);
matrix.drawPixel(7 + offSet, 3, onOff);
matrix.drawPixel(8 + offSet, 3, onOff);
matrix.drawPixel(9 + offSet, 3, onOff);
matrix.drawPixel(14 + offSet, 3, onOff);
matrix.drawPixel(15 + offSet, 3, onOff);
matrix.drawPixel(16 + offSet, 3, onOff);
matrix.drawPixel(17 + offSet, 3, onOff);
matrix.drawPixel(20 + offSet, 3, onOff);
matrix.drawPixel(5 + offSet, 4, onOff);
matrix.drawPixel(6 + offSet, 4, onOff);
matrix.drawPixel(7 + offSet, 4, onOff);
matrix.drawPixel(8 + offSet, 4, onOff);
matrix.drawPixel(9 + offSet, 4, onOff);
matrix.drawPixel(10 + offSet, 4, onOff);
matrix.drawPixel(13 + offSet, 4, onOff);
matrix.drawPixel(14 + offSet, 4, onOff);
matrix.drawPixel(15 + offSet, 4, onOff);
matrix.drawPixel(16 + offSet, 4, onOff);
matrix.drawPixel(17 + offSet, 4, onOff);
matrix.drawPixel(18 + offSet, 4, onOff);
matrix.drawPixel(5 + offSet, 5, onOff);
matrix.drawPixel(6 + offSet, 5, onOff);
matrix.drawPixel(7 + offSet, 5, onOff);
matrix.drawPixel(8 + offSet, 5, onOff);
matrix.drawPixel(9 + offSet, 5, onOff);
matrix.drawPixel(10 + offSet, 5, onOff);
matrix.drawPixel(11 + offSet, 5, onOff);
matrix.drawPixel(12 + offSet, 5, onOff);
matrix.drawPixel(13 + offSet, 5, onOff);
matrix.drawPixel(14 + offSet, 5, onOff);
matrix.drawPixel(15 + offSet, 5, onOff);
matrix.drawPixel(16 + offSet, 5, onOff);
matrix.drawPixel(17 + offSet, 5, onOff);
matrix.drawPixel(18 + offSet, 5, onOff);
matrix.drawPixel(5 + offSet, 6, onOff);
matrix.drawPixel(6 + offSet, 6, onOff);
matrix.drawPixel(7 + offSet, 6, onOff);
matrix.drawPixel(8 + offSet, 6, onOff);
matrix.drawPixel(9 + offSet, 6, onOff);
matrix.drawPixel(10 + offSet, 6, onOff);
matrix.drawPixel(11 + offSet, 6, onOff);
matrix.drawPixel(12 + offSet, 6, onOff);
matrix.drawPixel(13 + offSet, 6, onOff);
matrix.drawPixel(14 + offSet, 6, onOff);
matrix.drawPixel(15 + offSet, 6, onOff);
matrix.drawPixel(16 + offSet, 6, onOff);
matrix.drawPixel(17 + offSet, 6, onOff);
matrix.drawPixel(18 + offSet, 6, onOff);
matrix.drawPixel(1 + offSet, 7, onOff);
matrix.drawPixel(2 + offSet, 7, onOff);
matrix.drawPixel(5 + offSet, 7, onOff);
matrix.drawPixel(6 + offSet, 7, onOff);
matrix.drawPixel(7 + offSet, 7, onOff);
matrix.drawPixel(8 + offSet, 7, onOff);
matrix.drawPixel(9 + offSet, 7, onOff);
matrix.drawPixel(10 + offSet, 7, onOff);
matrix.drawPixel(11 + offSet, 7, onOff);
matrix.drawPixel(12 + offSet, 7, onOff);
matrix.drawPixel(13 + offSet, 7, onOff);
matrix.drawPixel(14 + offSet, 7, onOff);
matrix.drawPixel(15 + offSet, 7, onOff);
matrix.drawPixel(16 + offSet, 7, onOff);
matrix.drawPixel(17 + offSet, 7, onOff);
matrix.drawPixel(18 + offSet, 7, onOff);
matrix.drawPixel(21 + offSet, 7, onOff);
matrix.drawPixel(22 + offSet, 7, onOff);
matrix.drawPixel(6 + offSet, 8, onOff);
matrix.drawPixel(7 + offSet, 8, onOff);
matrix.drawPixel(8 + offSet, 8, onOff);
matrix.drawPixel(9 + offSet, 8, onOff);
matrix.drawPixel(10 + offSet, 8, onOff);
matrix.drawPixel(11 + offSet, 8, onOff);
matrix.drawPixel(12 + offSet, 8, onOff);
matrix.drawPixel(13 + offSet, 8, onOff);
matrix.drawPixel(14 + offSet, 8, onOff);
matrix.drawPixel(15 + offSet, 8, onOff);
matrix.drawPixel(16 + offSet, 8, onOff);
matrix.drawPixel(17 + offSet, 8, onOff);
matrix.drawPixel(7 + offSet, 9, onOff);
matrix.drawPixel(8 + offSet, 9, onOff);
matrix.drawPixel(9 + offSet, 9, onOff);
matrix.drawPixel(10 + offSet, 9, onOff);
matrix.drawPixel(11 + offSet, 9, onOff);
matrix.drawPixel(12 + offSet, 9, onOff);
matrix.drawPixel(13 + offSet, 9, onOff);
matrix.drawPixel(14 + offSet, 9, onOff);
matrix.drawPixel(15 + offSet, 9, onOff);
matrix.drawPixel(16 + offSet, 9, onOff);
matrix.drawPixel(4 + offSet, 10, onOff);
matrix.drawPixel(8 + offSet, 10, onOff);
matrix.drawPixel(9 + offSet, 10, onOff);
matrix.drawPixel(10 + offSet, 10, onOff);
matrix.drawPixel(11 + offSet, 10, onOff);
matrix.drawPixel(12 + offSet, 10, onOff);
matrix.drawPixel(13 + offSet, 10, onOff);
matrix.drawPixel(14 + offSet, 10, onOff);
matrix.drawPixel(15 + offSet, 10, onOff);
matrix.drawPixel(19 + offSet, 10, onOff);
matrix.drawPixel(3 + offSet, 11, onOff);
matrix.drawPixel(9 + offSet, 11, onOff);
matrix.drawPixel(10 + offSet, 11, onOff);
matrix.drawPixel(11 + offSet, 11, onOff);
matrix.drawPixel(12 + offSet, 11, onOff);
matrix.drawPixel(13 + offSet, 11, onOff);
matrix.drawPixel(14 + offSet, 11, onOff);
matrix.drawPixel(20 + offSet, 11, onOff);
matrix.drawPixel(10 + offSet, 12, onOff);
matrix.drawPixel(11 + offSet, 12, onOff);
matrix.drawPixel(12 + offSet, 12, onOff);
matrix.drawPixel(13 + offSet, 12, onOff);
matrix.drawPixel(11 + offSet, 13, onOff);
matrix.drawPixel(12 + offSet, 13, onOff);

 matrix.writeScreen();
}

void handOne(int offSet, int onOff)
{
matrix.drawPixel(8 + offSet, 4, onOff);
matrix.drawPixel(9 + offSet, 4, onOff);
matrix.drawPixel(10 + offSet, 4, onOff);
matrix.drawPixel(11 + offSet, 4, onOff);
matrix.drawPixel(12 + offSet, 4, onOff);
matrix.drawPixel(13 + offSet, 4, onOff);
matrix.drawPixel(7 + offSet, 5, onOff);
matrix.drawPixel(14 + offSet, 5, onOff);
matrix.drawPixel(3 + offSet, 6, onOff);
matrix.drawPixel(4 + offSet, 6, onOff);
matrix.drawPixel(5 + offSet, 6, onOff);
matrix.drawPixel(6 + offSet, 6, onOff);
matrix.drawPixel(7 + offSet, 6, onOff);
matrix.drawPixel(8 + offSet, 6, onOff);
matrix.drawPixel(14 + offSet, 6, onOff);
matrix.drawPixel(2 + offSet, 7, onOff);
matrix.drawPixel(14 + offSet, 7, onOff);
matrix.drawPixel(17 + offSet, 7, onOff);
matrix.drawPixel(18 + offSet, 7, onOff);
matrix.drawPixel(19 + offSet, 7, onOff);
matrix.drawPixel(20 + offSet, 7, onOff);
matrix.drawPixel(2 + offSet, 8, onOff);
matrix.drawPixel(15 + offSet, 8, onOff);
matrix.drawPixel(16 + offSet, 8, onOff);
matrix.drawPixel(19 + offSet, 8, onOff);
matrix.drawPixel(21 + offSet, 8, onOff);
matrix.drawPixel(3 + offSet, 9, onOff);
matrix.drawPixel(4 + offSet, 9, onOff);
matrix.drawPixel(21 + offSet, 9, onOff);
matrix.drawPixel(3 + offSet, 10, onOff);
matrix.drawPixel(21 + offSet, 10, onOff);
matrix.drawPixel(3 + offSet, 11, onOff);
matrix.drawPixel(21 + offSet, 11, onOff);
matrix.drawPixel(3 + offSet, 12, onOff);
matrix.drawPixel(21 + offSet, 12, onOff);
matrix.drawPixel(4 + offSet, 13, onOff);
matrix.drawPixel(21 + offSet, 13, onOff);
matrix.drawPixel(5 + offSet, 14, onOff);
matrix.drawPixel(6 + offSet, 14, onOff);
matrix.drawPixel(7 + offSet, 14, onOff);
matrix.drawPixel(8 + offSet, 14, onOff);
matrix.drawPixel(9 + offSet, 14, onOff);
matrix.drawPixel(10 + offSet, 14, onOff);
matrix.drawPixel(11 + offSet, 14, onOff);
matrix.drawPixel(12 + offSet, 14, onOff);
matrix.drawPixel(13 + offSet, 14, onOff);
matrix.drawPixel(14 + offSet, 14, onOff);
matrix.drawPixel(15 + offSet, 14, onOff);
matrix.drawPixel(20 + offSet, 14, onOff);
matrix.drawPixel(16 + offSet, 15, onOff);
matrix.drawPixel(17 + offSet, 15, onOff);
matrix.drawPixel(18 + offSet, 15, onOff);
matrix.drawPixel(19 + offSet, 15, onOff);

 matrix.writeScreen();
}

void handTwo(int offSet, int onOff)
{
matrix.drawPixel(6 + offSet, 0, onOff);
matrix.drawPixel(16 + offSet, 0, onOff);
matrix.drawPixel(7 + offSet, 1, onOff);
matrix.drawPixel(11 + offSet, 1, onOff);
matrix.drawPixel(12 + offSet, 1, onOff);
matrix.drawPixel(15 + offSet, 1, onOff);
matrix.drawPixel(10 + offSet, 2, onOff);
matrix.drawPixel(12 + offSet, 2, onOff);
matrix.drawPixel(5 + offSet, 3, onOff);
matrix.drawPixel(6 + offSet, 3, onOff);
matrix.drawPixel(9 + offSet, 3, onOff);
matrix.drawPixel(12 + offSet, 3, onOff);
matrix.drawPixel(16 + offSet, 3, onOff);
matrix.drawPixel(17 + offSet, 3, onOff);
matrix.drawPixel(9 + offSet, 4, onOff);
matrix.drawPixel(13 + offSet, 4, onOff);
matrix.drawPixel(9 + offSet, 5, onOff);
matrix.drawPixel(14 + offSet, 5, onOff);
matrix.drawPixel(3 + offSet, 6, onOff);
matrix.drawPixel(4 + offSet, 6, onOff);
matrix.drawPixel(5 + offSet, 6, onOff);
matrix.drawPixel(6 + offSet, 6, onOff);
matrix.drawPixel(7 + offSet, 6, onOff);
matrix.drawPixel(8 + offSet, 6, onOff);
matrix.drawPixel(14 + offSet, 6, onOff);
matrix.drawPixel(2 + offSet, 7, onOff);
matrix.drawPixel(14 + offSet, 7, onOff);
matrix.drawPixel(17 + offSet, 7, onOff);
matrix.drawPixel(18 + offSet, 7, onOff);
matrix.drawPixel(19 + offSet, 7, onOff);
matrix.drawPixel(20 + offSet, 7, onOff);
matrix.drawPixel(2 + offSet, 8, onOff);
matrix.drawPixel(15 + offSet, 8, onOff);
matrix.drawPixel(16 + offSet, 8, onOff);
matrix.drawPixel(19 + offSet, 8, onOff);
matrix.drawPixel(21 + offSet, 8, onOff);
matrix.drawPixel(3 + offSet, 9, onOff);
matrix.drawPixel(4 + offSet, 9, onOff);
matrix.drawPixel(21 + offSet, 9, onOff);
matrix.drawPixel(3 + offSet, 10, onOff);
matrix.drawPixel(21 + offSet, 10, onOff);
matrix.drawPixel(3 + offSet, 11, onOff);
matrix.drawPixel(21 + offSet, 11, onOff);
matrix.drawPixel(3 + offSet, 12, onOff);
matrix.drawPixel(21 + offSet, 12, onOff);
matrix.drawPixel(4 + offSet, 13, onOff);
matrix.drawPixel(21 + offSet, 13, onOff);
matrix.drawPixel(5 + offSet, 14, onOff);
matrix.drawPixel(6 + offSet, 14, onOff);
matrix.drawPixel(7 + offSet, 14, onOff);
matrix.drawPixel(8 + offSet, 14, onOff);
matrix.drawPixel(9 + offSet, 14, onOff);
matrix.drawPixel(10 + offSet, 14, onOff);
matrix.drawPixel(11 + offSet, 14, onOff);
matrix.drawPixel(12 + offSet, 14, onOff);
matrix.drawPixel(13 + offSet, 14, onOff);
matrix.drawPixel(14 + offSet, 14, onOff);
matrix.drawPixel(15 + offSet, 14, onOff);
matrix.drawPixel(20 + offSet, 14, onOff);
matrix.drawPixel(16 + offSet, 15, onOff);
matrix.drawPixel(17 + offSet, 15, onOff);
matrix.drawPixel(18 + offSet, 15, onOff);
matrix.drawPixel(19 + offSet, 15, onOff);

 matrix.writeScreen();
}

void hitchOne(int offSet, int onOff)
{
matrix.drawPixel(3 + offSet, 0, onOff);
matrix.drawPixel(5 + offSet, 0, onOff);
matrix.drawPixel(2 + offSet, 1, onOff);
matrix.drawPixel(3 + offSet, 1, onOff);
matrix.drawPixel(4 + offSet, 1, onOff);
matrix.drawPixel(5 + offSet, 1, onOff);
matrix.drawPixel(6 + offSet, 1, onOff);
matrix.drawPixel(9 + offSet, 1, onOff);
matrix.drawPixel(10 + offSet, 1, onOff);
matrix.drawPixel(11 + offSet, 1, onOff);
matrix.drawPixel(12 + offSet, 1, onOff);
matrix.drawPixel(3 + offSet, 2, onOff);
matrix.drawPixel(4 + offSet, 2, onOff);
matrix.drawPixel(5 + offSet, 2, onOff);
matrix.drawPixel(10 + offSet, 2, onOff);
matrix.drawPixel(11 + offSet, 2, onOff);
matrix.drawPixel(12 + offSet, 2, onOff);
matrix.drawPixel(4 + offSet, 3, onOff);
matrix.drawPixel(8 + offSet, 3, onOff);
matrix.drawPixel(9 + offSet, 3, onOff);
matrix.drawPixel(13 + offSet, 3, onOff);
matrix.drawPixel(14 + offSet, 3, onOff);
matrix.drawPixel(20 + offSet, 3, onOff);
matrix.drawPixel(8 + offSet, 4, onOff);
matrix.drawPixel(14 + offSet, 4, onOff);
matrix.drawPixel(20 + offSet, 4, onOff);
matrix.drawPixel(21 + offSet, 4, onOff);
matrix.drawPixel(8 + offSet, 5, onOff);
matrix.drawPixel(14 + offSet, 5, onOff);
matrix.drawPixel(19 + offSet, 5, onOff);
matrix.drawPixel(20 + offSet, 5, onOff);
matrix.drawPixel(21 + offSet, 5, onOff);
matrix.drawPixel(8 + offSet, 6, onOff);
matrix.drawPixel(9 + offSet, 6, onOff);
matrix.drawPixel(10 + offSet, 6, onOff);
matrix.drawPixel(11 + offSet, 6, onOff);
matrix.drawPixel(12 + offSet, 6, onOff);
matrix.drawPixel(13 + offSet, 6, onOff);
matrix.drawPixel(14 + offSet, 6, onOff);
matrix.drawPixel(18 + offSet, 6, onOff);
matrix.drawPixel(9 + offSet, 7, onOff);
matrix.drawPixel(10 + offSet, 7, onOff);
matrix.drawPixel(11 + offSet, 7, onOff);
matrix.drawPixel(12 + offSet, 7, onOff);
matrix.drawPixel(13 + offSet, 7, onOff);
matrix.drawPixel(15 + offSet, 7, onOff);
matrix.drawPixel(18 + offSet, 7, onOff);
matrix.drawPixel(6 + offSet, 8, onOff);
matrix.drawPixel(7 + offSet, 8, onOff);
matrix.drawPixel(9 + offSet, 8, onOff);
matrix.drawPixel(10 + offSet, 8, onOff);
matrix.drawPixel(11 + offSet, 8, onOff);
matrix.drawPixel(12 + offSet, 8, onOff);
matrix.drawPixel(13 + offSet, 8, onOff);
matrix.drawPixel(16 + offSet, 8, onOff);
matrix.drawPixel(17 + offSet, 8, onOff);
matrix.drawPixel(5 + offSet, 9, onOff);
matrix.drawPixel(9 + offSet, 9, onOff);
matrix.drawPixel(10 + offSet, 9, onOff);
matrix.drawPixel(11 + offSet, 9, onOff);
matrix.drawPixel(12 + offSet, 9, onOff);
matrix.drawPixel(13 + offSet, 9, onOff);
matrix.drawPixel(5 + offSet, 10, onOff);
matrix.drawPixel(10 + offSet, 10, onOff);
matrix.drawPixel(11 + offSet, 10, onOff);
matrix.drawPixel(12 + offSet, 10, onOff);
matrix.drawPixel(18 + offSet, 10, onOff);
matrix.drawPixel(20 + offSet, 10, onOff);
matrix.drawPixel(2 + offSet, 11, onOff);
matrix.drawPixel(3 + offSet, 11, onOff);
matrix.drawPixel(4 + offSet, 11, onOff);
matrix.drawPixel(9 + offSet, 11, onOff);
matrix.drawPixel(13 + offSet, 11, onOff);
matrix.drawPixel(17 + offSet, 11, onOff);
matrix.drawPixel(18 + offSet, 11, onOff);
matrix.drawPixel(19 + offSet, 11, onOff);
matrix.drawPixel(20 + offSet, 11, onOff);
matrix.drawPixel(21 + offSet, 11, onOff);
matrix.drawPixel(1 + offSet, 12, onOff);
matrix.drawPixel(9 + offSet, 12, onOff);
matrix.drawPixel(13 + offSet, 12, onOff);
matrix.drawPixel(18 + offSet, 12, onOff);
matrix.drawPixel(19 + offSet, 12, onOff);
matrix.drawPixel(20 + offSet, 12, onOff);
matrix.drawPixel(8 + offSet, 13, onOff);
matrix.drawPixel(9 + offSet, 13, onOff);
matrix.drawPixel(13 + offSet, 13, onOff);
matrix.drawPixel(14 + offSet, 13, onOff);
matrix.drawPixel(19 + offSet, 13, onOff);
matrix.drawPixel(7 + offSet, 14, onOff);
matrix.drawPixel(8 + offSet, 14, onOff);
matrix.drawPixel(9 + offSet, 14, onOff);
matrix.drawPixel(13 + offSet, 14, onOff);
matrix.drawPixel(14 + offSet, 14, onOff);
matrix.drawPixel(15 + offSet, 14, onOff);

 matrix.writeScreen();
}

void hitchTwo(int offSet, int onOff)
{
matrix.drawPixel(9 + offSet, 1, onOff);
matrix.drawPixel(10 + offSet, 1, onOff);
matrix.drawPixel(11 + offSet, 1, onOff);
matrix.drawPixel(12 + offSet, 1, onOff);
matrix.drawPixel(10 + offSet, 2, onOff);
matrix.drawPixel(11 + offSet, 2, onOff);
matrix.drawPixel(12 + offSet, 2, onOff);
matrix.drawPixel(18 + offSet, 2, onOff);
matrix.drawPixel(20 + offSet, 2, onOff);
matrix.drawPixel(8 + offSet, 3, onOff);
matrix.drawPixel(9 + offSet, 3, onOff);
matrix.drawPixel(13 + offSet, 3, onOff);
matrix.drawPixel(14 + offSet, 3, onOff);
matrix.drawPixel(17 + offSet, 3, onOff);
matrix.drawPixel(18 + offSet, 3, onOff);
matrix.drawPixel(19 + offSet, 3, onOff);
matrix.drawPixel(20 + offSet, 3, onOff);
matrix.drawPixel(21 + offSet, 3, onOff);
matrix.drawPixel(8 + offSet, 4, onOff);
matrix.drawPixel(14 + offSet, 4, onOff);
matrix.drawPixel(18 + offSet, 4, onOff);
matrix.drawPixel(19 + offSet, 4, onOff);
matrix.drawPixel(20 + offSet, 4, onOff);
matrix.drawPixel(2 + offSet, 5, onOff);
matrix.drawPixel(3 + offSet, 5, onOff);
matrix.drawPixel(4 + offSet, 5, onOff);
matrix.drawPixel(8 + offSet, 5, onOff);
matrix.drawPixel(14 + offSet, 5, onOff);
matrix.drawPixel(19 + offSet, 5, onOff);
matrix.drawPixel(1 + offSet, 6, onOff);
matrix.drawPixel(5 + offSet, 6, onOff);
matrix.drawPixel(8 + offSet, 6, onOff);
matrix.drawPixel(9 + offSet, 6, onOff);
matrix.drawPixel(10 + offSet, 6, onOff);
matrix.drawPixel(11 + offSet, 6, onOff);
matrix.drawPixel(12 + offSet, 6, onOff);
matrix.drawPixel(13 + offSet, 6, onOff);
matrix.drawPixel(14 + offSet, 6, onOff);
matrix.drawPixel(5 + offSet, 7, onOff);
matrix.drawPixel(9 + offSet, 7, onOff);
matrix.drawPixel(10 + offSet, 7, onOff);
matrix.drawPixel(11 + offSet, 7, onOff);
matrix.drawPixel(12 + offSet, 7, onOff);
matrix.drawPixel(13 + offSet, 7, onOff);
matrix.drawPixel(15 + offSet, 7, onOff);
matrix.drawPixel(6 + offSet, 8, onOff);
matrix.drawPixel(7 + offSet, 8, onOff);
matrix.drawPixel(9 + offSet, 8, onOff);
matrix.drawPixel(10 + offSet, 8, onOff);
matrix.drawPixel(11 + offSet, 8, onOff);
matrix.drawPixel(12 + offSet, 8, onOff);
matrix.drawPixel(13 + offSet, 8, onOff);
matrix.drawPixel(16 + offSet, 8, onOff);
matrix.drawPixel(17 + offSet, 8, onOff);
matrix.drawPixel(9 + offSet, 9, onOff);
matrix.drawPixel(10 + offSet, 9, onOff);
matrix.drawPixel(11 + offSet, 9, onOff);
matrix.drawPixel(12 + offSet, 9, onOff);
matrix.drawPixel(13 + offSet, 9, onOff);
matrix.drawPixel(18 + offSet, 9, onOff);
matrix.drawPixel(20 + offSet, 9, onOff);
matrix.drawPixel(10 + offSet, 10, onOff);
matrix.drawPixel(11 + offSet, 10, onOff);
matrix.drawPixel(12 + offSet, 10, onOff);
matrix.drawPixel(18 + offSet, 10, onOff);
matrix.drawPixel(20 + offSet, 10, onOff);
matrix.drawPixel(21 + offSet, 10, onOff);
matrix.drawPixel(3 + offSet, 11, onOff);
matrix.drawPixel(5 + offSet, 11, onOff);
matrix.drawPixel(9 + offSet, 11, onOff);
matrix.drawPixel(13 + offSet, 11, onOff);
matrix.drawPixel(19 + offSet, 11, onOff);
matrix.drawPixel(20 + offSet, 11, onOff);
matrix.drawPixel(21 + offSet, 11, onOff);
matrix.drawPixel(2 + offSet, 12, onOff);
matrix.drawPixel(3 + offSet, 12, onOff);
matrix.drawPixel(4 + offSet, 12, onOff);
matrix.drawPixel(5 + offSet, 12, onOff);
matrix.drawPixel(6 + offSet, 12, onOff);
matrix.drawPixel(9 + offSet, 12, onOff);
matrix.drawPixel(13 + offSet, 12, onOff);
matrix.drawPixel(3 + offSet, 13, onOff);
matrix.drawPixel(4 + offSet, 13, onOff);
matrix.drawPixel(5 + offSet, 13, onOff);
matrix.drawPixel(8 + offSet, 13, onOff);
matrix.drawPixel(9 + offSet, 13, onOff);
matrix.drawPixel(13 + offSet, 13, onOff);
matrix.drawPixel(14 + offSet, 13, onOff);
matrix.drawPixel(4 + offSet, 14, onOff);
matrix.drawPixel(7 + offSet, 14, onOff);
matrix.drawPixel(8 + offSet, 14, onOff);
matrix.drawPixel(9 + offSet, 14, onOff);
matrix.drawPixel(13 + offSet, 14, onOff);
matrix.drawPixel(14 + offSet, 14, onOff);
matrix.drawPixel(15 + offSet, 14, onOff);

 matrix.writeScreen();
}

void flagHandHeartAnimation()
{
  flagTwo(24*2, 0);
  hitchOne(24*3, 1);
  heartOne(24*2, 1);
  handOne(24, 1);
 // flagOne(24*2, 1);
  delay(150);
  handOne(24, 0);
  hitchOne(24*3, 0);
  heartOne(24*2, 0);
 // flagOne(24*2, 0);
  handTwo(24, 1);
  heartTwo(24*2, 1);
//  flagTwo(24*2, 1);
  hitchTwo(24*3, 1);
  delay(150);
  handTwo(24, 0);
  hitchTwo(24*3, 0);
  heartTwo(24*2, 0);
 // flagTwo(24*2, 0);
   handOne(24, 1);
  hitchOne(24*3, 1);
 // heartOne(24*2, 1);
  flagOne(24*2, 1);
  delay(150);
  flagOne(24*2, 0);
  flagTwo(24*2, 1);
}

