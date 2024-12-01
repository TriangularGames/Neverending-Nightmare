﻿using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public Animator transition;
    private float transitionTime = 2f;

    //Main Menu Objects
    public GameObject MainMenuPanel;

    //Options Menu Objects
    public GameObject OptionsPanel;


    public void playGame()
    {
        StartCoroutine("GameStartTransition");
    }

    public void optionsMenu()
    {
        StartCoroutine("OptionsTransition");
    }

    public void mainMenu()
    {
        StartCoroutine("MainMenuTransition");
    }

    public void Exit()
    {
        Application.Quit();
    }

    //Option Transition
    IEnumerator OptionsTransition()
    {
        //Start Transition
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        //Hide Main Menu
        MainMenuPanel.SetActive(false);

        //Show Options Menu
        OptionsPanel.SetActive(true);

        //Finish transition
        transition.SetTrigger("End");
        yield return new WaitForSeconds(1f);
    }

    IEnumerator MainMenuTransition()
    {
        //Start Transition
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        //Hide Options Menu
        OptionsPanel.SetActive(false);

        //Show Main Menu
        MainMenuPanel.SetActive(true);

        //Finish transition
        transition.SetTrigger("End");
        yield return new WaitForSeconds(1f);
    }

    IEnumerator GameStartTransition()
    {
        //Start Transition
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        yield return null;
    }

}
