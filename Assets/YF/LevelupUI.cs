using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class LevelupUI : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject level1;
    public GameObject level2;
    public GameObject level3;
    public TextMeshProUGUI Text;
    private int i = 0;
    void Start()
    {
        this.GetComponent<Button>().onClick.AddListener(click);
    }

    // Update is called once per frame
    void Update()
    {
        if (i > 0)
        {
            level1.SetActive(true);
            Text.text = "25% -> 35%";
        }
        if (i > 1)
        {
            level2.SetActive(true);
            Text.text = "35% -> 45%";
        }
        if (i > 2)
        {
            level3.SetActive(true);
            Text.text = "";
        }

    }
    void click()
    {
        i++;
    }
}
