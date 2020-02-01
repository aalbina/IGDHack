using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipMovement : MonoBehaviour
{
    public float speedCoef;
    public float maneuvreSpeed;
    public Rigidbody rb;
    public Vector2 movement;
    public Ship ship;

    public float maxVelocityConstraint;
    // Start is called before the first frame update
    void Start()
    {
        ship = this.GetComponent<Ship>();
        rb = this.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    { 
        if (Input.GetKey(KeyCode.S))
        {
            rb.AddForce(-rb.transform.forward * maneuvreSpeed * Time.deltaTime, ForceMode.Force);

        }
        // rotate

        if (Input.GetKey(KeyCode.D))
        {
            rb.transform.Rotate(0, 0.80f, 0);
            rb.AddForce(rb.transform.right * maneuvreSpeed * Time.deltaTime, ForceMode.Force);
     
        }
        if (Input.GetKey(KeyCode.A))
        {
            rb.transform.Rotate(0, -0.80f, 0);
            rb.AddForce(-rb.transform.right * maneuvreSpeed * Time.deltaTime, ForceMode.Force);
            
        }

        if (ship.GetAccelerationValue()< 5f)
        {
            rb.velocity = rb.velocity * 0.92f;
        }
        //Ускоряемся на значение акселерации.
        if(rb.velocity.z<maxVelocityConstraint){
            rb.AddForce(rb.transform.forward * ship.GetAccelerationValue()* speedCoef * Time.deltaTime, ForceMode.Force);
        }
        else{
            float vel=maxVelocityConstraint/rb.velocity.z;
            rb.velocity=new Vector3(0,0,rb.velocity.z*vel);
        }
    }

}
