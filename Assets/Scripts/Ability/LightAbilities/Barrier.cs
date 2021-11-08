using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Barrier : MonoBehaviour
{
    private NavMeshObstacle navMeshObstacle;
    BarrierSkill barrierSkill;
    //LightAbilityHolder lightAbilityHolder;
    private void Start()
    {
        navMeshObstacle = GetComponent<NavMeshObstacle>();


    }

    public void SetBarrierSkill(LightAbilityHolder lh)
    {

        StartCoroutine(DestroyBarrier(lh));
    }
    IEnumerator DestroyBarrier(LightAbilityHolder lh)
    {
        yield return new WaitForSeconds(3f);
        //barrierSkill.barrierCount--;
        lh.barrierCount--;
        Destroy(gameObject);
    }
}
