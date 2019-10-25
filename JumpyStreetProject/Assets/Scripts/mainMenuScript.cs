﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class mainMenuScript : MonoBehaviour
{
    //Int values
   public int score = 0;
  


    ///Texts
    public Text titleText;
    public Text scoreText;

    /// Buttons
    public Button startButton;
    public Button quitButton;
    public Button howToButton;
    public Button closeButton;

    ///Game Objects
    public GameObject selectCloseButton;
    public GameObject howCloseButton;
    public GameObject mainPanel;
    public GameObject charSelectPanel;
    public GameObject howToPanel;
    public GameObject quitPanel;
    public GameObject yesButton;
    public GameObject noButton;
    public GameObject charSelectCloseButton;
    public GameObject howToCloseButton;

    ///Audio
    public AudioClip bg;
    
    public void Start()
    {
        charSelectPanel.SetActive(false);
        howToPanel.SetActive(false);
        quitPanel.SetActive(false);
        mainPanel.SetActive(true);
    }

    public void closeAction()
    {
        charSelectPanel.SetActive(false);
        howToPanel.SetActive(false);
        quitPanel.SetActive(false);
        mainPanel.SetActive(true);
    }

    public void StartButton()
    {
        charSelectPanel.SetActive(true);
        mainPanel.SetActive(false);
    }

    public void HowToButton()
    {
        howToPanel.SetActive(true);
        mainPanel.SetActive(false);
    }

    public void QuitButton()
    {
        quitPanel.SetActive(true);
        mainPanel.SetActive(false);
    }

    public void YesButton()
    {
        Application.Quit();
        Debug.Log("Application Closed");
    }

    public void SelectCat()
    {
        Debug.Log("Loading Level with Cat");
        //PlayerControlScript.instance.ChooseModel();
        PlayerControlScript.instance.playerModels[1].SetActive(true);
        SceneManager.LoadScene("BlankScene");
    }
}
