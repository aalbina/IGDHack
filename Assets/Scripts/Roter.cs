using UnityEngine;

public class Roter : MonoBehaviour
{
    void FixedUpdate()
    {
        transform.Rotate(0, 10 * Time.deltaTime, 0);
    }
}
