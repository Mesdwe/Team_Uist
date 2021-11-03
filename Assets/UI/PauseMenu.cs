using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    // Start is called before the first frame update

    // Update is called once per frame
    public void Resume()
    {
        GameManager.Instance.ResumeGame();

    }

    public void Pause()
    {
        GameManager.Instance.PauseGame();
    }
    public void ReturnMainMenu()
    {
        GameManager.Instance.ReturnMainMenu();
    }


}
