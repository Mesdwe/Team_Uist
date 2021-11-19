using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class LevelManager : GenericSingletonClass<LevelManager>
{
    public static int level = 1;
    public static int wave = 1;
    public event Action OnLevelEnd;
    public event Action OnWaveStart;

    public event Action OnUpgrade;
    [SerializeField] private Wave[] waves;
    [SerializeField] private SpawnController spawnController;

    void Awake()
    {
        //OnWaveStart += AddLevelInfo;
        OnWaveStart += InitLevel;
    }

    void Start()
    {
        GameManager.Instance.gameState = GameState.Gameplay;
        StartCoroutine(StartInitLevel());
    }
    public void InitLevel()
    {
        // //StartCoroutine(StartInitLevel());
        // spawnController.StartSpawning(waves[0]); //temp
        int index = (level - 1) * 3 + (wave - 1);
        spawnController.StartSpawning(waves[index]); //temp

    }
    IEnumerator StartInitLevel()
    {
        yield return new WaitForSeconds(1f);
        InitLevel();

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

    private void AddLevelInfo()
    {
        int index = (level - 1) * 3 + (wave - 1);
        spawnController.StartSpawning(waves[index]); //temp

    }

    public void StartUpgrade()
    {
        GameManager.Instance.gameState = GameState.Upgrade;
        OnUpgrade?.Invoke();
    }
    public void NextWave()
    {
        wave += 1;
        if (wave > 3)
        {
            wave = 1;
            //NextLevel();
            StartUpgrade();
            return;
        }
        StartWave();
    }

    public void NextLevel()
    {
        level += 1;
        if (level > 3)
        {
            ResetLevel();
            Debug.Log("END");
            return;
        }
        OnLevelEnd?.Invoke();
        StartWave();
        GameManager.Instance.ResumeGame();
    }

    void OnDisable()
    {
        //OnWaveStart += AddLevelInfo;
        OnWaveStart -= InitLevel;
    }

}
