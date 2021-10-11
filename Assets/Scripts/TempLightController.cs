using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempLightController : MonoBehaviour
{
    public float CameraZDistance;
    private LightSkills lightSkills;
    public void Start()
    {
        CameraZDistance = Camera.main.WorldToScreenPoint(transform.position).z;
        lightSkills = GetComponent<LightSkills>();
    }
    void Update()
    {
        Vector3 screenPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, CameraZDistance);
        Vector3 newWolrdPosition = Camera.main.ScreenToWorldPoint(screenPosition);
        transform.position = new Vector3(newWolrdPosition.x, 4f, newWolrdPosition.z);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ship"))
        {
            //temp
            lightSkills.SpeedUpShips(other.gameObject.GetComponent<Ship>());
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Ship"))
        {
            //temp
            other.gameObject.GetComponent<Ship>().ResetShip();
        }
    }
}
