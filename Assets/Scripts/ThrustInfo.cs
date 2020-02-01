using UnityEngine;
using UnityEngine.UI;

public class ThrustInfo : MonoBehaviour
{
    public Ship ship;
    public Text thrustInfo;

    // Update is called once per frame
    void Update()
    {
        this.thrustInfo.text = "Thrust: " + ((int)ship.accelerationValue).ToString();
    }
}
