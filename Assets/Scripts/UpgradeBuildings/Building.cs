using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Buildings/Building")]
public class Building : ScriptableObject
{
    public string buildingName = "New Building";
    public int defaultHealth;
    public int maxHealth;
    private float healthPct;
    private int currentHealth;
    public void InitBuildingData()
    {
        currentHealth = defaultHealth;
        healthPct = (float)currentHealth / maxHealth;
        Debug.Log(currentHealth / maxHealth);
    }

    public void UpdateBuildingData(int fixValue)
    {
        currentHealth += fixValue;
        if (currentHealth > maxHealth)
            currentHealth = maxHealth;
        healthPct = (float)currentHealth / maxHealth;
    }
    public float GetCurrentHealthPct() => healthPct;

}
