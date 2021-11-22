using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterD : MonoBehaviour
{
    [SerializeField] float health;
    [SerializeField] float speed;
    [SerializeField] float damageDealt;
    [SerializeField] float hitChance;
    [SerializeField] GameObject bullet;
    [SerializeField] int minShot;
    [SerializeField] int maxShot;
    private int currentShotCount;
    private int shots;
    public float wtf;
    public Transform targetLighthouse;
    public Transform missShot;

    private bool isActive;

    [SerializeField] float shootSpeed;
    void Start()
    {
        // shots = Random.Range(minShot, maxShot);
        // isActive = true;
        // transform.LookAt(targetLighthouse);
        // StartCoroutine(Shoot());
    }

    public void InitMonster(Transform tLT, Transform mST)
    {
        targetLighthouse = tLT;
        missShot = mST;
        shots = Random.Range(minShot, maxShot);
        isActive = true;
        transform.LookAt(targetLighthouse);
        StartCoroutine(Shoot());
    }
    IEnumerator Shoot()
    {
        while (isActive)
        {
            yield return new WaitForSeconds(shootSpeed);
            GameObject bullete = Instantiate(bullet, transform.position, Quaternion.identity);
            currentShotCount++;
            if (Random.value > 0.7f)
                bullete.GetComponent<MonsterBullet>().SetStartPos(transform.position, targetLighthouse.position);
            else
                bullete.GetComponent<MonsterBullet>().SetStartPos(transform.position, missShot.position);

            if (currentShotCount == shots)
            {

                isActive = false;

                //Change it later
                Destroy(gameObject);
            }

        }
    }


}
