using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingDataHolder : MonoBehaviour
{
    public Building building;
    [SerializeField]
    private UpgradePanelUI upgradeUI;
    [SerializeField] private bool isInit;
    void Start()
    {
        if (isInit)
            building.InitBuildingData();
    }
    void OnMouseDown()
    {
        Debug.Log(building.buildingName);
    }

    public void OnBuildingClicked()
    {
        upgradeUI.DisplayBuildingData(building);
        //TODO: Update the button state
        //Compare the cost with rp
    }


}
