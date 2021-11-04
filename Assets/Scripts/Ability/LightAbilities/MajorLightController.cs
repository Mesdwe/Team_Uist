using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MajorLightController : MonoBehaviour
{
    private Ship currentShip;
    private bool lightOn = true;
    [SerializeField] ElectricityBar electricityBar;
    [SerializeField] private float maxElectricity = 100f;
    public float CurrentElectricity { get; private set; }
    [SerializeField] private float electricityDuration = 60f;
    private bool isDrain;
    // Start is called before the first frame update
    void Start()
    {
        CurrentElectricity = maxElectricity;
    }

    // Update is called once per frame
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
                //reset current active skill
                //currentShip.ResetShip(); in skill holder
                currentShip = other.gameObject.GetComponent<Ship>();
            }
            //UseAbility(currentShip);
            //TODO: iterate skill holder->ready:active->active:reset
            UseAbility(currentShip);
        }
    }

    void UseAbility(Ship ship)
    {
        LightAbilityHolder[] lightAbilityHolders = GetComponents<LightAbilityHolder>();
        foreach(var holder in lightAbilityHolders)
        {
            if(holder.state==AbilityState.ready)
            {
                holder.UseAbility(ship);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Ship"))
        {
            //temp
            //lightSkills.ResetAbility(currentShip);
            other.gameObject.GetComponent<Ship>().ResetShip();
            currentShip = null;
        }
    }
    private void Reset()
    {
        electricityBar.UpdateBarColor(isDrain);
        //barrierPreview.SetActive(false);
        if (currentShip != null)
        {
            //lightSkills.ResetAbility(currentShip);
            currentShip.GetComponent<Ship>().ResetShip();
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
