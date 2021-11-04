using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Abilities/LighthouseAbility")]
public class LightHouseAbility : Ability
{
    public Color lightColor;
    public int upgrade;
    public int maxUpgrade;
    public float[] upgradeData;
    public override void Initialize(GameObject obj)
    {
        Debug.Log("Initialise lighthouse ability: " + aName);
        obj.GetComponent<Light>().color = lightColor;
    }

    public override void TriggerAbility<T>(T type)
    { }
    public override void ResetAbility()
    {
        Debug.Log("Reset " + aName);
    }


}
