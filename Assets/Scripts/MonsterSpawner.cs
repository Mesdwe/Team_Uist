using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    [SerializeField] SpawnPoint[] spawnPoints;
    [SerializeField] GameObject[] monsters;
    [SerializeField] float minSpawnTime;
    [SerializeField] float maxSpawnTime;
    public Wave currentWave;


    public void MonsterInit(Wave wave)
    {
        currentWave = wave;
        StartCoroutine(WaitForShips());
    }

    IEnumerator WaitForShips()
    {
        yield return new WaitForSeconds(5f);
        StartCoroutine(SpawnMonster());

    }
    IEnumerator SpawnMonster()
    {
        float spawnTime = Random.Range(currentWave.minMonsterSpawnTime, currentWave.maxMonsterSpawnTime);
        yield return new WaitForSeconds(spawnTime);

        SpawnPoint spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        GameObject monster = GetSpawnMonster(Random.value);
        spawnPoint.SpawnTarget(monster);
        if (GameManager.Instance.gameState != GameState.Upgrade)
            StartCoroutine(SpawnMonster());
    }

    GameObject GetSpawnMonster(float value)
    {


        if (currentWave.monsterType.Length == 2)
        {
            if (value > 0.7f)
            {
                return currentWave.monsterType[1];        //Monster B
            }
            else
            {
                return currentWave.monsterType[2];    //Monster C
            }
        }
        else if (currentWave.monsterType.Length == 3)
        {
            if (value > 0.8f)
            {
                return currentWave.monsterType[0];        //Monster A
            }
            if (value > 0.3f)
            {
                return currentWave.monsterType[1];    //Monster B
            }
            else
            {
                return currentWave.monsterType[2];     //Monster C
            }
        }
        else
            return currentWave.monsterType[0];


    }
}
