using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FuelInfo : MonoBehaviour
{
    public Ship ship;
    public Text fuelInfo;

    // Update is called once per frame
    void Update()
    {
        this.fuelInfo.text = "Fuel: " + ((int)ship.fuel).ToString();

        if ((int)ship.fuel == 0)
		{
            this.fuelInfo.color = Color.red;
            SceneManager.LoadScene(3);
        }
    }
}
