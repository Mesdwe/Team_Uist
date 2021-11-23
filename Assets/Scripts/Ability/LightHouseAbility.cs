using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Abilities/LighthouseAbility")]
public class LightHouseAbility : Ability
{
    public Color lightColor;
    public int defaultUpgrade;
    public int upgrade;
    public int maxUpgrade;
    public float[] upgradeData;
    public int[] upgradeCost;

    public override void Initialize(GameObject obj)
    {
        Debug.Log("Initialise lighthouse ability: " + aName);

        obj.GetComponent<MajorLightController>().SetLightColor(lightColor);
        //obj.GetComponent<MajorLightController>().light.color = lightColor;
    }

    public override void TriggerAbility(GameObject obj)
    { }
    public override void ResetAbility()
    {
        Debug.Log("Reset " + aName);
    }

    public virtual void InitUpgrade()   //to default upgrade value
    {
        upgrade = defaultUpgrade;
    }

}
