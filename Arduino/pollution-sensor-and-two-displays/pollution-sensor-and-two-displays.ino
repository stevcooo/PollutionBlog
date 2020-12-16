#include "Arduino.h"
#include "SdsDustSensor.h"
#include "TM1637Display.h"

//Dust sensor
int txPin = D2;
int rxPin = D3;
SdsDustSensor sds(txPin, rxPin);

//PM 10 display
#define CLK_10 D5
#define DIO_10 D6
TM1637Display display10(CLK_10, DIO_10); //set up the 4-Digit Display.

//PM 2.5 display
#define CLK_25 D7
#define DIO_25 D8
TM1637Display display25(CLK_25, DIO_25); //set up the 4-Digit Display.

// The amount of time (in milliseconds) between tests
#define TEST_DELAY   2000

void setup() {
  
  Serial.begin(9600);
  display25.setBrightness(0x0a); //set the diplay to maximum brightness
  display25.showNumberDec(0);
  
  display10.setBrightness(0x0a); //set the diplay to maximum brightness
  display10.showNumberDec(0);
  
  sds.begin();    
  Serial.println(sds.queryFirmwareVersion().toString()); // prints firmware version
  Serial.println(sds.setActiveReportingMode().toString()); // ensures sensor is in 'active' reporting mode

  WorkingStateResult queryWorkingState = sds.queryWorkingState();
  while(!queryWorkingState.isWorking())
  {
    Serial.println("waking up the sensor...");
    sds.wakeup();
    queryWorkingState = sds.queryWorkingState();
  }
  
  Serial.begin(115200);
}


PmResult GetAverage(int numberOfReads){  
  PmResult result = sds.readPm();
  PmResult tmp = result;
  int counter = numberOfReads;  
  while(counter>0){    
    tmp = sds.readPm();
    if(tmp.isOk()){
      result.pm25 += tmp.pm25;
      result.pm10 += tmp.pm10;
     
      Serial.print("PM2.5 = ");
      Serial.print(tmp.pm25);
      display25.showNumberDec(tmp.pm25);
      
      Serial.print(", PM10 = ");
      Serial.println(tmp.pm10);
      display10.showNumberDec(tmp.pm10);
      
      Serial.print("ReadsLeft: ");
      Serial.println(counter);
      
      counter--;      
      delay(1000);
    }    
  }
  result.pm25 = result.pm25/numberOfReads;
  result.pm10 = result.pm10/numberOfReads; 

  return result;
}

void loop() {
  PmResult pm = GetAverage(10);
  
  if (pm.isOk()) {
    Serial.print("PM2.5 = ");
    Serial.print(pm.pm25);
    Serial.print(", PM10 = ");
    Serial.println(pm.pm10);    
    
    // Selectively set different digits
    int pm10 = pm.pm10;    
    Serial.print("PM10 = ");
    Serial.println(pm10);
    display10.showNumberDec(pm10);

    int pm25 = pm.pm25;
    Serial.print("PM2.5 = ");
    Serial.println(pm25);
    display25.showNumberDec(pm25);

  WorkingStateResult sleepResult = sds.sleep();
  while(sleepResult.isWorking())
  {
    Serial.println("putting sensor to sleep...");
    sleepResult = sds.sleep();
  }
  Serial.println("Sensor sleeps!");
  //5min = 5 x 60 x 1000 = 300000
  delay(60000);      
  WorkingStateResult wakeupResult = sds.wakeup();
  while(!wakeupResult.isWorking())
  {
    Serial.println("waking up the sensor...");
    wakeupResult = sds.wakeup();
  }
  Serial.println("Sensor is up and runnig!");
  } else {
    // notice that loop delay is set to 0.5s and some reads are not available
    Serial.print("Could not read values from sensor, reason: ");
    Serial.println(pm.statusToString());    
  }
}
