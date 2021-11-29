using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Buildings/Lighthouse")]

public class LighthouseBuilding : Building
{
    public LightHouseAbility[] lighthouseAbilities;

    //Initialise the upgrade to zero when the game starts


    public void UpgradeAbility(int index)
    {
        if (lighthouseAbilities[index].upgrade >= lighthouseAbilities[index].maxUpgrade)
            return;

        Player.Instance.UpdateRP(-lighthouseAbilities[index].upgradeCost[lighthouseAbilities[index].upgrade]);
        lighthouseAbilities[index].upgrade++;
    }

    public void TakingDamage(float damage)
    {
        currentHealth -= damage;
        healthPct = (float)currentHealth / maxHealth;
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            healthPct = 0;
        }
    }

    public void HandleHealthChange(LighthouseMajor lighthouse)
    {
        lighthouse.OnHealthPctChanged += TakingDamage;
    }
}
