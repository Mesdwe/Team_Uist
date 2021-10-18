using UnityEngine;
using System;
public class Ship : MonoBehaviour
{
    [SerializeField] float health;
    [SerializeField] float defaultSpeed;
    public float currentSpeed;
    private Movements movements;

    public event Action OnDeath;
    public event Action OnArrival;
    private void OnEnable()
    {
        OnArrival += ArrivedDock;
        OnDeath += DestroyShip;
    }
    private void Start()
    {
        currentSpeed = defaultSpeed;
        movements = GetComponent<Movements>();
        movements.SetSpeed(defaultSpeed);
    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Dock"))
        {
            OnArrival?.Invoke();
        }
    }

    public void UnderAttack(float damage)
    {
        Debug.Log("ATTACKED");
        GetComponentInChildren<Renderer>().material.color = Color.yellow;
        GetComponent<Movements>().isMoving = false;
        health -= damage;
        if (health <= 0f)
        {
            Debug.Log("SHIP DEAD");
            OnDeath?.Invoke();
        }
    }

    public void ArrivedDock()
    {
        GetComponent<Movements>().isMoving = false;
        Destroy(gameObject, 1f);
    }
    public void DestroyShip()
    {
        //Temp
        gameObject.tag = "Untagged";
        Destroy(gameObject);
    }
    public void SpeedUp()
    {
        Debug.Log("Speed Up");
        float newSpeed = defaultSpeed * 1.25f;
        currentSpeed = newSpeed;
        movements.SetSpeed(currentSpeed);
    }

    public void HealShip()
    {

    }
    public void ResetShip()
    {
        currentSpeed = defaultSpeed;
        movements.SetSpeed(currentSpeed);
    }
}
