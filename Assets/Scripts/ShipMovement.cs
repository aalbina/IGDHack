using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipMovement : MonoBehaviour
{
    public float speed;
    public Rigidbody rb;
    public Vector2 movement;

	// Start is called before the first frame update
	void Start()
	{
    	rb = this.GetComponent<Rigidbody>();
	}

	// Update is called once per frame
	void Update()
    {
   	 bool isKeyPressed = false;
   	 if (Input.GetKey(KeyCode.W))
   	 {
   		 rb.AddForce( rb.transform.forward * speed * Time.deltaTime, ForceMode.Force);
   		 //rb.velocity += rb.transform.forward * speed;
   		 isKeyPressed = true;
   	 }
   	 if (Input.GetKey(KeyCode.S))
   	 {
   		 rb.AddForce( -rb.transform.forward * speed * Time.deltaTime, ForceMode.Force);
   		 isKeyPressed = true;
   	 }
   	 // rotate

   	 if (Input.GetKey(KeyCode.D))
   	 {
   		 rb.transform.Rotate(0, 1, 0);//, Space.Self);
   		 isKeyPressed = true;
   	 }
   	 if (Input.GetKey(KeyCode.A))
   	 {

   		 rb.transform.Rotate(0, -1, 0);//, Space.Self);
   		 isKeyPressed = true;
   	 }

   	 if(!isKeyPressed)  
   	 {
   		 rb.velocity = rb.velocity * 0;
   	 }
    }

}
