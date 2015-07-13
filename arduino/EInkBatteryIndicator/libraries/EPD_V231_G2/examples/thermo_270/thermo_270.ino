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


// graphic temperature display

// Operation from reset:
// * display version
// * display compiled-in display setting
// * display FLASH detected or not
// * display temperature (displayed before every image is changed)
// * clear screen
// * update display (temperature)
// * delay 60 seconds (flash LED)
// * back to update display


#include <Arduino.h>
#include <inttypes.h>
#include <ctype.h>

// required libraried
#include <SPI.h>
#include <FLASH.h>
#include <EPD_V231_G2.h>
#define SCREEN_SIZE 270
#include <EPD_PANELS.h>
#include <S5813A.h>
#include <Adafruit_GFX.h>
#include <EPD_GFX.h>


// update delay in seconds
#define LOOP_DELAY_SECONDS 60


// no futher changes below this point

// current version number
#define THERMO_VERSION "4"


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

// graphic handler
EPD_GFX G_EPD(EPD, S5813A);

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
#endif
	Serial.println();
	Serial.println();
	Serial.println("Thermo version: " THERMO_VERSION);
	Serial.println("Display size: " MAKE_STRING(EPD_SIZE));
	Serial.println("Film: V" MAKE_STRING(EPD_FILM_VERSION));
	Serial.println("COG: G" MAKE_STRING(EPD_CHIP_VERSION));
	Serial.println();

	FLASH.begin(Pin_FLASH_CS);
	if (FLASH.available()) {
		Serial.println("FLASH chip detected OK");
	} else {
		Serial.println("unsupported FLASH chip");
	}

	// configure temperature sensor
	S5813A.begin(Pin_TEMPERATURE);

	// get the current temperature
	int temperature = S5813A.read();
	Serial.print("Temperature = ");
	Serial.print(temperature);
	Serial.println(" Celcius");

	// set up graphics EPD library
	// and clear the screen
	G_EPD.begin();
}


// main loop
void loop() {
	int temperature = S5813A.read();
	Serial.print("Temperature = ");
	Serial.print(temperature);
	Serial.println(" Celcius");

	int h = G_EPD.height();
	int w = G_EPD.width();

	G_EPD.drawRect(1, 1, w - 2, h - 2, EPD_GFX::BLACK);
	G_EPD.drawRect(3, 3, w - 6, h - 6, EPD_GFX::BLACK);

	G_EPD.fillTriangle(135,20, 186,40, 152,84, EPD_GFX::BLACK);
	G_EPD.fillTriangle(139,26, 180,44, 155,68, EPD_GFX::WHITE);

	char temp[sizeof("-999 C")];
	snprintf(temp, sizeof(temp), "%4d C", temperature);

	int x = 20;
	int y = 30;
	for (int i = 0; i < sizeof(temp) - 1; ++i, x += 14) {
		G_EPD.drawChar(x, y, temp[i], EPD_GFX::BLACK, EPD_GFX::WHITE, 2);
	}

	// small circle for degrees symbol
	G_EPD.drawCircle(20 + 4 * 14 + 6, 30, 4, EPD_GFX::BLACK);

// 100 difference just to simplify things
// so 1 pixel = 1 degree
#define T_MIN (-10)
#define T_MAX 80

	// clip
	if (temperature < T_MIN) {
		temperature= T_MIN;
	} else if (temperature > T_MAX) {
		temperature = T_MAX;
	}

	// temperature bar
	int bar_w = temperature - T_MIN;  // zero based
	int bar_h = 4;
	int bar_x0 = 24;
	int bar_y0 = 60;

	G_EPD.fillRect(bar_x0, bar_y0, T_MAX - T_MIN, bar_h, EPD_GFX::WHITE);
	G_EPD.fillRect(bar_x0, bar_y0, bar_w, bar_h, EPD_GFX::BLACK);

	// scale
	for (int t0 = T_MIN; t0 < T_MAX; t0 += 5) {
		int t = t0 - T_MIN;
		int tick = 8;
		if (0 == t0) {
			tick = 12;
			G_EPD.drawCircle(bar_x0 + t, bar_y0 + 16, 3, EPD_GFX::BLACK);
		} else if (0 == t0 % 10) {
			tick = 10;
		}
		G_EPD.drawLine(bar_x0 + t, bar_y0 + tick, bar_x0 + t, bar_y0 + 6, EPD_GFX::BLACK);
		G_EPD.drawLine(bar_x0 + t, bar_y0 + 6, bar_x0 + t + 5, bar_y0 + 6, EPD_GFX::BLACK);
		G_EPD.drawLine(bar_x0 + t + 5, bar_y0 + 6, bar_x0 + t + 5, bar_y0 + 8, EPD_GFX::BLACK);
	}

	// update the display
	G_EPD.display();

	// flash LED for a number of seconds
	for (int x = 0; x < LOOP_DELAY_SECONDS * 10; ++x) {
		  digitalWrite(Pin_RED_LED, LED_ON);
		  delay(50);
		  digitalWrite(Pin_RED_LED, LED_OFF);
		  delay(50);
	}
}
