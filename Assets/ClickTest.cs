using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickTest : MonoBehaviour
{
    public GameObject go;
    public Vector3 GetWolrdPositionOnPlane(Vector3 screenPosition, float z)
    {
        Debug.Log("EWRWERWERE");
        Ray ray = Camera.main.ScreenPointToRay(screenPosition);
        Plane xy = new Plane(Vector3.up, new Vector3(0, 0, z));
        float distance;
        xy.Raycast(ray, out distance);
        return ray.GetPoint(distance);
    }
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            go.transform.position = GetWolrdPositionOnPlane(Input.mousePosition, 300f);
        }
    }

}
