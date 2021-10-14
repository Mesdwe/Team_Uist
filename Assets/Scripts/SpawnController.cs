using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
    [SerializeField] SpawnPoint[] spawnPoints;
    [SerializeField] GameObject[] ships;
    //temp
    [SerializeField] int shipCount;
    int curShipIndex = 0;
    [SerializeField] float minSpawnTime;
    [SerializeField] float maxSpawnTime;

    void Start()
    {
        StartCoroutine(SpawnShip());
    }
    IEnumerator SpawnShip()
    {
        float spawnTime = Random.Range(minSpawnTime, maxSpawnTime);
        yield return new WaitForSeconds(spawnTime);

        SpawnPoint spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        GameObject ship = GetSpawnShip(Random.value);
        spawnPoint.SpawnTarget(ship);
        if (curShipIndex <= shipCount)
        {
            StartCoroutine(SpawnShip());
        }
        else
        {
            GetComponent<MonsterSpawner>().enabled = false;
        }
    }

    GameObject GetSpawnShip(float value)
    {
        if (curShipIndex >= shipCount)
        {
            curShipIndex++;
            return ships[2];        //ship A
        }
        if (value > 0.3f)
        {
            curShipIndex++;
            return ships[0];    //ship B
        }
        else
        {
            curShipIndex++;
            return ships[1];    //ship C
        }
    }
}
