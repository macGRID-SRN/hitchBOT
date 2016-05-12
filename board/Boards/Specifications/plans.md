# hitchBOT on a board (hbob) specifications outline
--------

The purpose of this document is to outline the planned functionality of most (not all) electronics for hitchBOT onto a single board.

The board should be designed as a shield for the OpenUPS by mini-box http://www.mini-box.com/OpenUPS


Power Inputs
------

hbob should accept power from multiple 12V sources.

Inputs: 

 - Laptop Transformer (12V, 7A or more)
 - Car Charger (12V, unknown amount of current)
 - Solar (12V or higher, ~0.5A on a good day)
 - Magic

Input conditioning
---

Car Charger input should be ISOLATED + heavily filtered

Power Outputs
---

 - 12V main output to OpenUPS board - molex 2x2
 - Anything which does not require battery power and should only be run during charging / power cycle (e.g. Servo)

Power Passback
---

After the 12V is brought back into the board it should be again split up into different outputs for different purposes.

 - 5V regulator for Servo
 - 5V regulator for electronics in head (should have 12V run into head and then stepped down to 5V there)

Filtering
---

The summed power input should be filtered to remove high frequency noise produced by radios (100+ MHz). A decoupling and bypass capacitor network should be used.

Power Protection
---

Fuses (approx 10A) should be used after power summation to ensure power consumption is not exceeded. The OpenUPS does have fuses onboard but additional fuses should be used.