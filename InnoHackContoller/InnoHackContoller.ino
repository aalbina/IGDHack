#include "I2Cdev.h"
#include "MPU6050_6Axis_MotionApps20.h"
#include "math.h"
#include "rgb_lcd.h"
#include <Arduino.h>
#include <TM1637Display.h>


MPU6050 mpu;
#define INTERRUPT_PIN 2  // use pin 2 on Arduino Uno & most boards
#define LED_PIN 13 // (Arduino is 13, Teensy is 11, Teensy++ is 6)
bool blinkState = false;
// MPU control/status vars
bool dmpReady = false;  // set true if DMP init was successful
uint8_t mpuIntStatus;   // holds actual interrupt status byte from MPU
uint8_t devStatus;      // return status after each device operation (0 = success, !0 = error)
uint16_t packetSize;    // expected DMP packet size (default is 42 bytes)
uint16_t fifoCount;     // count of all bytes currently in FIFO
uint8_t fifoBuffer[64]; // FIFO storage buffer

Quaternion q;           // [w, x, y, z]         quaternion container
VectorFloat gravity;    // [x, y, z]            gravity vector
float ypr[3];           // [yaw, pitch, roll]   yaw/pitch/roll container and gravity vector

volatile bool mpuInterrupt = false;     // indicates whether MPU interrupt pin has gone high
void dmpDataReady() {
    mpuInterrupt = true;
}

// Module connection pins (Digital Pins)
#define CLK 4
#define DIO 3
TM1637Display display(CLK, DIO);
const byte minus_seg[1]={SEG_G};
void drawGas(byte gas,bool reverse){
  display.clear();
  display.showNumberDec(gas, true,3,1);
  if(reverse){
    display.setSegments(minus_seg,1,0);
  }
}
rgb_lcd lcd;
byte black_square[8] = {
    0b11111,
    0b11111,
    0b11111,
    0b11111,
    0b11111,
    0b11111,
    0b11111,
    0b11111
};
void setlcdlevel(byte level){
  lcd.clear();  
  for(byte i=0;i<level;i++){
    lcd.setCursor(i,0);
    lcd.write((unsigned char)0);
    lcd.setCursor(i,1);
    lcd.write((unsigned char)0);
  }
}
byte red_ind[3]={240,20,20};
byte green_ind[3]={40,120,40};

int angle=0;
String data;
bool isFuelLow=true;
byte fuelLevel=0;
//unsigned long del=0;
//unsigned long update_delay=10;//ms


void setup() {
  Serial.begin(115200);

  lcd.begin(16, 2);
  lcd.createChar(0, black_square);
  setlcdlevel(6);
  lcd.setRGB(40, 120, 40);

  
  display.setBrightness(0x0f);
  //add reverse read before;
  drawGas(100,false);
  
  pinMode(LED_PIN, OUTPUT); 
  
  Wire.begin();
  Wire.setClock(400000);
  Serial.println(F("Initializing I2C devices..."));
  mpu.initialize();
  pinMode(INTERRUPT_PIN, INPUT);
  
  // verify connection
  Serial.println(F("Testing device connections..."));
  Serial.println(mpu.testConnection() ? F("MPU6050 connection successful") : F("MPU6050 connection failed"));

  // load and configure the DMP
  Serial.println(F("Initializing DMP..."));
  devStatus = mpu.dmpInitialize();

  // supply your own gyro offsets here, scaled for min sensitivity
  mpu.setXGyroOffset(220);
  mpu.setYGyroOffset(76);
  mpu.setZGyroOffset(-85);
  mpu.setZAccelOffset(1788); // 1688 factory default for my test chip

  // make sure it worked (returns 0 if so)
  if (devStatus == 0) {
      // Calibration Time: generate offsets and calibrate our MPU6050
      mpu.CalibrateAccel(6);
      mpu.CalibrateGyro(6);
      mpu.PrintActiveOffsets();
      // turn on the DMP, now that it's ready
      Serial.println(F("Enabling DMP..."));
      mpu.setDMPEnabled(true);

      // enable Arduino interrupt detection
      Serial.print(F("Enabling interrupt detection (Arduino external interrupt "));
      Serial.print(digitalPinToInterrupt(INTERRUPT_PIN));
      Serial.println(F(")..."));
      attachInterrupt(digitalPinToInterrupt(INTERRUPT_PIN), dmpDataReady, RISING);
      mpuIntStatus = mpu.getIntStatus();

      // set our DMP Ready flag so the main loop() function knows it's okay to use it
      Serial.println(F("DMP ready! Waiting for first interrupt..."));
      dmpReady = true;

      // get expected DMP packet size for later comparison
      packetSize = mpu.dmpGetFIFOPacketSize();
  } else {
      // ERROR!
      // 1 = initial memory load failed
      // 2 = DMP configuration updates failed
      // (if it's going to break, usually the code will be 1)
      Serial.print(F("DMP Initialization failed (code "));
      Serial.print(devStatus);
      Serial.println(F(")"));
  }
}

