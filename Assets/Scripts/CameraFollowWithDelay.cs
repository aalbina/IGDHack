using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowWithDelay : MonoBehaviour
{
    public Transform target;
    public Ship ship;
    [HideInInspector] public float cameraFollowSpeed = 60f;
    public Vector3 offset;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(ship.accelerationValue > 30f)
        {
            cameraFollowSpeed = ship.accelerationValue * 2f * Time.deltaTime;
        }
        transform.position = Vector3.Lerp(transform.position, target.position + offset, cameraFollowSpeed);
    }
}
