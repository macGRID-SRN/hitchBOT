// -*- mode: c++ -*-
// Based on the Pervasive Displays demo code:
//================================================
// Copyright 2013-2015 Pervasive Displays, Inc.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at:
//
//   http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing,
// software distributed under the License is distributed on an
// "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either
// express or implied.  See the License for the specific language
// governing permissions and limitations under the License.


// {% SYSTEM:notice %}
//================================================

// A program to display the hitchBOT battery indicator
// To create the XBM Files:
// -Use a maximum of 4 shades (eg: white, gray, dark gray, black)
// -Save as 8bit BMP with NO dithering
// -Use this site to convert to XBM: http://www.online-utility.org/image_converter.jsp?outputType=XBM
// -Edit the XBM file it produces to have the filename and _2_7 (for 2.7" display) rather than the random number string
// -Put the XBM in your Arduino libraries/Images folder
// -The UNO can fit four images roughly plus some code for a 2.7" display, so for more images you may want to look into SD card usage

#include <Arduino.h>
#include <inttypes.h>
#include <ctype.h>

// required libraries and definitions for the E-INK display
#include <SPI.h>
#include <FLASH.h>
#include <EPD_V231_G2.h>
#define SCREEN_SIZE 270
#include <EPD_PANELS.h>
#include <S5813A.h>

// Voltage Read analogue pin
#define voltageREAD A5
#define chargingREAD A3

//Define our battery levels in %
#define PLUG_GOOD_PERCENT 95
#define UNPLUG_GOOD_PERCENT 80
#define PLUG_BAD_PERCENT 10
#define UNPLUG_BAD_PERCENT 10

//Define the images
#define IMAGE_1 happy
#define IMAGE_2 excite
#define IMAGE_3 panic
#define IMAGE_4 sleep

// version number
#define COMMAND_VERSION "4"

// definition of I/O pins LaunchPad and Arduino are different
#if defined(__MSP430_CPU__)

// TI LaunchPad IO layout
const int Pin_TEMPERATURE = A4;
const int Pin_PANEL_ON = P2_3;
const int Pin_BORDER = P2_5;
const int Pin_DISCHARGE = P2_4;
#if EPD_PWM_REQUIRED
const int Pin_PWM = P2_1;
#endif
const int Pin_RESET = P2_2;
const int Pin_BUSY = P2_0;
const int Pin_EPD_CS = P2_6;
const int Pin_FLASH_CS = P2_7;
const int Pin_SW2 = P1_3;
const int Pin_RED_LED = P1_0;
#else
// Arduino IO layout
const int Pin_TEMPERATURE = A0;
const int Pin_PANEL_ON = 2;
const int Pin_BORDER = 3;
const int Pin_DISCHARGE = 4;
#if EPD_PWM_REQUIRED
const int Pin_PWM = 5;
#endif
const int Pin_RESET = 6;
const int Pin_BUSY = 7;
const int Pin_EPD_CS = 8;
const int Pin_FLASH_CS = 9;
const int Pin_SW2 = 12;
const int Pin_RED_LED = 13;
#endif


// LED anode through resistor to I/O pin
// LED cathode to Ground
#define LED_ON  HIGH
#define LED_OFF LOW

// pre-processor convert to string
#define MAKE_STRING1(X) #X
#define MAKE_STRING(X) MAKE_STRING1(X)

// other pre-processor magic
// token joining and computing the string for #include
#define ID(X) X
#define MAKE_NAME1(X,Y) ID(X##Y)
#define MAKE_NAME(X,Y) MAKE_NAME1(X,Y)
#define MAKE_JOIN(X,Y) MAKE_STRING(MAKE_NAME(X,Y))

