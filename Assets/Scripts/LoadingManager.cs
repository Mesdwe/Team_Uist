using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class LoadingManager : MonoBehaviour
{
    // Start is called before the first frame update
    public Text m_Text;
    private bool isClicked = false;
    private bool clickable;
    [SerializeField] private Animator animator;
    void Start()
    {
        Time.timeScale = 1f;
        GameManager.Instance.gameState = GameState.Loading;
        //StartCoroutine(FakeLoading());
        StartCoroutine(LoadScene());
    }
    IEnumerator LoadScene()
    {
        yield return new WaitForSeconds(2f);

        //Begin to load the Scene you specify
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(3);
        //Don't let the Scene activate until you allow it to
        asyncOperation.allowSceneActivation = false;
        Debug.Log("Pro :" + asyncOperation.progress);
        //When the load is still in progress, output the Text and progress bar
        while (!asyncOperation.isDone)
        {
            //Output the current progress
            m_Text.text = "Loading progress: " + (asyncOperation.progress * 100 * 0.03) + "%";

            // Check if the load has finished
            if (asyncOperation.progress >= 0.9f)
            {
                //Change the Text to show the Scene is ready
                m_Text.text = "Press the space bar to continue";
                //Wait to you press the space key to activate the Scene
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    GameManager.Instance.gameState = GameState.Gameplay;

                    //Activate the Scene
                    asyncOperation.allowSceneActivation = true;
                }

            }

            yield return null;
        }
    }
    // IEnumerator LoadTheMainScene()
    // {

    //     AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(2);

    //     yield return new WaitForSeconds(2f);

    //     // Wait until the asynchronous scene fully loads
    //     while (!asyncLoad.isDone)
    //     {
    //         yield return null;
    //     }
    //     while (!isClicked)
    //     {
    //         yield return null;
    //     }
    //     //?????
    //     yield return new WaitForSeconds(1f);
    //     GameManager.Instance.gameState = GameState.Gameplay;
    // }

    // IEnumerator FakeLoading()
    // {
    //     yield return new WaitForSeconds(8f);
    //     clickable = true;
    // }
    // void Update()
    // {
    //     if (clickable)
    //     {
    //         if (Input.GetKeyDown(KeyCode.Mouse0))
    //         {
    //             //StartCoroutine(LoadTheMainScene());
    //             isClicked = true;
    //             animator.SetTrigger("New Trigger");
    //         }
    //     }
    // }



}
