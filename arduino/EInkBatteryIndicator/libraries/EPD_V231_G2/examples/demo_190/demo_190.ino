// -*- mode: c++ -*-
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


// Notice: ***** Generated file: DO _NOT_ MODIFY, Created on: 2015-03-27 05:59:59 UTC *****


// Simple demo to toggle EPD between two images.

// Operation from reset:
// * display version
// * display compiled-in display setting
// * display FLASH detected or not
// * display temperature (displayed before every image is changed)
// * clear screen
// * delay 5 seconds (flash LED)
// * display text image
// * delay 5 seconds (flash LED)
// * display picture
// * delay 5 seconds (flash LED)
// * back to text display


#include <Arduino.h>
#include <inttypes.h>
#include <ctype.h>

// required libraries
#include <SPI.h>
#include <FLASH.h>
#include <EPD_V231_G2.h>
#define SCREEN_SIZE 190
#include <EPD_PANELS.h>
#include <S5813A.h>

// select two images from:  text_image text-hello cat aphrodite venus saturn
#define IMAGE_1  text_image
#define IMAGE_2  cat

// Error message for MSP430
#if (SCREEN_SIZE == 270) && defined(__MSP430_CPU__)
#error MSP430: not enough memory
#endif

// no futher changed below this point

// current version number
#define DEMO_VERSION "4"


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

	Serial.begin(9600);
#if defined(__AVR__)
	// wait for USB CDC serial port to connect.  Arduino Leonardo only
	while (!Serial) {
	}
	delay(20);  // allows terminal time to sync
#endif
	Serial.println();
	Serial.println();
	Serial.println("Demo version: " DEMO_VERSION);
	Serial.println("Display size: " MAKE_STRING(EPD_SIZE));
	Serial.println("Film: V" MAKE_STRING(EPD_FILM_VERSION));
	Serial.println("COG: G" MAKE_STRING(EPD_CHIP_VERSION));

	Serial.println();

	FLASH.begin(Pin_FLASH_CS);
	if (FLASH.available()) {
		Serial.println("FLASH chip detected OK");
	} else {
		uint8_t maufacturer;
		uint16_t device;
		FLASH.info(&maufacturer, &device);
		Serial.print("unsupported FLASH chip: MFG: 0x");
		Serial.print(maufacturer, HEX);
		Serial.print("  device: 0x");
		Serial.print(device, HEX);
		Serial.println();
	}

	// configure temperature sensor
	S5813A.begin(Pin_TEMPERATURE);
}


static int state = 0;


// main loop
void loop() {
	int temperature = S5813A.read();
	Serial.print("Temperature = ");
	Serial.print(temperature);
	Serial.println(" Celcius");

	EPD.begin(); // power up the EPD panel
	EPD.setFactor(temperature); // adjust for current temperature

	int delay_counts = 50;
	switch(state) {
	default:
	case 0:         // clear the screen
		EPD.clear();
		state = 1;
		delay_counts = 5;  // reduce delay so first image come up quickly
		break;

	case 1:         // clear -> text
#if EPD_IMAGE_ONE_ARG
		EPD.image(IMAGE_1_BITS);
#elif EPD_IMAGE_TWO_ARG
		EPD.image_0(IMAGE_1_BITS);
#else
#error "unsupported image function"
#endif
		++state;
		break;

	case 2:         // text -> picture
#if EPD_IMAGE_ONE_ARG
		EPD.image(IMAGE_2_BITS);
#elif EPD_IMAGE_TWO_ARG
		EPD.image(IMAGE_1_BITS, IMAGE_2_BITS);
#else
#error "unsupported image function"
#endif
		++state;
		break;

	case 3:        // picture -> text
#if EPD_IMAGE_ONE_ARG
		EPD.image(IMAGE_1_BITS);
#elif EPD_IMAGE_TWO_ARG
		EPD.image(IMAGE_2_BITS, IMAGE_1_BITS);
#else
#error "unsupported image function"
#endif
		state = 2;  // back to picture next time
		break;
	}
	EPD.end();   // power down the EPD panel

	// flash LED for 5 seconds
	for (int x = 0; x < delay_counts; ++x) {
		digitalWrite(Pin_RED_LED, LED_ON);
		delay(50);
		digitalWrite(Pin_RED_LED, LED_OFF);
		delay(50);
	}
}
