using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/Barrier")]
public class BarrierSkill : LightHouseAbility
{
    public GameObject barrierPreview;
    public int barrierCount = 0;
    public GameObject barrierTemplate;
    public override void Initialize(GameObject obj)
    {
        base.Initialize(obj);
        barrierPreview = obj.transform.GetChild(0).gameObject;
        barrierPreview.SetActive(true);
    }
    public override void TriggerAbility(GameObject goo)
    {
        Debug.Log(barrierCount);
        if (barrierCount >= upgradeData[upgrade])
            return;
        Debug.Log("USE IT");

        var go = Instantiate(barrierTemplate, barrierPreview.transform.position, barrierPreview.transform.rotation, barrierPreview.transform.parent);
        go.transform.SetParent(null);
        barrierCount++;
        go.GetComponent<Barrier>().SetBarrierSkill(this);

    }
    public override void ResetAbility()
    {
        Debug.Log("Reset " + aName);
        if (barrierPreview != null)
            barrierPreview.SetActive(false);
    }
}
