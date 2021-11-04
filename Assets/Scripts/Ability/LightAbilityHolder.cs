using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightAbilityHolder : MonoBehaviour
{
    public Ability ability;
    float cooldownTime;
    float activeTime;
    int currentUpgrade;
    public AbilityState state;
    public KeyCode key;

    private void Update()
    {
        if (Input.GetKeyDown(key))
        {
            ability.Initialize(gameObject);
            state = AbilityState.ready;
        }
    }

    public void UseAbility(Ship ship)
    {
        ability.TriggerAbility<Ship>(ship);
    }

    public void ResetAbility()
    {
        ability.ResetAbility();
    }
}

    public enum AbilityState
    {
        ready,
        active,
        cooldown
    }