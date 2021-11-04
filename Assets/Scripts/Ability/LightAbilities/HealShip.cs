using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Abilities/Heal")]
public class HealShip : LightHouseAbility
{
    bool isHealing;
    public int healTime;
    public override void Initialize(GameObject obj)
    {
        base.Initialize(obj);

    }
    public override void TriggerAbility<Ship>(Ship ship)
    {
        //ship.SpeedUp(upgradeData[upgrade]);
    }
    public override void ResetAbility()
    {
        Debug.Log("Reset " + aName);
        isHealing =false;
    }
}
