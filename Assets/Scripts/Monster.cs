using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Monster : MonoBehaviour
{
    [SerializeField] float health;
    [SerializeField] float speed;
    [SerializeField] float damageDealt;
    public float spawnChances;

    //AI part
    NavMeshAgent agent;
    public Transform target;
    [SerializeField] float maxDist = 10;
    [SerializeField] float minDist = 2;

    private GameObject[] multipleShips;
    //public Transform closestShip;
    public bool shipContact;

    [SerializeField] float life;
    private float timer;
    private bool switchTarget = true;
    private bool cooldown = false;


    //private Movements movements;
    void Start()
    {
        //movements = GetComponent<Movements>();
        //movements.SetSpeed(speed);
        agent = GetComponent<NavMeshAgent>();
        shipContact = false;

        FindATarget();
    }
    public Transform GetClosestShip()
    {
        multipleShips = GameObject.FindGameObjectsWithTag("Ship");
        float closestDistance = maxDist;
        Transform trans = null;
        foreach (GameObject go in multipleShips)
        {
            float currentDistance;
            currentDistance = Vector3.Distance(transform.position, go.transform.position);
            if (currentDistance < closestDistance)
            {
                closestDistance = currentDistance;
                trans = go.transform;
            }
        }
        return trans;
    }
    void Update()
    {
        if (timer <= life)
        {
            timer += Time.deltaTime;
        }
        if (timer > life)
        {
            switchTarget = false;
        }
        if (target != null)
        {
            float distance = Vector3.Distance(target.position, transform.position);
            if (distance <= maxDist)
            {
                //transform.LookAt(target);
                agent.SetDestination(target.position);
                if (distance <= agent.stoppingDistance)
                {
                    transform.LookAt(target);
                    Ship targetShip = target.GetComponent<Ship>();

                    agent.speed = targetShip.UnderAttackSpeed();
                    if (!cooldown)
                        StartCoroutine(Attack(target));
                }
            }
        }
    }

    private IEnumerator Attack(Transform tar)
    {
        cooldown = true;
        tar.gameObject.GetComponent<Ship>().UnderAttack(damageDealt);
        yield return new WaitForSeconds(1f);
        cooldown = false;
    }
    private void FindATarget()
    {
        Transform closestShip = GetClosestShip();
        SetTarget(closestShip);
        if (closestShip != null)
        {
            //closestShip.gameObject.GetComponentInChildren<Renderer>().material.color = Color.yellow;
            closestShip.gameObject.GetComponent<Ship>().OnArrival += TargetDisapear;
            closestShip.gameObject.GetComponent<Ship>().OnDeath += TargetDisapear;
            transform.LookAt(closestShip.transform);
        }
        else
            Destroy(gameObject, 0.6f);  //Destroy Effect
    }
    public void TargetDisapear()
    {
        shipContact = false;
        if (switchTarget)
        {
            FindATarget();
        }
        else       //kill
        {
            Destroy(gameObject, 0.6f);
        }
    }

    public void SetTarget(Transform tar)
    {
        target = tar;
        //movements.SetTargetTransform(tar);
    }

}
