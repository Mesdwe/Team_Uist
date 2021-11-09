using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Waves/Wave")]

public class Wave : ScriptableObject
{
    public int levelID;
    public int waveID;
    public int shipCount;
    public Ship[] ShipTypes;
    public int monsterCount;
    public Monster[] monsterType;
}
