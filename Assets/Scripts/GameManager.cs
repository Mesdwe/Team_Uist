using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : GenericSingletonClass<GameManager>
{
    public GameState gameState;

    public void PauseGame()
    {
        if (gameState == GameState.Gameplay || gameState == GameState.Upgrade)
        {
            Time.timeScale = 0f;
            gameState = GameState.Pause;
            GameObject pauseMenu = GameObject.Find("Pause");
            if (pauseMenu != null)
            {
                pauseMenu.transform.GetChild(0).gameObject.SetActive(true);
            }
        }

    }
    public void ResumeGame()
    {
        Time.timeScale = 1f;
        gameState = GameState.Gameplay;
        GameObject pauseMenu = GameObject.Find("Pause/Image");
        if (pauseMenu != null)
            pauseMenu.SetActive(false);
    }

    public void InitMainMenu()
    {
        Time.timeScale = 1f;
        gameState = GameState.MainMenu;
    }
    public void ReturnMainMenu()
    {
        SceneManager.LoadScene(1);
    }

    public void InitGameplay()
    {
        gameState = GameState.Gameplay;
        Player.Instance.InitPlayer();   //Test

        if (LevelManager.Instance != null)
            Destroy(LevelManager.Instance.gameObject);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gameState == GameState.Gameplay || gameState == GameState.Upgrade)
            {
                Debug.Log("Pause");
                PauseGame();
                return;
            }
            if (gameState == GameState.Pause || gameState == GameState.Upgrade)
            {
                Debug.Log("Resume");
                ResumeGame();
                return;
            }
        }
    }
}
public enum GameState
{
    Intro,
    MainMenu,
    Loading,
    Gameplay,
    Pause,
    Upgrade
}
