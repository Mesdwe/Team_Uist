using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedMonsterSpawner : MonoBehaviour
{
    [SerializeField] SpawnPoint[] spawnPoints;
    [SerializeField] GameObject monster;
    public Wave currentWave;


    void Start()
    {
        InitRangedMonster(); //Remove it later maybe?
    }
    public void InitRangedMonster()
    {
        StartCoroutine(WaitForShips());
    }

    IEnumerator WaitForShips()
    {
        yield return new WaitForSeconds(5f);
        StartCoroutine(SpawnMonster());
    }

    IEnumerator SpawnMonster()
    {
        float spawnTime = Random.Range(10, 20);
        yield return new WaitForSeconds(spawnTime);

        if (GameManager.Instance.gameState != GameState.Upgrade)
        {
            SpawnPoint spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
            spawnPoint.SpawnTarget(monster);

            StartCoroutine(SpawnMonster());

        }

        //if (GameManager.Instance.gameState != GameState.Upgrade)
    }
}
