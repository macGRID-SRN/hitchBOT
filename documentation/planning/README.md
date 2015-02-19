The purpose of this document is define the exact requirements of the hardware for hitchBOT. This information should be used as an aid for hardware selection and customization.

What part of hitchbot is the hardware? What is often referred to as the "brain". The part of it that *does stuff*. The part which humans interact with. This does not include power system hardware such at the battery/power system

**The following is a list of general requirements.**

 - Pictures. Picture storage for when picture updates cannot be sent.
 - Speech Recognition/Speech Synthesis/Chatbot
 - IO - Control of Face/Arm/Lights. Battery Management, Monitoring. Control of any additional electronic systems. Other types of IO are required such as Audio Out, Microphone in.
 - GPS - constant logging, this should be a separate system with its own battery and reporting ability to ensure reliability and real time updates. The goal for the next iteration: **logging coordinates every 3 minutes.**
 - LTE/3G or some type of internet connectivity. This is required to send pictures and other data. It may also be used for speech recognition as internet connected solutions tend to have higher recognition rates.

There will likely not be a single device which will meet all of these requirements. The entire system can be broken down into subsystem if required.