The power system inside of hitchBOT has proven to not be a simple task.

All the power coming into the bot is from an external source with unpredictable quality and voltage.

The main source of power is the 120-220V AC-DC converter. Since it is expected to have global performance with the ability to account for varying voltages and varying power supply quality it is assumed to be managed within the transformer.

The 12V DC IN has proven itself to be a potential issue. Older cars are well known for creating a large amount of noise - often directly from the alternator or radio system.

## Feedback ##

The previous system worked fairly well - feedback was more slack should be left in the wiring and no short wires. Also no terminal blocks.

## PCB Design Requirements ##

Boards should be designed as individual modules with interchangeable connectors.

Design should reflect an IN -> OUT design pattern. Overall board shape should be rectangular. The shorter sides should be IN/OUT sides. This design allows for easy module to module connections as well as easy heat shrink protection.

We plan to move to stackable boards.

The following features are also a must:

 - Additional Input/Output Connectors - this will allow for easy additions to modules if required.
 - Status LEDs for power, activity and errors.
 - Mounting Holes - should pick a standard size soon.
 - Power planes - when possible
 - **NO TERMINAL BLOCKS** - if a board we need to use has them they should be removed and replaced with a different connector or wires could be soldered directly to the PCB.

## Internal Wiring ##

Voltage should be kept as high as possible at all times. This is to reduce power loss over wires and reduce noise in power system transmission. An example of this would be to decentralize the stepping down of 12V - 5V DC. The current setup has most of the step down boards centrally located near the power distribution board. This could be improved by moving the stepup/stepdown boards closer to the device they are powering. The voltage will be 12v instead of 5v over the wire to the board, meaning the current will be lower. There will be less of a voltage drop as a result.

Shielded Stranded Wire should be used for all wires moving wire throughout the bot. This is to reduce noise and other types of interference.

Appropriate wire gauge size should be selected based on expected throughput current and voltage. Multiple conductor wire should be used when necessary - it reduces the bulk of the wiring and is much cleaner. Also reduced noise if the wire is shielded.


Heatshrink should be used as protection when required.

## Load Testing ##

Specifications for the bot are as follows *(to be written)*

*Should also included heat verification.*

## Assumptions ##

The following assumptions are made when calculating power system requirements:

 - **80% efficiency on ANY** step down/step up or other switching voltage regulator. This includes ones listed above 80% efficiency - this is because while a component may be listed as "95%" efficient"; this stat only refers to a very specific input and output voltage and current draw. It can also be assumed our implementation of a circuit is inefficient. Data can be collected to properly determine component and circuit implementation efficiency.