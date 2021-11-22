using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterBullet : MonoBehaviour
{
    protected float animation;
    private Vector3 startPos;

    public Vector3 target;
    void Start()
    {
        // StartCoroutine(BulletFall());
    }

    public void SetStartPos(Vector3 sp, Vector3 tar)
    {
        startPos = sp;
        target = tar;
    }
    IEnumerator BulletFall()
    {
        yield return new WaitForSeconds(0.5f);
        Debug.Log("FALL");

        GetComponent<Rigidbody>().AddForce(Physics.gravity * 300000f);
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Barrier"))
            Destroy(gameObject);

    }
    void Update()
    {
        animation += Time.deltaTime;
        animation = animation % 3f;
        if (Vector3.Distance(transform.position, target) <= 100f)
        {
            Debug.Log("WTHAFDFSDF");
            Destroy(gameObject);
            return; // object has reached goal 
        }

        transform.position = MathParabola.Parabola(startPos, target, 300f, animation);//Vector3.forward * 100f


    }
}
