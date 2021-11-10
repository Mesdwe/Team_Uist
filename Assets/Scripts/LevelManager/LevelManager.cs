using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class LevelManager : GenericSingletonClass<LevelManager>
{
    public static int level = 1;
    public static int wave = 1;
    public event Action OnWaveEnd;
    public event Action OnLevelEnd;
    public event Action OnWaveStart;

    [SerializeField] private Wave[] waves;
    [SerializeField] private SpawnController spawnController;


    void Start()
    {
        StartCoroutine(StartInitLevel());
    }

    IEnumerator StartInitLevel()
    {
        yield return new WaitForSeconds(1f);
        spawnController.StartSpawning(waves[0]); //temp
        //OnWaveStart();

    }
    public void StartWave()
    {
        OnWaveStart?.Invoke();
    }
    public void ResetLevel()
    {
        level = 1;
        wave = 1;
    }

    public void NextWave()
    {
        wave += 1;
        if (wave > 3)
        {
            wave = 1;
            NextLevel();
        }
    }

    public void NextLevel()
    {
        level += 1;
        if (level > 3)
        {
            ResetLevel();
            Debug.Log("END");
        }
    }
}
