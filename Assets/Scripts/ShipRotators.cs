using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipRotators : MonoBehaviour
{
    float accel;
    public Ship ship;

    // Update is called once per frame
    void FixedUpdate()
    {
        accel = 10 * accelerationValue;
        transform.Rotate(0, accel * Time.deltaTime, 0);
    }
}
