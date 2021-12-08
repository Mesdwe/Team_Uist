using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class IntroManager : MonoBehaviour
{
    // public VideoPlayer vid;


    //void Start() { vid.loopPointReached += CheckOver; }
    private bool isDone;
    public GameObject secondImage;
    void CheckOver(UnityEngine.Video.VideoPlayer vp)
    {
        SceneManager.LoadScene(1);

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!isDone)
            {
                secondImage.SetActive(true);
                isDone = true;
                return;
            }
            if (isDone)
            {
                SceneManager.LoadScene(1);
            }

        }
    }
}
