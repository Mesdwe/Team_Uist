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

    [SerializeField] private TextMeshProUGUI[] descriptionText;


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
        for (int i = 0; i < upgradeButtons.Length; i++) //foreach (UpgradeButton ub in upgradeButtons)
        {
            int buildingLevel = building.GetCurrentHealthLevel();
            TextMeshProUGUI buttonText = upgradeButtons[i].gameObject.GetComponentInChildren<TextMeshProUGUI>();
            buttonText.text = currentBuilding.fixCosts[buildingLevel].ToString();

            if (isLighthouse && i >= 1)
            {
                LighthouseBuilding lighthouse = (LighthouseBuilding)building;
                if (lighthouse.lighthouseAbilities.Length < 2)      //lighthouse minors
                    return;
                LightHouseAbility currentAbility = lighthouse.lighthouseAbilities[i - 1];
                if (currentAbility.upgrade < currentAbility.upgradeCost.Length)
                {
                    int currentUpgradeCost = currentAbility.upgradeCost[currentAbility.upgrade];
                    buttonText.text = currentUpgradeCost.ToString();
                    bool interactable = (Player.Instance.rp >= currentUpgradeCost) ? true : false;
                    upgradeButtons[i].UpdateButtonState(interactable);

                }
                else
                {
                    buttonText.text = "  ";
                    upgradeButtons[i].UpdateButtonState(false);
                }

            }
            else//if (!isLighthouse)
            {
                buttonText.text = currentBuilding.fixCosts[buildingLevel].ToString();
                if (buildingLevel < building.fixCosts.Length)
                {
                    bool interactable = (Player.Instance.rp >= building.fixCosts[buildingLevel]) ? true : false;
                    upgradeButtons[i].UpdateButtonState(interactable);
                }
                else
                    upgradeButtons[i].UpdateButtonState(false);//TODO: change the text later
            }

            for (int j = 0; j < descriptionText.Length; j++)
            {
                float currentEffect = currentBuilding.healthEffects[currentBuilding.GetCurrentHealthLevel()];
                float targetEffect = currentBuilding.healthEffects[currentBuilding.GetCurrentHealthLevel() + 1];
                descriptionText[j].text = currentEffect + " -> " + targetEffect;
            }
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
