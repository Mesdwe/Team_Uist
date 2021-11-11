using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LoadingManager : MonoBehaviour
{
    // Start is called before the first frame update
    private bool isClicked;
    private bool clickable;
    [SerializeField] private Animator animator;
    void Start()
    {
        StartCoroutine(FakeLoading());
    }

    IEnumerator LoadTheMainScene()
    {
        animator.SetTrigger("New Trigger"); //?????
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(2);

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }

    IEnumerator FakeLoading()
    {
        yield return new WaitForSeconds(8f);
        clickable = true;
    }
    void Update()
    {
        if (clickable)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                StartCoroutine(LoadTheMainScene());
            }
        }
    }

}
