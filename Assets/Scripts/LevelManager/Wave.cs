using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Waves/Wave")]

public class Wave : ScriptableObject
{
    public int levelID;
    public int waveID;
    public int shipCount;
    public GameObject[] ShipTypes;
    public float minShipSpawnTime;
    public float maxShipSpawnTime;
    public GameObject[] monsterType;
    public float minMonsterSpawnTime;
    public float maxMonsterSpawnTime;
}
