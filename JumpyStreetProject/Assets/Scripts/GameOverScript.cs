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
    public Text playerEndScore;
    public Text highScoreText;

    ///Game Objects
    public GameObject scorePanel;
    public GameObject replayButton;
    public GameObject menuButton;
    public GameObject endButton;

    public GameObject scoreObject;


    public GameObject splashAnim;
    public GameObject splatAnim;

    private int scoreValue;
    private static int highScoreValue;

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
        scoreValue = 0;

        highScoreValue = PlayerPrefs.GetInt("highscore", highScoreValue);
        highScoreText.text = "High Score: " + highScoreValue.ToString();
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
        scoreValue = Mathf.FloorToInt(player.position.z) - 5;
        scoreText.text = "Score: " + scoreValue.ToString();

        if (scoreValue > highScoreValue)
        {
            highScoreValue = scoreValue;
            highScoreText.text = "Highscore " + scoreValue;

            PlayerPrefs.SetInt("highscore", highScoreValue);
        }
        
    }

    public void InvokeMeDaddy()
    {
        Invoke("OpenPanel", 1f);
    }

    private void OpenPanel()
    {
        scorePanel.SetActive(true);
        scoreObject.SetActive(false);
        playerEndScore.text = "Player Score: " + scoreValue.ToString();

        Debug.Log("Score Board Open");
    } 

    private void SplashAnim()
    {
        Instantiate(splashAnim, PlayerControlScript.instance.transform);
    }
} 
