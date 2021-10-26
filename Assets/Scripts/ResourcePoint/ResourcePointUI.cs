using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ResourcePointUI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI rpTMP;
    private float currentRP;

    void Start()
    {
        rpTMP.text = Player.Instance.rp.ToString();
        Player.Instance.OnRPChanged += UpdateRP;

    }

    private void UpdateRP()
    {
        //temp
        rpTMP.text = Player.Instance.rp.ToString();
    }

    void OnDisable()
    {
        Player.Instance.OnRPChanged -= UpdateRP;
    }

}
