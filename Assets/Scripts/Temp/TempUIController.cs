using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempUIController : MonoBehaviour
{
    public void SpeedUp()
    {
        if (Time.timeScale <= 50)
            Time.timeScale *= 2f;
    }

    public void ReplayGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }
}