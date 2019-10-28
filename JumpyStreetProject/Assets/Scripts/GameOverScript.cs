using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverScript : MonoBehaviour
{
   //Transforms
    public Transform player;

    ///Texts
    public Text scoreText;

    ///Game Objects
    public GameObject scorePanel;
    public GameObject replayButton;
    public GameObject menuButton;
    public GameObject endButton;


    ///Audio
    public AudioClip bg;

    public void Start()
    {
        //
    }

    public void menuLoad()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void ReplayButton()
    {
        SceneManager.LoadScene("GameplayScene");
    }


    public void EndGameButton()
    {
        Application.Quit();
        Debug.Log("Application Closed");
    }

    public void Update()
    {
        scoreText.text = player.position.z.ToString("0");
    }

}
