using UnityEngine;
using System;
using UnityEngine.AI;
public class Ship : MonoBehaviour
{
    [SerializeField] float health;
    [SerializeField] float defaultSpeed;
    [SerializeField] float underAttackSpeed;
    private float currentSpeed;
    //private Movements movements;

    public event Action OnDeath;
    public event Action OnArrival;

    public static Action<Ship> OnHealthAdded;
    public static Action<Ship> OnHealthRemoved;
    public event Action<float> OnHealthPctChanged;
    public float CurrentHealth { get; private set; }

    NavMeshAgent agent;
    private Transform target;

    private void OnEnable()
    {
        OnArrival += ArrivedDock;
        OnDeath += DestroyShip;
        CurrentHealth = health;
        OnHealthAdded?.Invoke(this);

        Player.Instance.SetShip(this);
    }

    private void Start()
    {
        currentSpeed = defaultSpeed;
        //movements = GetComponent<Movements>();
        //movements.SetSpeed(defaultSpeed);
        agent = GetComponent<NavMeshAgent>();
        agent.speed = defaultSpeed;
    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Dock"))
        {
            OnArrival?.Invoke();
        }
    }
    void Update()
    {
        ShipMovement();
    }
    public void SetSpeed(float newSpeed)
    {
        agent.speed = newSpeed;
    }
    public void SetTargetTransform(Transform tar)
    {
        target = tar;
    }
    private void ShipMovement()
    {
        float distance = Vector3.Distance(target.position, transform.position);
        agent.SetDestination(target.position);
        // if (distance <= agent.stoppingDistance)
        // {
        //     //effect on arrive docks
        //     OnArrival?.Invoke();
        // }
    }
    public float UnderAttackSpeed()
    {
        return underAttackSpeed;
    }
    public void UnderAttack(float damage)
    {
        currentSpeed = underAttackSpeed;
        agent.speed = currentSpeed;
        ModifyHealth(-damage);
        if (CurrentHealth <= 0f)
        {
            OnDeath?.Invoke();
        }
    }

    public void ArrivedDock()
    {
        //GetComponent<Movements>().isMoving = false;
        agent.speed = 0;
        gameObject.tag = "Untagged";
        //disappear effect
        Destroy(gameObject);
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
        agent.speed = currentSpeed;
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
        // movements.SetSpeed(currentSpeed);
        agent.speed = currentSpeed;

    }
    private void OnDisable()
    {
        OnHealthRemoved?.Invoke(this);
        OnArrival -= Player.Instance.HandleRPChanged;
    }
}
