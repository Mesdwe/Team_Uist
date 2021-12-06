using UnityEngine;
using System;
using System.Threading.Tasks;
using UnityEngine.AI;
using System.Collections;
public class Ship : MonoBehaviour
{
    [SerializeField] float health;
    [SerializeField] float defaultSpeed;
    [SerializeField] float underAttackSpeed;
    private float currentSpeed;
    //private Movements movements;

    public event Action<Ship> OnDeath;
    public event Action<Ship> OnArrival;

    public static Action<Ship> OnHealthAdded;
    public static Action<Ship> OnHealthRemoved;
    public event Action<float> OnHealthPctChanged;
    public float CurrentHealth { get; private set; }

    NavMeshAgent agent;
    private Transform target;

    public ShipRewards shipRewards;
    private bool healing;

    private AudioController audioController;

    [SerializeField] GameObject effect;
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

        audioController = GetComponent<AudioController>();
    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Dock"))
        {
            OnArrival?.Invoke(this);
        }
    }

    public float GetCurrentHealthPct()
    {
        return CurrentHealth / health;
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
            OnDeath?.Invoke(this);
        }
    }

    public void ArrivedDock(Ship ship)  //Docked
    {
        OnHealthRemoved?.Invoke(this);
        DestroyShipObject();
        effect.SetActive(true);
        //GetComponent<Movements>().isMoving = false;
        agent.speed = 0;
        gameObject.tag = "Untagged";
        //disappear effect
        audioController.SetAudioClip(0);
        audioController.PlayAudioClip();
    }
    public async void DestroyShip(Ship ship)  //Destroyed by monster
    {
        OnHealthRemoved?.Invoke(this);

        //DestroyShipObject();
        //Temp
        agent.speed = 0;
        agent.velocity = Vector3.zero;
        gameObject.tag = "Untagged";
        audioController.SetAudioClip(1);
        audioController.PlayAudioClip();

        //Vector3 rotator = new Vector3(0, 0, 1);
        Transform target = transform.GetChild(0);

        //Rotate
        var endRotate = Time.time + 0.5f;
        while (Time.time < endRotate)
        {
            //rotator.z += Time.deltaTime;
            target.Rotate(new Vector3(0, 0, 1) * Time.deltaTime * -100);
            await Task.Yield();
        }
        var end = Time.time + 1.5f;
        while (Time.time < end)
        {
            //Transform target = transform.GetChild(0);
            //rotator.z += Time.deltaTime * 10f;

            target.position += new Vector3(0, -1, 0) * Time.deltaTime * 200;
            //target.Rotate(rotator, Space.Self);
            await Task.Yield();
        }

        Destroy(gameObject);
        //
    }

    private void DestroyShipObject()    //Destroy the ship
    {
        Destroy(gameObject, 0.5f);
    }
    public void SpeedUp(float increase)
    {
        Debug.Log("Speed Up");
        float newSpeed = currentSpeed * (1 + increase);
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
        healing = false;

    }

    private void OnDisable()
    {
        //OnHealthRemoved?.Invoke(this);
        if (Player.Instance != null)
        {
            OnArrival -= Player.Instance.HandleRPChanged;
            OnArrival -= Player.Instance.SavedShip;
            OnDeath -= Player.Instance.LostShip;
        }

        OnDeath -= DestroyShip;
        if (GameObject.FindGameObjectsWithTag("Ship").Length == 0)
        {
            if (LevelManager.Instance != null)
            {
                if (!LevelManager.Instance.waveOver)
                    return;
                if (!LevelManager.Instance.GetApplicationIsQuitting())
                    LevelManager.Instance.NextWave();   //triggered when close game
            }
        }
    }

    public void HealShips(float healValue)
    {
        if (healing)
            return;
        healing = true;
        StartCoroutine(HealingShip(healValue));
    }

    private IEnumerator HealingShip(float healValue)
    {
        while (healing)
        {
            yield return new WaitForSeconds(0.2f);
            if (healing)
            {
                Debug.Log("HEALED");
                ModifyHealth(+healValue);
            }
        }
    }


}
