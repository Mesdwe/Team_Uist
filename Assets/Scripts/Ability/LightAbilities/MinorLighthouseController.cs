using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinorLighthouseController : MonoBehaviour
{
    public LightHouseAbility ability;
    float cooldownTime;
    float activeTime;

    int currentUpgrade;

    public AbilityState state;

    private void Start()
    {
        cooldownTime = ability.cooldownTime;
        activeTime = ability.activeTime;
        ability.InitUpgrade();      // to default value, move to when it's unlocked
        ability.Initialize(gameObject.transform.GetChild(0).gameObject);

    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Enter");
        if (state != AbilityState.ready)
        {
            Debug.Log("Not ready");
            return;

        }
        if (other.gameObject.CompareTag("Monster"))
        {
            GameObject obj = gameObject.transform.GetChild(0).gameObject;
            ability.TriggerAbility(other.gameObject);

            state = AbilityState.cooldown;
            StartCoroutine(ResetAbility());
        }
    }

    IEnumerator ResetAbility()
    {
        yield return new WaitForSeconds(activeTime);
        ability.ResetAbility();
        StartCoroutine(Cooldown());
    }

    IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(cooldownTime);
        ability.Initialize(gameObject.transform.GetChild(0).gameObject);
        state = AbilityState.ready;

    }
}
