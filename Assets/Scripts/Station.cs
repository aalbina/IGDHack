using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Station : MonoBehaviour
{
    public HashSet<Detail> details = new HashSet<Detail>();
    public Ship ship;
    public Coffee coffee;
    public Coffee coffee1;
    public Coffee coffee2;
    public Coffee coffee3;
    public string requiredDetailType;
    int maxDetails = 1;
    private bool isCompleted;


    public void AttachDetail(Ship ship)
	{
        if (ship.detail && details.Count < maxDetails && ship.detail.type.Equals(requiredDetailType))
        {
            Detail detail = ship.detail;
            ship.detail = null;

            details.Add(detail);
            detail.transform.position = this.transform.position;
            isCompleted = true;
            GenerateFuel();
		}
	}

    void GenerateFuel()
	{
        coffee.Show();
        coffee1.Show();
        coffee2.Show();
        coffee3.Show();
    }

    public bool IsSetFull()
	{
        return details.Count < maxDetails;

    }

    void OnTriggerEnter(Collider collider)
    {
        string colliderName = collider.name;

        if (colliderName == "Ship")
        {
            this.AttachDetail(ship);
        }
    }
}
