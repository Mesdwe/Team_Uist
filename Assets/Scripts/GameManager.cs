using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : GenericSingletonClass<GameManager>
{
    public GameState gameState;

}
public enum GameState
{
    MainMenu,
    Gameplay,
    Pause,
    Upgrade
}
