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
        serialPort.PortName="/dev/ttyUSB0";
        serialPort.BaudRate=115200;
        for(int i=0;i<15;i++){
            serialPort.ReadTo("\n");
        }
    }

    // Update is called once per frame
    void Update()
    {
        data=serialPort.ReadTo("\n");
        string type=data.Substring(0,2);
        string value=data.Substring(2);
        int number=int.Parse(value);
        if(type.Contains("g")){
            this.gasValue=number;
        }
        else if(type.Contains("s")){
            this.steeringValue=number;
        }
        else if(type.Contains("e")){
            this.on=(number==1);
        }
        else if(type.Contains("r")){//if reverse is 0 - move forward
            this.forward=(number==0);
        }
    }

    public void setFuelLevel(int level){
        string msg="f:"+level.ToString();
        serialPort.WriteLine(msg);
    }
}
