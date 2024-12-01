using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartMenu : MonoBehaviour
{
    public Animator transition;
    private float transitionTime = 2f;

    //Main Menu Objects
    public GameObject RestartPanel;

    public void retryGame()
    {
        StartCoroutine("RetryTransition");
    }

    public void mainMenu()
    {
        StartCoroutine("MainMenuTransition");
    }

    IEnumerator MainMenuTransition()
    {
        //Start Transition
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(0);
        yield return null;
    }

    IEnumerator RetryTransition()
    {
        //Start Transition
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(1);
        yield return null;
    }
}