// calculate the include name and variable names
#define IMAGE_1_FILE MAKE_JOIN(IMAGE_1,EPD_IMAGE_FILE_SUFFIX)
#define IMAGE_1_BITS MAKE_NAME(IMAGE_1,EPD_IMAGE_NAME_SUFFIX)
#define IMAGE_2_FILE MAKE_JOIN(IMAGE_2,EPD_IMAGE_FILE_SUFFIX)
#define IMAGE_2_BITS MAKE_NAME(IMAGE_2,EPD_IMAGE_NAME_SUFFIX)
#define IMAGE_3_FILE MAKE_JOIN(IMAGE_3,EPD_IMAGE_FILE_SUFFIX)
#define IMAGE_3_BITS MAKE_NAME(IMAGE_3,EPD_IMAGE_NAME_SUFFIX)
#define IMAGE_4_FILE MAKE_JOIN(IMAGE_4,EPD_IMAGE_FILE_SUFFIX)
#define IMAGE_4_BITS MAKE_NAME(IMAGE_4,EPD_IMAGE_NAME_SUFFIX)

// Add Images library to compiler path
#include <Images.h>  // this is just an empty file

// images
PROGMEM const
#define unsigned
#define char uint8_t
#include IMAGE_1_FILE
#undef char
#undef unsigned

PROGMEM const
#define unsigned
#define char uint8_t
#include IMAGE_2_FILE
#undef char
#undef unsigned


PROGMEM const
#define unsigned
#define char uint8_t
#include IMAGE_3_FILE
#undef char
#undef unsigned


PROGMEM const
#define unsigned
#define char uint8_t
#include IMAGE_4_FILE
#undef char
#undef unsigned

// define the E-Ink display
EPD_Class EPD(EPD_SIZE,
	      Pin_PANEL_ON,
	      Pin_BORDER,
	      Pin_DISCHARGE,
#if EPD_PWM_REQUIRED
	      Pin_PWM,
#endif
	      Pin_RESET,
	      Pin_BUSY,
	      Pin_EPD_CS);

int currIMG = 0;
int lastIMG = 0; //The last image displayed, we only change image if we notice a difference between currIMG and lastIMG!        

//Reading the charging status
boolean getCharging(){
  if (analogRead(chargingREAD) * (5.0 / 1023.0) > 2.0) {
    return true;
  } else {
    return false;
  }
}

//Reading voltages
double getVoltage(){
  return analogRead(voltageREAD) * (5.0 / 1023.0);
}

//Average the voltage out over the number of samples  
double getAverageVoltage(int samples){
  double sum = 0.0;
  for(int i = 0 ; i < samples ; i ++){
    sum += getVoltage();
  }
  return sum / samples;
}

//Preparing the correct image to display based on current battery percentages
int getCorrectImage(){
  boolean charging = getCharging();
  /*
  3.96+ = full (95%) charge (20.3V)
  3.81 = 80% charge (19.5V)
  3.66 = 20% charge (18.75V)
  3.60 = 10% charge (18.5V)
  */
  
  double voltage = getAverageVoltage(8);
  
  int battery = 0;
  
  if(voltage >= 3.96) {
    battery = 95;
  }
  else if(voltage >= 3.81) {
    battery = 80;
  }
  else if(voltage >= 3.66) {
    battery = 20;
  }
  else { //3.60 is "true" 10%, but we want to have some battery saved for the final E-Ink image to be applied!
    battery = 10;
  }
  
  //Image numbers to return
  //0 = GOOD!
  //1 = CHARGING
  //2 = CHARGE ME!
  //3 = PLUG ME IN!!!
  
  //If plugged in and above plugged-in-good-percent then we are ready for adventure
  if(battery >= PLUG_GOOD_PERCENT && charging){
    return 0;
  }
  //If not plugged in and above unplugged-good-percent then we are OKAY
  if(battery >= UNPLUG_GOOD_PERCENT && !charging){
    return 0;
  }
  //If plugged in and above the BAD percentage, we are charging
  if(battery > PLUG_BAD_PERCENT && charging){
    return 1;
  }
  //If not plugged in and above BAD percentage, we need to charge (the returns above prevent us from getting here if GOOD is true)
  if(battery > UNPLUG_BAD_PERCENT && !charging){
    return 2;
  }
  //If plugged in below BAD percentage, we emphasize that we still need to CHARGE
  if(battery <= PLUG_BAD_PERCENT && charging){
    return 1;
  }
  //If not plugged in below BAD percentage, we are begging them to plug us in
  //"If you want me to be talkative and my battery is low, you're going to have a bad time!"
  //if(voltage <= PLUG_BAD_PERCENT && !charging){ 
    //No need for this IF, save code by defaulting to the PLUG ME IN!!! image below
  //}
  return 3;
  
}

