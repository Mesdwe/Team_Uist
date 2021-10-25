using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : GenericSingletonClass<GameManager>
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Player.Instance.UpdateRP();
            Debug.Log(Player.Instance.rp);
            SceneManager.LoadScene(1);
        }
    }
}
