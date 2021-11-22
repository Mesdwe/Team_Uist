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
    public float wtf;


    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            GameObject instFoam = Instantiate(bullet, transform.position, Quaternion.identity);
             Rigidbody instFoamRB = instFoam.GetComponent<Rigidbody>();
 
             instFoamRB.AddForce(-transform.GetChild(0).right * 500000);
             instFoamRB.AddForce(Physics.gravity * wtf);
        }
    }

}
