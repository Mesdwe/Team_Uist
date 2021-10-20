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

    public static Action<Ship> OnHealthAdded;
    public static Action<Ship> OnHealthRemoved;
    public event Action<float> OnHealthPctChanged;
    public float CurrentHealth { get; private set; }

    private void OnEnable()
    {
        OnArrival += ArrivedDock;
        OnDeath += DestroyShip;
        CurrentHealth = health;
        OnHealthAdded(this);
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
        ModifyHealth(-damage);
        GetComponentInChildren<Renderer>().material.color = Color.yellow;
        GetComponent<Movements>().isMoving = false;
        //health -= damage;
        if (CurrentHealth <= 0f)
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
    public void ModifyHealth(float amount)
    {
        CurrentHealth += amount;
        if (CurrentHealth >= health)
            CurrentHealth = health;
        float currentHealthPct = CurrentHealth / health;
        OnHealthPctChanged?.Invoke(currentHealthPct);
    }

    public void ResetShip()
    {
        currentSpeed = defaultSpeed;
        movements.SetSpeed(currentSpeed);

    }
    private void OnDisable()
    {
        OnHealthRemoved(this);
    }
}
