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
    public int fixCost;

    public int defaultHealthLevel;
    private int currentHealthLevel;
    public int[] healthValues;
    public float[] healthEffects;
    [TextArea(10, 100)]
    public string description;
    public virtual void InitBuildingData()
    {
        currentHealth = defaultHealth;
        healthPct = (float)currentHealth / maxHealth;
        currentHealthLevel = defaultHealthLevel;
    }

    public void UpdateBuildingData(int fixValue)
    {
        if (currentHealth >= maxHealth)
            return;

        Player.Instance.UpdateRP(-fixCost);

        currentHealth += fixValue;
        if (currentHealth > maxHealth)
            currentHealth = maxHealth;
        if ((currentHealthLevel < healthValues.Length - 1) && currentHealth >= healthValues[currentHealthLevel + 1])      //upgrade
        {
            currentHealthLevel += 1;
            Debug.Log("Current Health: " + currentHealth + "  Current Health Level: " + currentHealthLevel);
        }
        healthPct = (float)currentHealth / maxHealth;
    }
    public float GetCurrentHealthPct() => healthPct;
    public float GetCurrentHealthLevel() => currentHealthLevel;

    public float GetCurrentHealthEffect()
    {
        return healthEffects[currentHealthLevel];
    }
}
