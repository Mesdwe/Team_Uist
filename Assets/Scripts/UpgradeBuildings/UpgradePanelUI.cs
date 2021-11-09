using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class UpgradePanelUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI nameTMP;
    [SerializeField] private Image healthImage;
    private Building currentBuilding;


    public void DisplayBuildingData(Building building)
    {
        currentBuilding = building;
        nameTMP.text = building.buildingName;
        healthImage.fillAmount = building.GetCurrentHealthPct();
    }

    public void FixBuilding()
    {
        if (currentBuilding != null)
        {
            currentBuilding.UpdateBuildingData(10);
            DisplayBuildingData(currentBuilding);
        }
    }
}
