using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class ShipMovement : MonoBehaviour
{
    public float speedCoef;
    public float maneuvreSpeed;
    public Rigidbody rb;
    public Vector2 movement;
    public Ship ship;

    public controllerComm controllerComm;

    public float maxVelocityConstraint;
    // Start is called before the first frame update
    void Start()
    {
        ship = this.GetComponent<Ship>();
        rb = this.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()    { 
        bool isShipActive = controllerComm.isOn();
        if(!isShipActive){
            rb.velocity = rb.velocity * 0.92f;
            return;
        }
        if (Input.GetKey(KeyCode.S))
        {
            rb.AddForce(-rb.transform.forward * maneuvreSpeed * Time.deltaTime, ForceMode.Force);

        }
        // rotate
        if(ship.isControllerEnabled){
            SteeringWithController();
        }else{
            SteeringWithWasd();
        }

        if (ship.GetAccelerationValue()< 5f)
        {
            rb.velocity = rb.velocity * 0.92f;
        }
        //Ускоряемся на значение акселерации.
        if(rb.velocity.magnitude < maxVelocityConstraint){
            rb.AddForce(rb.transform.forward * ship.GetAccelerationValue()* speedCoef * Time.deltaTime, ForceMode.Force);
        }
        else{
            float vel=maxVelocityConstraint/rb.velocity.magnitude;
            rb.velocity*= vel;
        }
    }

    private void SteeringWithController(){
        if(controllerComm.getSteeringValue() == 0){
            rb.transform.Rotate(0, -1f, 0);
            rb.AddForce(-rb.transform.right * maneuvreSpeed * Time.deltaTime, ForceMode.Force);
            return;
        } 
        if(controllerComm.getSteeringValue() == 10){
            rb.transform.Rotate(0, 1f, 0);
            rb.AddForce(rb.transform.right * maneuvreSpeed * Time.deltaTime, ForceMode.Force);
        }
    }

        private void SteeringWithWasd(){
        if(Input.GetKey(KeyCode.A)){
            rb.transform.Rotate(0, -1f, 0);
            rb.AddForce(-rb.transform.right * maneuvreSpeed * Time.deltaTime, ForceMode.Force);
            return;
        } 
        if(Input.GetKey(KeyCode.D)){
            rb.transform.Rotate(0, 1f, 0);
            rb.AddForce(rb.transform.right * maneuvreSpeed * Time.deltaTime, ForceMode.Force);
        }
    }

}
