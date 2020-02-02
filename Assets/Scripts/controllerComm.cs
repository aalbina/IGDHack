using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.ComponentModel;
using System.IO.Ports;
using System;
public class controllerComm : MonoBehaviour
{
    // Start is called before the first frame update
    public SerialController serialController;
    int gasValue;
    int steeringValue = 5;
    bool on;
    bool forward;
    bool init=false;
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

    public void setFuelLevel(int level){
        string msg="f:"+level.ToString();
        serialController.SendSerialMessage(msg);
    }
    
    void OnMessageArrived(string msg)
    {          
        string data=msg;
        if(on){
            string value = data.Substring(2);
            int number = int.Parse(value);
            if(data.Contains("g")){                    
                this.gasValue=number;
            }
            else if(data.Contains("s")){
                this.steeringValue=number;
            }
            else if(data.Contains("e")){
                this.on=(number==1);
            }
            //if reverse is 0 - move forward
            else if(data.Contains("r")){
                this.forward=(number==0);
            }
        }        
        else if(data.Contains("e:1")){
            this.on=true;
        }
    }
    void OnConnectionEvent(){

    }
}
