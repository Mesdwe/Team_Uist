using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    public Animator ani;

    public void Test()
    {
        this.ani.SetTrigger("test");
    }
}
