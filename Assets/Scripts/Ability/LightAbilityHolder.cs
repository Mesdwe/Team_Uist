using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightAbilityHolder : MonoBehaviour
{
    public LightHouseAbility ability;
    float cooldownTime;
    float activeTime;
    int currentUpgrade;
    public AbilityState state;
    public KeyCode key;
    public bool barrier;
    public int barrierCount;
    private MajorLightController major;
    void Start()
    {
        major = GetComponent<MajorLightController>();
        ability.InitUpgrade();
    }
    private void Update()
    {
        if (!major.lightOn)
            return;
        if (Input.GetKeyDown(key))
        {
            ability.Initialize(gameObject);
            state = AbilityState.ready;
            GetComponent<MajorLightController>().SetCurrentAbility(this);
        }
        if (barrier && state == AbilityState.ready)
        {
            if (Input.GetAxis("Mouse ScrollWheel") > 0)
            {
                transform.Rotate(Vector3.forward * 15f, Space.Self);
            }
            if (Input.GetAxis("Mouse ScrollWheel") < 0)
            {
                transform.Rotate(Vector3.back * 15f, Space.Self);
            }
            if (Input.GetKeyDown(KeyCode.Mouse0) && state == AbilityState.ready)
            {
                ability.TriggerAbility(gameObject);
                state = AbilityState.ready;
            }
        }
    }

    public void UseAbility(GameObject ship)
    {
        state = AbilityState.active;

        ability.TriggerAbility(ship);
    }

    public void ResetAbility()
    {
        ability.ResetAbility();
        //state = AbilityState.ready;
    }
}

public enum AbilityState
{
    ready,
    active,
    cooldown
}