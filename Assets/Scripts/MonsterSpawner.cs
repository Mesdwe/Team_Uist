using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    [SerializeField] SpawnPoint[] spawnPoints;
    [SerializeField] GameObject[] monsters;
    //temp
    [SerializeField] int monsterCount;
    //int curShipIndex = 0;
    [SerializeField] float minSpawnTime;
    [SerializeField] float maxSpawnTime;

    void Start()
    {
        StartCoroutine(MonsterInit());
    }
    IEnumerator MonsterInit()
    {
        yield return new WaitForSeconds(5f);
        StartCoroutine(SpawnMonster());
    }
    IEnumerator SpawnMonster()
    {
        float spawnTime = Random.Range(minSpawnTime, maxSpawnTime);
        yield return new WaitForSeconds(spawnTime);

        SpawnPoint spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        GameObject monster = GetSpawnMonster(Random.value);
        spawnPoint.SpawnTarget(monster);
        if (this.enabled)
            StartCoroutine(SpawnMonster());
    }

    GameObject GetSpawnMonster(float value)
    {
        if (value > 0.8f)
        {
            return monsters[2];        //Monster A
        }
        if (value > 0.3f)
        {
            return monsters[0];    //Monster B
        }
        else
        {
            return monsters[1];    //Monster C
        }
    }
}
