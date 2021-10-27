using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : GenericSingletonClass<GameManager>
{
    public GameState gameState;

    public void PauseGame()
    {
        Time.timeScale = 0f;
        gameState = GameState.Pause;
    }
    public void ResumeGame()
    {
        Time.timeScale = 1f;
        gameState = GameState.Gameplay;
    }

    public void InitMainMenu()
    {
        Time.timeScale = 1f;
        gameState = GameState.MainMenu;
    }
    public void ReturnMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void InitGameplay()
    {
        gameState = GameState.Gameplay;
    }
}
public enum GameState
{
    MainMenu,
    Gameplay,
    Pause,
    Upgrade
}
