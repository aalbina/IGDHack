using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coffee : MonoBehaviour
{
    public int capacity;
    public Ship ship;

    private bool grabbed;

    void Start()
	{
        this.GetComponent<Renderer>().enabled = false;
	}

    public void Show()
	{
        this.grabbed = false;
        this.GetComponent<Renderer>().enabled = !this.GetComponent<Renderer>().enabled;
    }

    void OnTriggerEnter(Collider collider)
    {
        string colliderName = collider.name;

        if (colliderName == "Ship" && !this.grabbed)
        {
            ship.Refuel(this);
            this.GetComponent<Renderer>().enabled = !this.GetComponent<Renderer>().enabled;
            this.grabbed = true;
        }
    }
}
