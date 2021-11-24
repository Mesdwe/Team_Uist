using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class LighthouseMajor : MonoBehaviour
{
    public LighthouseBuilding lighthouse;
    public event Action<float> OnHealthPctChanged;
    void Start()
    {
        lighthouse.InitBuildingData();
    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            Debug.Log("hit lighthouse");
            lighthouse.HandleHealthChange(this);
            lighthouse.TakingDamage(10);
            OnHealthPctChanged?.Invoke(lighthouse.GetCurrentHealthPct());//CHANGE IT!!! Get monster's attack
            //

            if (lighthouse.GetCurrentHealthPct() <= 0)
            {
                //Bad!
                GameObject losePanel = GameObject.Find("Pause/LosePanel");
                if (losePanel != null)
                {
                    losePanel.SetActive(true);
                }

            }
        }
    }

    public void UpdateHealth()
    {
        OnHealthPctChanged?.Invoke(lighthouse.GetCurrentHealthPct());//CHANGE IT!!! Get monster's attack
    }
}
