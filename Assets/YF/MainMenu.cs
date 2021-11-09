using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    void Start()
    {
        GameManager.Instance.InitMainMenu();
    }
    // Start is called before the first frame update
    public void PlayGame()
    {
        GameManager.Instance.InitGameplay();
        SceneManager.LoadScene(1);
    }

    public void ReturnMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
