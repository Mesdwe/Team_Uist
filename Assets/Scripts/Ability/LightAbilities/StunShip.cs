using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/Stun")]
public class StunShip : LightHouseAbility
{
    GameObject currentLighthouse;
    public override void Initialize(GameObject obj)
    {
        //base.Initialize(obj);
        obj.GetComponent<Light>().color = Color.white;
        currentLighthouse = obj;
    }

    public override void TriggerAbility(GameObject obj)
    {
        //currentLighthouse = obj;
        currentLighthouse.GetComponent<Light>().color = Color.red;
        Debug.Log(obj.name);
        obj.GetComponentInParent<Monster>().Stunned(upgradeData[upgrade]);
    }
    public override void ResetAbility()
    {
        currentLighthouse.GetComponent<Light>().color = Color.black;
    }
}
