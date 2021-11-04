using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/IncreaseSpeed")]
public class IncreaseShipSpeed : LightHouseAbility
{
    public override void Initialize(GameObject obj)
    {
        base.Initialize(obj);

    }
    public override void TriggerAbility(GameObject obj)
    {
        obj.GetComponent<Ship>().SpeedUp(upgradeData[upgrade]);
    }
    public override void ResetAbility()
    {
        Debug.Log("Reset " + aName);
    }
}
