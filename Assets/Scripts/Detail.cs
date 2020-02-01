using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detail : MonoBehaviour
{
    public string type;
    public Ship ship;

    float accelx;
    bool grabbed = false;

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!this.grabbed)
		{
            accelx = 10;
            transform.Rotate(accelx * Time.deltaTime, accelx * Time.deltaTime, accelx * Time.deltaTime);
        }
    }

    void OnTriggerEnter(Collider collider)
    {
        string colliderName = collider.name;

        if (colliderName == "Ship")
        {
            if (!ship.IsDetailGrabbed() && !this.grabbed)
            {
                this.grabbed = true;
                ship.GrabbDetail(this);
			}
        }
    }
}
