using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    [SerializeField] float health;
    [SerializeField] float speed;
    [SerializeField] float damageDealt;
    public float spawnChances;

    private Movements movements;
    private void Start()
    {
        movements = GetComponent<Movements>();
        movements.SetSpeed(speed);
    }
}
