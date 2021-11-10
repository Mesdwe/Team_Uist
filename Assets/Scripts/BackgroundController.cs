using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundController : MonoBehaviour
{
    public void OnNextWaveClicked()
    {
        LevelManager.Instance.StartWave();
    }
}
