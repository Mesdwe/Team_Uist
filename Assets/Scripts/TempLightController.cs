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
    [SerializeField] private GameObject barrierPreview;
    [SerializeField] private Barrier barrierPrefab;
    public void Start()
    {
        CameraZDistance = mainCamera.WorldToScreenPoint(transform.position).z;
        lightSkills = GetComponent<LightSkills>(); //temp
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            //CHANGE THE LIGHT'S ABILITY
            barrierPreview.SetActive(false);       //temp

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
            barrierPreview.SetActive(false);       //temp

            if (currentShip != null)
                UseAbility(currentShip);

        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            //CHANGE THE LIGHT'S ABILITY
            GetComponent<Light>().color = Color.red;
            lightAbilities = LightAbilities.Barrier;
            UseAbility(currentShip);

            //if (currentShip != null)
        }

        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            barrierPreview.transform.Rotate(Vector3.forward * 15f, Space.Self);
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            barrierPreview.transform.Rotate(Vector3.back * 15f, Space.Self);
        }

        //temp
        if (Input.GetKeyDown(KeyCode.Mouse0) && lightAbilities == LightAbilities.Barrier)
        {
            var go = Instantiate(barrierPrefab, barrierPreview.transform.position, barrierPreview.transform.rotation, transform);
            go.transform.SetParent(null);
        }

        transform.position = GetWolrdPositionOnPlane(Input.mousePosition, 355f);
    }
    private void UseAbility(Ship ship)
    {
        lightSkills.ResetAbility(ship);
        if (lightAbilities == LightAbilities.Light)
        {
            lightSkills.SpeedUpShips(ship);

            return;
        }
        if (lightAbilities == LightAbilities.Heal)
        {
            Debug.Log("HEALING");
            lightSkills.HealShips(ship);

            return;
        }
        if (lightAbilities == LightAbilities.Barrier)
        {
            Debug.Log("Drawing Barrier");
            barrierPreview.SetActive(true); //temp
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
