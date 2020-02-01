using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{
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

        Debug.Log(this.fuel);
        this.fuel = this.fuel - (accelerationValue * Time.deltaTime) ;
        Debug.Log(this.fuel);
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
}
