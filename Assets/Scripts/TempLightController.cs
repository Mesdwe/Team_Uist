using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempLightController : MonoBehaviour
{
    public float CameraZDistance;
    private LightSkills lightSkills;
    private LightAbilities lightAbilities;
    public Camera mainCamera;
    private Ship currentShip;
    public void Start()
    {
        CameraZDistance = mainCamera.WorldToScreenPoint(transform.position).z;
        lightSkills = GetComponent<LightSkills>();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            //CHANGE THE LIGHT'S ABILITY
            GetComponent<Light>().color = Color.white;
            lightAbilities = LightAbilities.Light;
            if (currentShip != null)
                UseAbility(currentShip);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            //CHANGE THE LIGHT'S ABILITY
            GetComponent<Light>().color = Color.green;
            lightAbilities = LightAbilities.Heal;
            if (currentShip != null)
                UseAbility(currentShip);

        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            //CHANGE THE LIGHT'S ABILITY
            GetComponent<Light>().color = Color.red;
            lightAbilities = LightAbilities.Barrier;
            if (currentShip != null)
                UseAbility(currentShip);
        }
        transform.position = GetWolrdPositionOnPlane(Input.mousePosition, 355f);
    }
    private void UseAbility(Ship ship)
    {
        if (lightAbilities == LightAbilities.Light)
        {
            lightSkills.SpeedUpShips(ship);
            return;
        }
        if (lightAbilities == LightAbilities.Heal)
        {
            Debug.Log("HEALING");
            //lightSkills.SpeedUpShips(ship);
            return;
        }
        if (lightAbilities == LightAbilities.Barrier)
        {
            Debug.Log("Drawing Barrier");
        }
    }
    public Vector3 GetWolrdPositionOnPlane(Vector3 screenPosition, float y)
    {
        Ray ray = Camera.main.ScreenPointToRay(screenPosition);
        Plane xy = new Plane(Vector3.up, new Vector3(0, y, 0));
        float distance;
        xy.Raycast(ray, out distance);
        return ray.GetPoint(distance);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ship"))
        {
            //temp
            currentShip = other.gameObject.GetComponent<Ship>();
            UseAbility(currentShip);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Ship"))
        {
            //temp
            other.gameObject.GetComponent<Ship>().ResetShip();
            currentShip = null;
        }
    }
}
