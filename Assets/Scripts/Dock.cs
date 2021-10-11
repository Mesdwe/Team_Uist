using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dock : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Ship"))
        {
            other.gameObject.GetComponent<Ship>().OnArrival();
        }
    }
}
