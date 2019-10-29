using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverScript : MonoBehaviour
{

    public static GameOverScript instance;

    //Transforms
    public Transform player;

    ///Texts
    public Text scoreText;

    ///Game Objects
    public GameObject scorePanel;
    public GameObject replayButton;
    public GameObject menuButton;
    public GameObject endButton;

    public GameObject score;


    public GameObject splashAnim;
    public GameObject splatAnim;

    ///Audio
    public AudioClip bg;
    AudioSource splashAudio;
    AudioSource splatAudio;

    public void Start()
    {

        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
        scorePanel.SetActive(false);
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
        int score = Mathf.FloorToInt(player.position.z) - 5;
        scoreText.text = "Score: " + score.ToString();
    }

    public void InvokeMeDaddy()
    {
        Invoke("OpenPanel", 1f);
    }

    private void OpenPanel()
    {
        scorePanel.SetActive(true);
        score.SetActive(false);
        Debug.Log("Score Board Open");
    } 

    private void SplashAnim()
    {
        Instantiate(splashAnim, PlayerControlScript.instance.transform);
    }
} 