void loop() {
    if (!dmpReady) return;

    // wait for MPU interrupt or extra packet(s) available
    while (!mpuInterrupt && fifoCount < packetSize) {
        if (mpuInterrupt && fifoCount < packetSize) {
          // try to get out of the infinite loop 
          fifoCount = mpu.getFIFOCount();
        }
        //other program here
        //Serial.println();
        
        //add reverse read
        
        //add enable read
        
        //gas angle management
        angle=int(round(ypr[0] * 180/M_PI));
        angle=constrain(angle,-65,-5);
        angle=map(angle,-65,-5,0,100);
        drawGas(angle,false);
        Serial.print("g:");
        Serial.println(angle);

        //reading management
        if(Serial.available()>0){
          data=Serial.readStringUntil("\n");
          String type=data.substring(0,2);
          String value=data.substring(2);
          int number=value.toInt();
          if(type.charAt(0)=='f'){
             //fuel management 
             if(number<30){
              isFuelLow=true;
              lcd.setRGB(red_ind[0], red_ind[1], red_ind[2]);
             }
             else if(number>31){
              isFuelLow=false;
              lcd.setRGB(green_ind[0], green_ind[1], green_ind[2]);
             }
             fuelLevel=byte(map(number,0,100,0,16));             
             setlcdlevel(fuelLevel);
             //add lcd segment
          }
        }
        //while((millis()-del)<update_delay){}
        //del=millis();
    }

    // reset interrupt flag and get INT_STATUS byte
    mpuInterrupt = false;
    mpuIntStatus = mpu.getIntStatus();

    // get current FIFO count
    fifoCount = mpu.getFIFOCount();
    if(fifoCount < packetSize){
            //Lets go back and wait for another interrupt. We shouldn't be here, we got an interrupt from another event
        // This is blocking so don't do it   while (fifoCount < packetSize) fifoCount = mpu.getFIFOCount();
    }
    // check for overflow (this should never happen unless our code is too inefficient)
    else if ((mpuIntStatus & _BV(MPU6050_INTERRUPT_FIFO_OFLOW_BIT)) || fifoCount >= 1024) {
        // reset so we can continue cleanly
        mpu.resetFIFO();
      //  fifoCount = mpu.getFIFOCount();  // will be zero after reset no need to ask
        Serial.println(F("FIFO overflow!"));

    // otherwise, check for DMP data ready interrupt (this should happen frequently)
    } else if (mpuIntStatus & _BV(MPU6050_INTERRUPT_DMP_INT_BIT)) {
          // read a packet from FIFO
      while(fifoCount >= packetSize){ // Lets catch up to NOW, someone is using the dreaded delay()!
        mpu.getFIFOBytes(fifoBuffer, packetSize);
        // track FIFO count here in case there is > 1 packet available
        // (this lets us immediately read more without waiting for an interrupt)
        fifoCount -= packetSize;
      }
      // display Euler angles in degrees
      mpu.dmpGetQuaternion(&q, fifoBuffer);
      mpu.dmpGetGravity(&gravity, &q);
      mpu.dmpGetYawPitchRoll(ypr, &q, &gravity);
//      Serial.print("ypr\t");
//      Serial.print(ypr[0] * 180/M_PI);
//      Serial.print("\t");
//      Serial.print(ypr[1] * 180/M_PI);
//      Serial.print("\t");
//      Serial.println(ypr[2] * 180/M_PI);
    }
}
