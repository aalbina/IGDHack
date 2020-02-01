using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detail : MonoBehaviour
{
    public string type;
    public Ship ship;

    float accel;
    bool grabbed = false;

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!this.grabbed)
		{
            accel = 10;
            transform.Rotate(accel * Time.deltaTime, 0, 0);
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
