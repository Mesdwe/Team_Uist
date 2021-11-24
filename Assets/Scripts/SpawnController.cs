using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
    [SerializeField] SpawnPoint[] spawnPoints;

    [SerializeField] int shipCount;
    int curShipIndex = 0;
    // [SerializeField] float minSpawnTime;
    // [SerializeField] float maxSpawnTime;

    public Wave currentWave;
    void Start()
    {
        // LevelManager.Instance.InitLevel();
    }
    public void StartSpawning(Wave wave)
    {
        currentWave = wave;
        curShipIndex = 0;

        StartCoroutine(SpawnShip());
        GetComponent<MonsterSpawner>().MonsterInit(wave);
        GetComponent<RangedMonsterSpawner>().InitRangedMonster();

    }
    IEnumerator SpawnShip()
    {
        float spawnTime = Random.Range(currentWave.minShipSpawnTime, currentWave.maxShipSpawnTime);
        yield return new WaitForSeconds(spawnTime);

        SpawnPoint spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        GameObject ship = GetSpawnShip(Random.value);
        spawnPoint.SpawnTarget(ship);
        if (curShipIndex <= currentWave.shipCount)
        {
            StartCoroutine(SpawnShip());
        }
        // else
        // {
        //     GetComponent<MonsterSpawner>().enabled = false;
        // }

    }

    GameObject GetSpawnShip(float value)
    {
        if (currentWave.ShipTypes.Length > 2)
        {
            if (curShipIndex >= currentWave.shipCount)
            {
                curShipIndex++;
                return currentWave.ShipTypes[0];        //ship A
            }
            if (value > 0.3f)
            {
                curShipIndex++;
                return currentWave.ShipTypes[1];       //ship B
            }
            else
            {
                curShipIndex++;
                return currentWave.ShipTypes[2];    //ship C
            }
        }

        else
        {
            if (value > 0.3f)
            {
                curShipIndex++;
                return currentWave.ShipTypes[0];       //ship B
            }
            else
            {
                curShipIndex++;
                return currentWave.ShipTypes[1];    //ship C
            }
        }
    }
}
