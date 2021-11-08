using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Buildings/Building")]
public class Building : ScriptableObject
{
    public string buildingName = "New Building";
    public int defaultHealth;
    public int MaxHealth;
    public float healthPct;
}
