using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CargoShip : MonoBehaviour
{
    Ship ship;

    //TESTING
    public GameObject upgradeCamera;
    void Start()
    {
        ship = GetComponent<Ship>();
        ship.OnArrival += OnCargoShipArrived;
        upgradeCamera = GameObject.Find("Upgrade_Camera");
    }

    private void OnCargoShipArrived(Ship ship)
    {
        upgradeCamera.GetComponent<Camera>().enabled = true;
    }

    void OnDisable()
    {
        ship.OnArrival -= OnCargoShipArrived;
    }
}
