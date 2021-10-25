using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ResourcePointUI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI rpTMP;

    void Start()
    {
        rpTMP.text = Player.Instance.rp.ToString();
    }

}
