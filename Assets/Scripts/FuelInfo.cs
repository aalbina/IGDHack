using UnityEngine;
using UnityEngine.UI;

public class FuelInfo : MonoBehaviour
{
    public Ship ship;
    public Text fuelInfo;

    // Update is called once per frame
    void Update()
    {
        this.fuelInfo.text = "Fuel: " + ((int)ship.fuel).ToString();
    }
}
