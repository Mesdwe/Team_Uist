using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LighthouseMinorManager : MonoBehaviour
{
    public MinorLighthouseController[] controllers;

    public void UnlockLighthouse(int index)// or controller?
    {
        controllers[index].gameObject.SetActive(true);
    }
}
