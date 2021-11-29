using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TutorialInGame : MonoBehaviour
{
    public Sprite[] images;
    public Image target;
    public int id = 0;
    public GameObject menu;
    private bool isOn;
    public void NextPic()
    {
        id++;
        if (id >= images.Length)
            id = 0;
        target.sprite = images[id];
    }
    public void PreviousPic()
    {
        id--;
        if (id < 0)
            id = images.Length - 1;
        target.sprite = images[id];
    }

    public void TutorialButton()
    {
        if (isOn)
        {
            menu.SetActive(false);
            isOn = false;
            return;
        }
        if (!isOn)
        {
            menu.SetActive(true);
            isOn = true;
            return;
        }
    }
}
