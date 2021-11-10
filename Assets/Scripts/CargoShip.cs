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
        upgradeCamera = GameObject.Find("GB_Camera");
        GameManager.Instance.gameState = GameState.Upgrade;
    }

    private void OnCargoShipArrived(Ship ship)
    {
        //upgradeCamera.GetComponent<Camera>().enabled = true;
        //LevelManager.Instance.StartUpgrade();
    }

    void OnDisable()
    {
        ship.OnArrival -= OnCargoShipArrived;
    }
}