//------------------------------------------------------------------------------------------------------------
//------------------------------------------------------------------------------------------------------------
//SETUP
//------------------------------------------------------------------------------------------------------------
//------------------------------------------------------------------------------------------------------------
// I/O setup
void setup() {
	pinMode(Pin_RED_LED, OUTPUT);
	pinMode(Pin_SW2, INPUT);
	pinMode(Pin_TEMPERATURE, INPUT);
#if EPD_PWM_REQUIRED
	pinMode(Pin_PWM, OUTPUT);
#endif
	pinMode(Pin_BUSY, INPUT);
	pinMode(Pin_RESET, OUTPUT);
	pinMode(Pin_PANEL_ON, OUTPUT);
	pinMode(Pin_DISCHARGE, OUTPUT);
	pinMode(Pin_BORDER, OUTPUT);
	pinMode(Pin_EPD_CS, OUTPUT);
	pinMode(Pin_FLASH_CS, OUTPUT);

	digitalWrite(Pin_RED_LED, LOW);
#if EPD_PWM_REQUIRED
	digitalWrite(Pin_PWM, LOW);
#endif
	digitalWrite(Pin_RESET, LOW);
	digitalWrite(Pin_PANEL_ON, LOW);
	digitalWrite(Pin_DISCHARGE, LOW);
	digitalWrite(Pin_BORDER, LOW);
	digitalWrite(Pin_EPD_CS, LOW);
	digitalWrite(Pin_FLASH_CS, HIGH);
        
	FLASH.begin(Pin_FLASH_CS);

	// configure temperature sensor
	S5813A.begin(Pin_TEMPERATURE);

      //Hackin'
        EPD.begin();
	int t = S5813A.read();
	EPD.setFactor(t);
	EPD.clear();
    	EPD.end();
    
      currIMG = 0;
      lastIMG = 0;

}


//------------------------------------------------------------------------------------------------------------
//------------------------------------------------------------------------------------------------------------
//MAIN
//------------------------------------------------------------------------------------------------------------
//------------------------------------------------------------------------------------------------------------
// main loop
void loop() {
        currIMG = getCorrectImage();
        
        //Only change the image when it has actually changed
        if(currIMG != lastIMG) {
          //Clear the screen first!
          EPD.begin();
    	  int t = S5813A.read();
    	  EPD.setFactor(t);
    	  EPD.clear();
          EPD.end();
          
          EPD.begin();
	  t = S5813A.read();
	  EPD.setFactor(t);
        
          
          switch(currIMG) {
            default:
            case 0:
  	
#if EPD_IMAGE_ONE_ARG
  		EPD.image(IMAGE_1_BITS);
#elif EPD_IMAGE_TWO_ARG
  		EPD.image_0(IMAGE_1_BITS);
#else
#error "unsupported image function"
#endif
              break;
            case 1:
#if EPD_IMAGE_ONE_ARG
  		EPD.image(IMAGE_2_BITS);
#elif EPD_IMAGE_TWO_ARG
  		EPD.image_0(IMAGE_2_BITS);
#else
#error "unsupported image function"
#endif
              break;
            case 2:
#if EPD_IMAGE_ONE_ARG
  		EPD.image(IMAGE_3_BITS);
#elif EPD_IMAGE_TWO_ARG
  		EPD.image_0(IMAGE_3_BITS);
#else
#error "unsupported image function"
#endif
              break;
            case 3:
#if EPD_IMAGE_ONE_ARG
  		EPD.image(IMAGE_4_BITS);
#elif EPD_IMAGE_TWO_ARG
  		EPD.image_0(IMAGE_4_BITS);
#else
#error "unsupported image function"
#endif
             break;
          }
          
       EPD.end();
       
       lastIMG = currIMG; //Update lastIMG
    }
    
    delay(1000); //Wait for next check!

}
