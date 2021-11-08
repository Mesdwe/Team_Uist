using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempLightController : MonoBehaviour
{

    private LightSkills lightSkills;
    private LightAbilities lightAbilities;
    private Ship currentShip;
    [SerializeField] private GameObject barrierPreview;
    [SerializeField] private Barrier barrierPrefab;

    private bool lightOn = true;
    [SerializeField] ElectricityBar electricityBar;
    [SerializeField] private float maxElectricity = 100f;
    public float CurrentElectricity { get; private set; }
    [SerializeField] private float electricityDuration = 60f;
    private bool isDrain;

    public void Start()
    {
        lightSkills = GetComponent<LightSkills>(); //temp
        CurrentElectricity = maxElectricity;
    }

    private void UseAbility(Ship ship)
    {
        lightSkills.ResetAbility(ship);
        if (!lightOn)
            return;
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

    void Update()
    {
        if (GameManager.Instance.gameState != GameState.Gameplay)
            return;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            lightOn = !lightOn;
            GetComponent<Light>().enabled = lightOn;
            Reset();
        }

        if (lightOn)
        {
            CurrentElectricity -= (maxElectricity / electricityDuration) * Time.deltaTime;
            electricityBar.HandleElectricityChanged(CurrentElectricity / maxElectricity);

            if (CurrentElectricity < 0)
            {
                isDrain = true;
                CurrentElectricity = 0;
                lightOn = false;
                GetComponent<Light>().enabled = lightOn;
                Reset();
                return;
            }
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

        }
        else
        {
            if (!isDrain)
                CurrentElectricity += (maxElectricity / electricityDuration) * Time.deltaTime;
            else
                CurrentElectricity += (maxElectricity / electricityDuration) / 2f * Time.deltaTime;
            if (CurrentElectricity >= 100f)
            {
                CurrentElectricity = 100f;
                isDrain = false;
                electricityBar.UpdateBarColor(isDrain);
            }
            electricityBar.HandleElectricityChanged(CurrentElectricity / maxElectricity);
        }
        transform.position = GetWolrdPositionOnPlane(Input.mousePosition, 355f);

    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ship"))
        {
            //temp
            if (currentShip == null)
                currentShip = other.gameObject.GetComponent<Ship>();
            else if (other.gameObject.GetComponent<Ship>() != currentShip)
            {
                lightSkills.ResetAbility(currentShip);
                currentShip.ResetShip();
                currentShip = other.gameObject.GetComponent<Ship>();
            }
            UseAbility(currentShip);
        }
    }

    private void Reset()
    {
        electricityBar.UpdateBarColor(isDrain);
        barrierPreview.SetActive(false);
        if (currentShip != null)
        {
            lightSkills.ResetAbility(currentShip);
            currentShip.GetComponent<Ship>().ResetShip();
        }

    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Ship"))
        {
            //temp
            lightSkills.ResetAbility(currentShip);
            other.gameObject.GetComponent<Ship>().ResetShip();
            currentShip = null;
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
}
