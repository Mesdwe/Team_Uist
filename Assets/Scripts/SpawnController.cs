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
    // [SerializeField] float minSpawnTime;
    // [SerializeField] float maxSpawnTime;

    public Wave currentWave;
    void Start()
    {

    }
    public void StartSpawning(Wave wave)
    {
        currentWave = wave;
        StartCoroutine(SpawnShip());
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
        else if (GameManager.Instance.gameState == GameState.Upgrade)
            GetComponent<MonsterSpawner>().enabled = false;
    }

    GameObject GetSpawnShip(float value)
    {
        if (curShipIndex >= shipCount)
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
}
