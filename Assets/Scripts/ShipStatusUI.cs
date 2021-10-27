using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ShipStatusUI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI tmp;

    // Start is called before the first frame update
    void Start()
    {
        tmp.text = "0";//temp
        //Player.Instance.OnRPChanged += UpdateRP;
    }

}
