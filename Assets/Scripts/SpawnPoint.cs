using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    [SerializeField] Transform target;
    public void SpawnTarget(GameObject ship)
    {
        GameObject go = Instantiate(ship, transform);
        Movements movements = go.GetComponent<Movements>();
        if (movements!=null)
            go.GetComponent<Movements>().SetTargetTransform(target);
    }
}
