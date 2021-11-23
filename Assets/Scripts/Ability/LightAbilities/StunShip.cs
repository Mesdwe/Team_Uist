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
        Debug.Log("STUNNNN");
        //currentLighthouse = obj;
        currentLighthouse.GetComponent<Light>().color = Color.red;
        obj.GetComponentInParent<Monster>().Stunned(3f);    //Fix it
    }
    public override void ResetAbility()
    {
        currentLighthouse.GetComponent<Light>().color = Color.black;
    }
}
