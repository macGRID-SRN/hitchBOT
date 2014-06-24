
RBL BLE Library Package
———————————


1. BLE Shield Library

This is the library for the BLE Shield that provides BLE capability for Arduino. It currently supports the following boards:
- UNO
- Leonardo
- MEGA 2560
- DUE

For DUE, you need to use Arduino IDE 1.5.4 or above.

Other variants may be compatible but not yet been tested. You can report to us if you find it works on your boards.

The lower layer is provided the Nordic ACI library (Nordic_BLE) which communicates to the nRF8001 BLE chip via the SPI interface.

The APIs we defined are in the RBL_BLEShield folder, which allows your sketch to deal with BLE easier with the read/write APIs.

2. BLE Mini Library

The BLE Mini library allows you to communicate to Arduino or other external MCU via the UART interface.

3. iOS/Android SDK

The iOS/Android SDK provides BLE Framework that allows you to develop iOS/Android App with BLE technology. It also comes some demos for you to try.

For iOS, it supports version 7 and the following devices are supported:
- iPhone 4S/5/5s
- iPad 3/4/air/mini
- iPod touch 5

For Android, it supports Android 4.3 and the following devices are supported:
- Nexus 4/7

Other Android devices with version 4.3 with BLE technology should work but not yet been tested.
