using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;
public class controllerComm : MonoBehaviour
{
    // Start is called before the first frame update
    SerialPort serialPort = new SerialPort();
    string data;
    int gasValue;
    int steeringValue;
    bool on;
    bool forward;
    bool init=false;
    public string comPortName;
    public bool isForward(){
        return this.forward;
    }
    public bool isOn()
    {
        return this.on;
    }

    public int getGasValue()
    {
        return this.gasValue;
    }

    public int getSteeringValue()
    {
        return this.steeringValue;
    }

    void Start()
    {
        this.gasValue=0;
        this.steeringValue=0;
        serialPort.PortName=this.comPortName;
        serialPort.BaudRate=115200;
        serialPort.Open();
        for(int i=0;i<20;i++){
            serialPort.ReadTo("\n");
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        data=serialPort.ReadTo("\n");
        //string type=data.Substring(0,2);
        string value=data.Substring(2);
        int number=int.Parse(value);
        if(data[0]=='g'){
            this.gasValue=number;
        }
        else if(data[0]=='s'){
            this.steeringValue=number;
        }
        else if(data[0]=='e'){
            this.on=(number==1);
        }
        else if(data[0]=='r'){//if reverse is 0 - move forward
            this.forward=(number==0);
        }        
    }

    public void setFuelLevel(int level){
        string msg="f:"+level.ToString();
        serialPort.WriteLine(msg);
    }
}
