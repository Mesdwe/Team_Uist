using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ShipStatusUI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI tmp;
    private int currentCount;
    public bool isSaved;
    void Start()
    {
        tmp.text = "0";//temp
        Player.Instance.OnShipChanged += UpdateShipCount;

    }
    private void UpdateRP()
    {
        tmp.text = Player.Instance.rp.ToString();
    }

    private void UpdateShipCount(bool saved, int count)     //ship.OnArrial+=UpdateRP
    {
        //temp

        if (isSaved == saved)
        {
            Debug.Log("DIE SHIP DIE" + count);
            tmp.text = count.ToString();

        }
    }

    void OnDisable()
    {
        if (Player.Instance != null)
            Player.Instance.OnShipChanged -= UpdateShipCount;
    }
}
