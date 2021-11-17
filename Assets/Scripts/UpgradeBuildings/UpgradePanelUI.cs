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

    [SerializeField] private TextMeshProUGUI[] abilityText;

    [SerializeField] private UpgradeButton[] upgradeButtons;

    public void DisplayBuildingData(Building building)
    {
        currentBuilding = building;
        nameTMP.text = building.buildingName;
        healthImage.fillAmount = building.GetCurrentHealthPct();
        var isLighthouse = building.GetType() == typeof(LighthouseBuilding);
        if (isLighthouse)
        {
            LighthouseBuilding lighthouse = (LighthouseBuilding)building;
            //TODO: Show lighthouse abilities current upgrade
            for (int i = 0; i < lighthouse.lighthouseAbilities.Length; i++)
            {
                abilityText[i].text = lighthouse.lighthouseAbilities[i].upgrade.ToString();
            }
        }

        //Check the cost and RP
        foreach (UpgradeButton ub in upgradeButtons)
        {

            bool interactable = (Player.Instance.rp >= building.fixCost) ? true : false;
            ub.UpdateButtonState(interactable);
        }
    }

    public void FixBuilding()
    {
        if (currentBuilding != null)
        {
            currentBuilding.UpdateBuildingData(10);

            DisplayBuildingData(currentBuilding);
        }
    }

    public void UpgradeAbility(int index)
    {
        LighthouseBuilding lighthouse = (LighthouseBuilding)currentBuilding;
        lighthouse.UpgradeAbility(index);

        DisplayBuildingData(lighthouse);
    }

    public void SetUpgradeButtonState(Button button)
    {
        button.interactable = false;
    }
}
