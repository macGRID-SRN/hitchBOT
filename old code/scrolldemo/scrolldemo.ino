#include "HT1632.h"

#define DATA 2
#define WR   3
#define CS   4
#define CS2  5

// use this line for single matrix
//HT1632LEDMatrix matrix = HT1632LEDMatrix(DATA, WR, CS);
// use this line for two matrices!
HT1632LEDMatrix matrix = HT1632LEDMatrix(DATA, WR, CS, CS2);

void setup() {
  Serial.begin(9600);
  matrix.begin(HT1632_COMMON_16NMOS);  
}

void loop() {
  int x=0;
  char displayString [] = "Sample String";
  int stringLength = sizeof(displayString) - 1;
  int lowerLeftLimit = stringLength * 6 * -1;
  int displayWidth = matrix.width() - 1;
  int topX;
  
  matrix.clearScreen(); 
  //matrix.setTextSize(1);    // this works
  matrix.setTextSize(2);  //this fails when x < -1
                          // text just disappears
  matrix.setTextColor(1);   // 'lit' LEDs
  
  for (x = displayWidth; x > (lowerLeftLimit);x--){
    if (x >= lowerLeftLimit) {
      matrix.setCursor(x, 0);   // next line, 8 pixels down
      matrix.print(displayString);
    }
    matrix.writeScreen();
    delay(50);
    matrix.clearScreen();
  }
  // whew!
}
