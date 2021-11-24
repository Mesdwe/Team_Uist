using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeIcon : MonoBehaviour
{
    public GameObject level1;
    public GameObject level2;
    public GameObject level3;
    private int i = 0;


    public void ResetLevelInfo(int level)       //when current building changed
    {
        level1.SetActive(false);
        level2.SetActive(false);
        level3.SetActive(false);
        i = level;
        if (i > 0)
        {
            level1.SetActive(true);
        }
        if (i > 1)
        {
            level2.SetActive(true);
        }
        if (i > 2)
        {
            level3.SetActive(true);
        }
    }
}
