using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingDataHolder : MonoBehaviour
{
    public Building building;

    void OnMouseDown()
    {
        Debug.Log(building.buildingName);
    }
}
