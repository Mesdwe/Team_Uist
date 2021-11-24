using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Buildings/Building")]
public class Building : ScriptableObject
{
    public string buildingName = "New Building";
    public float defaultHealth;
    public float maxHealth;
    protected float healthPct;
    protected float currentHealth;
    public int[] fixCosts;

    public int defaultHealthLevel;
    private int currentHealthLevel;
    public float[] healthValues;
    public float[] healthEffects;
    [TextArea(10, 100)]
    public string description;
    public virtual void InitBuildingData()
    {
        currentHealth = defaultHealth;
        healthPct = (float)currentHealth / maxHealth;
        currentHealthLevel = defaultHealthLevel;
    }

    public void UpdateBuildingData(int fixValue)        //fixing and upgrading
    {
        if (currentHealth >= maxHealth)
            return;

        Player.Instance.UpdateRP(-fixCosts[currentHealthLevel]);

        currentHealth += fixValue;

        // if ((currentHealthLevel < healthValues.Length - 1))// && currentHealth >= healthValues[currentHealthLevel + 1])      //upgrade
        // {
        //     currentHealthLevel += 1;
        //     currentHealth = healthValues[currentHealthLevel];
        //     Debug.Log("Fixing the building");
        if (currentHealth >= maxHealth)
        {
            currentHealth = maxHealth;
            Debug.Log("Fully fixed");
        }
        //     //Debug.Log("Current Health: " + currentHealth + "  Current Health Level: " + currentHealthLevel);
        // }
        healthPct = (float)currentHealth / maxHealth;
    }
    public float GetCurrentHealthPct() => healthPct;
    public int GetCurrentHealthLevel() => currentHealthLevel;

    public float GetCurrentHealthEffect()
    {
        return healthEffects[currentHealthLevel];
    }
}
