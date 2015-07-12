#define voltageREAD A0


void setup() {
  Serial.begin(9600);
  

}

void loop() {
  int sensorValue = analogRead(voltageREAD);
  float voltage= sensorValue * (5.0 / 1023.0);
  delay(100);
  Serial.println(voltage);



}

double getVoltage(){
  return analogRead(voltageREAD) * (5.0 / 1023.0);
}

double getAverageVoltage(int samples){
  double sum = 0.0;
  for(int i = 0 ; i < samples ; i ++){
    sum += getVoltage();
  }
  return sum / samples;
}

