using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{
    public controllerComm controller;
    public Rigidbody rb;

    public bool isControllerEnabled;

    [HideInInspector] public float fuel = 160f;
    private float fuelMax = 480f;
    private float fuelForDisplay = 0f;
    public float accelerationValue = 0;
    public Detail detail;
    public Station station;

    void Start()
    {
        fuel=480f;    
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (this.IsDetailGrabbed())
        {
            this.detail.transform.position = this.transform.position + this.transform.forward * 2;
        }

    }

    void Update()
    {
        if (isControllerEnabled)
        {
            this.accelerationValue = controller.getGasValue();

        }
        else
        {
            if (Input.GetKey(KeyCode.W) && accelerationValue < 100)
            {
                this.accelerationValue += 1;
            }
        }

        this.fuel = this.fuel - (accelerationValue / 10 * Time.deltaTime);
        if (isControllerEnabled)
        {
            int newFuelForDisplay = (int)fuel / 30;
            if ((fuelForDisplay != newFuelForDisplay) && (newFuelForDisplay >= 0))
            {
                fuelForDisplay=newFuelForDisplay;
                controller.setFuelLevel(newFuelForDisplay);
            }
        }
    
    
    

        if (Input.GetKey(KeyCode.S) && accelerationValue >0)
        {
            this.accelerationValue -= 1;
        }
    }

    public void Refuel(Coffee coffee)
    {
        fuel += coffee.capacity;
        if (fuel > fuelMax)
        {
            fuel = fuelMax;
        }
    }

    public void GrabbDetail(Detail detail)
    {
        if (!this.detail)
        {
            this.detail = detail;
            detail.transform.position = this.transform.position + this.transform.forward * 2;
        }
    }

    public bool IsDetailGrabbed()
    {
        return this.detail != null;
    }

    public float GetAccelerationValue()
    {
        return this.accelerationValue;
    }
}
