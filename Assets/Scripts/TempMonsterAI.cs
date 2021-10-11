using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempMonsterAI : MonoBehaviour
{
    public Transform target;
    [SerializeField] float maxDist = 10;
    [SerializeField] float minDist = 2;

    private GameObject[] multipleShips;
    public Transform closestShip;
    public bool shipContact;

    [SerializeField] float life;
    private float timer;
    private bool switchTarget = true;
    // Start is called before the first frame update
    void Start()
    {
        closestShip = null;
        shipContact = false;

        closestShip = GetClosestShip();
        GetComponent<Movements>().target = closestShip;
        GetComponent<TempMonsterAI>().target = closestShip;
        if (closestShip != null)
            closestShip.gameObject.GetComponentInChildren<Renderer>().material.color = Color.yellow;
    }
    public Transform GetClosestShip()
    {
        multipleShips = GameObject.FindGameObjectsWithTag("Ship");
        float closestDistance = Mathf.Infinity;
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
            if (Vector3.Distance(transform.position, target.position) <= maxDist)
            {
                if (Vector3.Distance(transform.position, target.position) <= minDist)
                {
                    Debug.Log("attack");
                    OnTargetDeath();
                    target.gameObject.GetComponent<Ship>().UnderAttack();


                    GetComponent<Movements>().isMoving = false;
                    return;
                }
                GetComponent<Movements>().isMoving = true;
            }
        }
        else
        {
            OnTargetDeath();
        }

    }

    public void OnTargetDeath()
    {
        if (switchTarget)
        {
            closestShip = GetClosestShip();
            GetComponent<Movements>().target = closestShip;
            GetComponent<TempMonsterAI>().target = closestShip;
            if (closestShip != null)
            {
                closestShip.gameObject.GetComponentInChildren<Renderer>().material.color = Color.yellow;
            }
            else
                Destroy(gameObject, 0.6f);
        }
        else       //kill
        {
            Destroy(gameObject, 1f);
        }
    }
}
