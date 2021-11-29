using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class UpgradePanelUI : MonoBehaviour
{
    //[SerializeField] private TextMeshProUGUI nameTMP;
    [SerializeField] private Image healthImage;
    private Building currentBuilding;

    [SerializeField] private TextMeshProUGUI[] abilityNames;
    [SerializeField] private TextMeshProUGUI[] abilityDescriptions;
    //[SerializeField] private TextMeshProUGUI[] abilityUpgradeData;
    [SerializeField] private GameObject[] abilityUI;

    [SerializeField] private UpgradeButton[] upgradeButtons;

    [SerializeField] private Building defaultBuilding;
    [SerializeField] private GameObject upgradePanel;
    void Start()
    {
        Debug.Log("INIT DEFAULT BUILDING");
        //defaultBuilding.InitBuildingData();
        DisplayBuildingData(defaultBuilding);
    }
    public void DisplayBuildingData(Building building)
    {
        currentBuilding = building;
        //nameTMP.text = building.buildingName;
        healthImage.fillAmount = building.GetCurrentHealthPct();
        var isLighthouse = building.GetType() == typeof(LighthouseBuilding);
        if (isLighthouse)
        {
            LighthouseBuilding lighthouse = (LighthouseBuilding)building;
            //TODO: Show lighthouse abilities current upgrade
            upgradePanel.SetActive(true);
            DisplaySkillInfo(lighthouse.lighthouseAbilities);

        }

        //Check the cost and RP
        for (int i = 0; i < upgradeButtons.Length; i++) //foreach (UpgradeButton ub in upgradeButtons)
        {
            int buildingLevel = building.GetCurrentHealthLevel();
            TextMeshProUGUI buttonText = upgradeButtons[i].gameObject.GetComponentInChildren<TextMeshProUGUI>();
            bool interactableFix = (Player.Instance.rp >= currentBuilding.fixCosts[0]) ? true : false;
            buttonText.text = currentBuilding.fixCosts[buildingLevel].ToString();
            upgradeButtons[0].UpdateButtonState(interactableFix);

            if (isLighthouse && i >= 1)
            {
                LighthouseBuilding lighthouse = (LighthouseBuilding)building;
                if (lighthouse.lighthouseAbilities.Length < 2)  //minor
                {
                    bool unlocked1 = lighthouse.GetCurrentHealthPct() >= 1 ? true : false;
                    abilityUI[0].SetActive(unlocked1);
                    abilityUI[1].SetActive(false);  //get rid of the 2nd and 3rd
                    abilityUI[2].SetActive(false);
                    LightHouseAbility currentAbilityMinor = lighthouse.lighthouseAbilities[0];

                    //Init upgrade icon
                    abilityUI[0].GetComponent<UpgradeIcon>().ResetLevelInfo(currentAbilityMinor.upgrade);


                    if (currentAbilityMinor.upgrade < currentAbilityMinor.upgradeCost.Length)
                    {
                        int currentUpgradeCost = currentAbilityMinor.upgradeCost[currentAbilityMinor.upgrade];
                        buttonText.text = currentUpgradeCost.ToString();
                        bool interactable = (Player.Instance.rp >= currentUpgradeCost) ? true : false;
                        upgradeButtons[i].UpdateButtonState(interactable);
                    }
                    else
                    {
                        buttonText.text = "  ";
                        upgradeButtons[i].UpdateButtonState(false);
                    }
                    return;
                }

                //major
                LightHouseAbility currentAbility = lighthouse.lighthouseAbilities[i - 1];
                abilityUI[i - 1].SetActive(true);
                abilityUI[i - 1].GetComponent<UpgradeIcon>().ResetLevelInfo(currentAbility.upgrade);
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
        }
    }

    private void DisplaySkillInfo(LightHouseAbility[] lighthouseAbilities)
    {
        for (int i = 0; i < lighthouseAbilities.Length; i++)
        {
            abilityNames[i].text = lighthouseAbilities[i].aName;
            abilityDescriptions[i].text = lighthouseAbilities[i].description;

            // int currentUpgrade = lighthouseAbilities[i].upgrade;
            // string symbol = lighthouseAbilities[i].abilityDataSymbol;
            // if (currentUpgrade + 1 <= lighthouseAbilities[i].maxUpgrade)
            // {
            //     string currentData, nextData;
            //     if (symbol == "%")
            //     {
            //         currentData = (lighthouseAbilities[i].upgradeData[currentUpgrade] * 100).ToString() + symbol;
            //         nextData = (lighthouseAbilities[i].upgradeData[currentUpgrade + 1] * 100).ToString() + symbol;
            //     }
            //     else
            //     {
            //         currentData = lighthouseAbilities[i].upgradeData[currentUpgrade].ToString() + symbol;
            //         nextData = lighthouseAbilities[i].upgradeData[currentUpgrade + 1].ToString() + symbol;
            //     }
            //     abilityUpgradeData[i].text = currentData + " -> " + nextData;
            // }
            // else if (symbol == "%")
            // {
            //     abilityUpgradeData[i].text = (lighthouseAbilities[i].upgradeData[currentUpgrade] * 100).ToString() + lighthouseAbilities[i].abilityDataSymbol;
            // }
            // else
            //     abilityUpgradeData[i].text = lighthouseAbilities[i].upgradeData[currentUpgrade].ToString() + symbol;
        }
    }
    public void FixBuilding()
    {
        if (currentBuilding != null)
        {
            if (currentBuilding.buildingName == "Major Lighthouse")
                currentBuilding.UpdateBuildingData(10);
            else
            {
                currentBuilding.UpdateBuildingData(50);

                if (currentBuilding.GetCurrentHealthPct() == 1) //fully fixed
                {
                    //BAD APPROACH!!!!! FIX IT!
                    int buildingIndex = 0;
                    if (currentBuilding.buildingName == "1")
                    {
                        buildingIndex = 0;
                    }
                    if (currentBuilding.buildingName == "2")
                    {
                        buildingIndex = 1;
                    }
                    if (currentBuilding.buildingName == "3")
                    {
                        buildingIndex = 2;
                    }
                    GameObject.Find("Minor_Lighthouses").GetComponent<LighthouseMinorManager>().UnlockLighthouse(buildingIndex);

                }
            }

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
