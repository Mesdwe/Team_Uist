using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipRadar : MonoBehaviour
{
    private GameObject[] multipleShips;
    public Transform closestShip;
    public bool shipContact;

    // Start is called before the first frame update
    void Start()
    {
        closestShip = null;
        shipContact = false;
    }

    // Update is called once per frame
    void Update()
    {
        closestShip = GetClosestShip();
        GetComponent<Movements>().target = closestShip;
        GetComponent<TempMonsterAI>().target = closestShip;
        closestShip.gameObject.GetComponentInChildren<Renderer>().material.color = Color.red;
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
}
