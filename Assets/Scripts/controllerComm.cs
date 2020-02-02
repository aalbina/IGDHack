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
    int steeringValue;
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

    void Start(){
        
    }
    
    public void setFuelLevel(int level){
        string msg="f:"+level.ToString();
        serialController.SendSerialMessage(msg);
    }
    
    void OnMessageArrived(string msg)
    {          
        if(on){
            string value=msg.Substring(2);          
            int number=int.Parse(value);
            if(msg[0]=='g'){
                this.gasValue=number;
            }
            else if(msg[0]=='s'){
                this.steeringValue=number;
            }
            else if(msg[0]=='e'){
                this.on=(number==1);
            }
            //if reverse is 0 - move forward
            else if(msg[0]=='r'){
                this.forward=(number==0);
            }
        } 
        else if(msg.Contains("e:1")){
            this.on=true;
        }
    }
    void OnConnectionEvent(){

    }
}
