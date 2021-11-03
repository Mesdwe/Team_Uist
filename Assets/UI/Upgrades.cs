using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Upgrades : MonoBehaviour
{

    public TextMeshProUGUI text;

    int a = 1;
    
    void Update()
    {
        text.text = a.ToString();
    }

    public void levelup()
    {
        if (a<3)
        a++;
    }
    
}