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
    public override void TriggerAbility(GameObject obj)
    {
        Debug.Log("USE IT");

        obj.GetComponent<Ship>().HealShips(upgradeData[upgrade]);
    }
    public override void ResetAbility()
    {
        Debug.Log("Reset " + aName);
    }
}
