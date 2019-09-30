using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class mainMenuScript : MonoBehaviour
{
    public Text titleText;

    public Button startButton;
    public Button quitButton;
    public Button howToButton;
    public Button closeButton;
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
    //public GameObject 

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


    void Update()
    {
        
    }
}
