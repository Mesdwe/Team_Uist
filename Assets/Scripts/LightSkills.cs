using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSkills : MonoBehaviour
{
    public void SetLightSkills(float healTime, float healValue)
    {
        this.healTime = healTime;
        this.healValue = healValue;
    }
    public float healTime;
    public float healValue;
    private bool healing = false;
    private bool startHealing = false;
    private Ship currentShip;
    public void SpeedUpShips(Ship ship)
    {
        ship.SpeedUp();
    }
    public void ResetAbility(Ship ship)
    {
        startHealing = false;
        currentShip = null;
    }
    public void HealShips(Ship ship)
    {
        if (healing)
            return;
        currentShip = ship;
        startHealing = true;
        StartCoroutine(HealingShip(ship));
    }

    private IEnumerator HealingShip(Ship ship)
    {
        healing = true;
        yield return new WaitForSeconds(healTime);
        if (startHealing)
        {
            Debug.Log("HEALED");
            ship.ModifyHealth(+healValue);

        }
        healing = false;
    }

    private void Update()
    {
        if (startHealing)
            HealShips(currentShip);
    }
}
public enum LightAbilities
{
    Light,
    Heal,
    Barrier,
    Stun
}

