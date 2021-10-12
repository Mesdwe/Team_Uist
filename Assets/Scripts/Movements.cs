using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movements : MonoBehaviour
{
    private float speed;
    private Transform target;
    public bool isMoving;

    void Update()
    {
        if (isMoving && target != null)
        {
            transform.LookAt(target);
            float step = speed * Time.deltaTime / 200f;
            transform.position = Vector3.MoveTowards(transform.position, target.position, step);
        }
    }
    public void SetSpeed(float newSpeed)
    {
        speed = newSpeed;
    }
    public void SetTargetTransform(Transform tar)
    {
        target = tar;
    }

}
