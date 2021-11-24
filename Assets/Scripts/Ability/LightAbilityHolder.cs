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
    //public int barrierCount;
    private MajorLightController major;

    private AudioController audioController;
    public int id;

    void Start()
    {
        major = GetComponent<MajorLightController>();
        ability.InitUpgrade();
        cooldownTime = ability.cooldownTime;
        //Init audio
        audioController = GetComponent<AudioController>();
        audioController.SetAudioClip(0);    //speed up
    }
    private void Update()
    {
        if (!major.lightOn)
            return;
        if (Input.GetKeyDown(key) && state != AbilityState.active)
        {
            audioController.SetAudioClip(id);
            if (state != AbilityState.cooldown)
            {
                ability.Initialize(gameObject);

                state = AbilityState.ready;
                GetComponent<MajorLightController>().SetCurrentAbility(this);

            }

            if (state == AbilityState.cooldown)
            {
                state = AbilityState.readyCooldown;
                ability.Initialize(gameObject);

            }
            //wait for the coolingdown over
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
                audioController.PlayAudioClip();
                state = AbilityState.active;
            }
        }
    }

    public void UseAbility(GameObject ship)
    {
        state = AbilityState.active;

        ability.TriggerAbility(ship);
        audioController.PlayAudioClip();
    }

    public void StartCooldown()
    {
        StartCoroutine(CoolingDown());
    }

    IEnumerator CoolingDown()
    {
        if (state != AbilityState.readyCooldown)
            state = AbilityState.cooldown;
        //if (state == AbilityState.readyCooldown)
        yield return new WaitForSeconds(cooldownTime);
        Debug.Log("Cooldown time: " + cooldownTime);
        if (state == AbilityState.readyCooldown)
        {
            state = AbilityState.ready;
            ability.Initialize(gameObject);
        }
        else
            state = AbilityState.inactive;
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
    cooldown,
    inactive,
    readyCooldown //in the mode but still cooling
}