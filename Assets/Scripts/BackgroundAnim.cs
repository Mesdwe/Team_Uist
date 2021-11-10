using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundAnim : MonoBehaviour
{
    public Animator ani;

    void Start()
    {
        LevelManager.Instance.OnWaveStart += SetAnimTrigger;
        LevelManager.Instance.OnUpgrade += SetAnimTrigger;


    }
    public void SetAnimTrigger()
    {
        this.ani.SetTrigger("test");
    }
}
