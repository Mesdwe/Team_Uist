using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Barrier : MonoBehaviour
{
    private NavMeshObstacle navMeshObstacle;


    private void Start()
    {
        navMeshObstacle = GetComponent<NavMeshObstacle>();
        Destroy(gameObject, 5f);
    }
}
