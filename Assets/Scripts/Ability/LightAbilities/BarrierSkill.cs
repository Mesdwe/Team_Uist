using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/Barrier")]
public class BarrierSkill : LightHouseAbility
{
    public GameObject barrierPreview;

    public GameObject barrierTemplate;
    public LightAbilityHolder lightAbilityHolder;
    public override void Initialize(GameObject obj)
    {
        base.Initialize(obj);
        barrierPreview = obj.transform.GetChild(0).gameObject;
        barrierPreview.SetActive(true);
        LightAbilityHolder[] holders = obj.GetComponents<LightAbilityHolder>();
        for (int i = 0; i < holders.Length; i++)
        {
            if (holders[i].barrier)
                lightAbilityHolder = holders[i];
        }
    }
    public override void TriggerAbility(GameObject goo)
    {
        int barrierCount = lightAbilityHolder.barrierCount;
        if (barrierCount >= upgradeData[upgrade])
            return;
        Debug.Log("USE IT");

        var go = Instantiate(barrierTemplate, barrierPreview.transform.position, barrierPreview.transform.rotation, barrierPreview.transform.parent);
        go.transform.SetParent(null);
        lightAbilityHolder.barrierCount++;
        go.GetComponent<Barrier>().SetBarrierSkill(lightAbilityHolder);

    }
    public override void ResetAbility()
    {
        Debug.Log("Reset " + aName);
        if (barrierPreview != null)
            barrierPreview.SetActive(false);
    }
}
