using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{
    public controllerComm controller;
    public Rigidbody rb;

    public float forwardForce = 0.1f;
    public float sidewayForce = 1f;

    public float fuel = 500f;
    public float accelerationValue = 0;
    public Detail detail;
    public Station station;


    // Update is called once per frame
    void FixedUpdate()
    {
        if (this.IsDetailGrabbed())
        {
            this.detail.transform.position = this.transform.position + this.transform.forward * 2;
        }
        this.fuel = this.fuel - (accelerationValue/5 * Time.deltaTime);
        
    }

    void Update()
    {
        // if (Input.GetKey(KeyCode.W) && accelerationValue < 100)
        // {
        //     this.accelerationValue += 1;
        // }
        this.accelerationValue=controller.getGasValue();
        if (Input.GetKey(KeyCode.S) && accelerationValue >0)
        {
            this.accelerationValue -= 1;
        }
    }

    public void Refuel(Coffee coffee)
    {
        fuel += coffee.capacity;
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
