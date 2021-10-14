using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempLightController : MonoBehaviour
{
    public float CameraZDistance;
    private LightSkills lightSkills;
    public Camera mainCamera;
    public void Start()
    {
        CameraZDistance = mainCamera.WorldToScreenPoint(transform.position).z;
        lightSkills = GetComponent<LightSkills>();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //CHANGE THE LIGHT'S ABILITY
            GetComponent<Light>().color = Color.red;    //temp
        }
        transform.position = GetWolrdPositionOnPlane(Input.mousePosition, 355f);
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
