using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] Transform missTarget;

    public void SpawnTarget(GameObject ship)
    {
        GameObject go = Instantiate(ship, transform);
        //Movements movements = go.GetComponent<Movements>();
        Ship shipT = go.GetComponent<Ship>();
        if (shipT != null)
        {
            shipT.SetTargetTransform(target);
            return;
        }
        MonsterD monsterT = go.GetComponent<MonsterD>();
        if (monsterT != null)
        {
            monsterT.InitMonster(target, missTarget);
            return;
        }
    }
}
