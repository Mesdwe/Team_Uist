using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMovement : MonoBehaviour
{
    private bool _moving;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            _moving = true;
        }
        if (Input.GetKeyUp(KeyCode.W))
        {
            _moving = false;
        }
        if (_moving)
            transform.position += transform.right * 10 * Time.deltaTime;

    }
}
