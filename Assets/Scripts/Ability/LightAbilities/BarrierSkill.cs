using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/Barrier")]
public class BarrierSkill : LightHouseAbility
{
    public GameObject barrierPreview;

    public GameObject barrierTemplate;
    public LightAbilityHolder lightAbilityHolder;
    public Vector3 previewScale;
    public Vector3 prefabScale;
    public override void Initialize(GameObject obj)
    {
        base.Initialize(obj);
        barrierPreview = obj.transform.GetChild(0).gameObject;
        barrierPreview.transform.localScale = previewScale + new Vector3(previewScale.x * upgradeData[upgrade], 0, 0);

        //barrierPreview.SetActive(true);
        LightAbilityHolder[] holders = obj.GetComponents<LightAbilityHolder>();
        for (int i = 0; i < holders.Length; i++)
        {
            if (holders[i].barrier)
                lightAbilityHolder = holders[i];
        }
        if (lightAbilityHolder.state == AbilityState.readyCooldown)
            barrierPreview.SetActive(false);
        else
            barrierPreview.SetActive(true);

    }
    public override void TriggerAbility(GameObject goo)
    {
        // int barrierCount = lightAbilityHolder.barrierCount;
        // if (barrierCount >= upgradeData[upgrade])
        //     return;
        // Debug.Log("USE IT");

        barrierPreview.SetActive(false);
        var go = Instantiate(barrierTemplate, barrierPreview.transform.position, barrierPreview.transform.rotation, barrierPreview.transform.parent);
        go.transform.localScale = prefabScale + new Vector3(prefabScale.x * upgradeData[upgrade], 0, 0);
        go.transform.SetParent(null);
        //lightAbilityHolder.barrierCount++;
        go.GetComponent<Barrier>().SetBarrierSkill(lightAbilityHolder);
    }

    public void ReactiveBarrier()
    {
        barrierPreview.SetActive(true);
    }
    public override void ResetAbility()
    {
        Debug.Log("Reset " + aName);
        if (barrierPreview != null)
            barrierPreview.SetActive(false);
    }
}
