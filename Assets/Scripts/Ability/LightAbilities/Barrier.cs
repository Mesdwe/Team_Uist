using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Barrier : MonoBehaviour
{
    private NavMeshObstacle navMeshObstacle;
    BarrierSkill barrierSkill;
    private void Start()
    {
        navMeshObstacle = GetComponent<NavMeshObstacle>();


    }

    public void SetBarrierSkill(BarrierSkill bs)
    {
        barrierSkill = bs;
        StartCoroutine(DestroyBarrier());
    }
    IEnumerator DestroyBarrier()
    {
        yield return new WaitForSeconds(3f);
        barrierSkill.barrierCount--;
        Destroy(gameObject);
    }
}
