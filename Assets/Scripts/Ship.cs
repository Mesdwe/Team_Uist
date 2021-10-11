using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{
    [SerializeField] float health;
    [SerializeField] float defaultSpeed;
    private float currentSpeed;
    private Movements movements;
    private void Start()
    {
        currentSpeed = defaultSpeed;
        movements = GetComponent<Movements>();
        movements.SetSpeed(defaultSpeed);
    }
    public void OnArrival()
    {
        //add RP
        //visual effects
        //sound effect
        //stop
        GetComponent<Movements>().isMoving = false;
        Destroy(gameObject, 1f);
    }

    public void UnderAttack()
    {
        GetComponentInChildren<Renderer>().material.color = Color.yellow;
        GetComponent<Movements>().isMoving = false;
        Destroy(gameObject, 0.5f);
    }

    public void SpeedUp()
    {
        float newSpeed = defaultSpeed * 2f;
        currentSpeed = newSpeed;
        movements.SetSpeed(currentSpeed);
    }
    public void ResetShip()
    {
        currentSpeed = defaultSpeed;
        movements.SetSpeed(currentSpeed);
    }
}
