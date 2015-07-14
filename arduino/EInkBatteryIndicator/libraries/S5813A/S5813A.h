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

#if !defined(EPD_S5813A_H)
#define EPD_S5813A_H 1

#include <Arduino.h>

// TODO: Why is begin allowed to change the temperature pin(?)
// Except for that could make temperature_pin a const
class S5813A_Class {
private:
	uint8_t temperature_pin;

	S5813A_Class(const S5813A_Class &f);  // prevent copy

public:
	int read(void);
	long readVoltage(void);  // returns micro volts

	// inline static void attachInterrupt();
	// inline static void detachInterrupt();

	void begin(uint8_t input_pin);
	void end(void);

	S5813A_Class(uint8_t input_pin);

};

extern S5813A_Class S5813A;

#endif
