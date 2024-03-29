using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ElectricityBar : MonoBehaviour
{
    [SerializeField] private Image foregroundImage;
    //[SerializeField] private float updateSpeedSeconds = 0.5f;


    public void HandleElectricityChanged(float pct)
    {
        foregroundImage.fillAmount = pct;
    }

    public void UpdateBarColor(bool isDrain)
    {
        if (isDrain)
            foregroundImage.color = Color.red;
        else
            foregroundImage.color = Color.yellow;
    }
}
