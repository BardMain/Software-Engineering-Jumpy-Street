﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControlScript : MonoBehaviour
{
    public static PlayerControlScript instance;
    public GameObject[] playerModels = new GameObject[4];


    //gets relative directions
    private Vector3 up = Vector3.zero,
        right = new Vector3(0, 90, 0),
        down = new Vector3(0, 180, 0),
        left = new Vector3(0, 270, 0),
        currentDir = Vector3.zero;

    //to be filled in to see which way to face and which way to go
    private Vector3 nextPos, destination, diection, startPos;

    private float speed = 4f;
    private float rayLength = 1f;
    private float inheritedSpeed = 0f;
    private float gameEdge = 5f;

    //HANDLING 'ANIMATIONS'
    //public Transform player;


    ///Handling Score
    [HideInInspector]
    /////////////////////////////////////////////////
    ///          ALLISON, CHECK HERE
    ///          
    /// the score updates whenever you move so to
    /// access the current score just do:
    /// 
    ///     yourVariableHere = PlayerControlScript.instance.score;
    /// 
    ////////////////////////////////////////////////
    public float score;

    private bool canMove;
    private bool alive;

    [HideInInspector]
    public bool onLand;
    [HideInInspector]
    public bool onLog;

    [HideInInspector]
    public int modelChoice;

    void Start()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }

        startPos = transform.position;

        modelChoice = 0;
        alive = true;
        onLand = true;
        onLog = false;

        currentDir = up;
        nextPos = Vector3.forward;
        destination = transform.position;

        
        ChooseModel(HoldCharacterSelect.CharacterSelect.characterSelected);
    }

    void Update()
    {
        if (!onLand && !onLog)
        {
            float roundedZ = Mathf.Round(transform.position.z);

            if (roundedZ == transform.position.z)
            {
                Death("splash");
            }
        }
        if (alive)
        {
            Move();
        }
        if (onLog)
        {
            LogMove();
        }
        CheckEdge();
    }

    private void CheckEdge()
    {
        float xPos = transform.position.x;
        if((xPos > gameEdge) || (xPos < -gameEdge))
        {
            Death("outtaBounds");
        }
    }

    private void HidePlayerModels()
    {
        for (int x = 0; x < playerModels.Length; x++)
        {
            playerModels[x].SetActive(false);
        }
    }

    /////////////////////////////////////////////////
    ///          ALLISON, CHECK HERE
    ///          
    /// all you need to do here to selecte the player model
    /// is to have whatever function you use execute the choose
    /// model function below. That can be done with:
    /// 
    ///    PlayerControlScript.instance.ChooseModel();
    /// 
    ////////////////////////////////////////////////
    public void ChooseModel(int selection)
    {
        HidePlayerModels();
        playerModels[selection].SetActive(true);
    }

    //Handles where you gonna move
    private void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, destination, speed * Time.deltaTime);

        GetInputs();

        if (Vector3.Distance(destination, transform.position) <= .0001f)
        {
            transform.localEulerAngles = currentDir;
            //If are not stuck and can move to a spot will move you
            if (canMove && Valid())
            {
                //Only corrects your horizontal placement if you are moving up our down
                //This allows us to avoid needing nodes to place player on while keeping
                //the player from crashing into a wall they are halfway through
                float roundedZ = Mathf.Round(transform.position.z);

                if (roundedZ == transform.position.z)
                {
                    destination = transform.position + nextPos;
                    destination.x = Mathf.Round(destination.x);
                    Debug.Log(destination.x);
                    canMove = false;
                    score = getScore();
                }
                else
                {
                    destination = transform.position + nextPos;

                    canMove = false;
                    score = getScore();
                }
            }

        }
    }

    private float getScore()
    {
        float score = 0;
        score = transform.position.z -startPos.z;
        return (score);
    }

    private void LogMove()
    {
        float roundedZ = Mathf.Round(transform.position.z);
        ///////////////////////
        /// I want to adjust how lateral movement works on logs here
        ////////////////////////
        if (roundedZ == transform.position.z)
        {
            Vector3 toMove = Vector3.zero;
            toMove.x = inheritedSpeed * Time.deltaTime;
            destination += toMove;
            //Debug.Log("I have moved " + toMove.x + " Since last time");

        }
        else
        {
            //Debug.Log("I am moving up and down, don't bug me");
        }
    }

    //Gets your inputs
    private void GetInputs()
    {
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            nextPos = Vector3.forward;
            currentDir = up;
            canMove = true;
        }
        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            nextPos = Vector3.back;
            currentDir = down;
            canMove = true;
        }
        if (!onLog)
        {
            if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
            {
                nextPos = Vector3.right;
                currentDir = right;
                canMove = true;
            }
            if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
            {
                nextPos = Vector3.left;
                currentDir = left;
                canMove = true;
            }
        }
    }

    //Checks to see if you can move there
    private bool Valid()
    {
        Ray myRay = new Ray(transform.position + new Vector3(0, .25f, 0), transform.forward);
        RaycastHit hit;

        Debug.DrawRay(myRay.origin, myRay.direction, Color.red);

        if (Physics.Raycast(myRay, out hit, rayLength))
        {
            if (hit.collider.tag == "Wall")
            {
                return false;
            }
        }
        return true;
    }

    //Collider, use this when hitting an obstacle
    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Hey I'm colliding over hea!");

        if (other.gameObject.tag == "Killer")
        {
            Death("splat");
        }
        if (other.gameObject.tag == "Land")
        {
            onLand = true;
        }
        if (other.gameObject.tag == "Floater")
        {
            if (!onLog)
            {
                inheritedSpeed = ObstacleSpawnerScript.instance.HandOverThatFloat(other);
                Debug.Log("The inherited speed should be " + inheritedSpeed);
            }
            onLog = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Land")
        {
            onLand = false;
        }
        if (other.gameObject.tag == "Floater")
        {
            onLog = false;
        }
    }

    //Everything that happens in death should occur here
    public void Death(string death)
    {
        Debug.Log("you ded man");
        switch (death)
        {
            case "splat":
                this.transform.localScale = new Vector3(1.5f, .3f, 1.5f);
                Vector3 downSome = Vector3.zero;
                downSome.y = .51f;
                transform.position -= downSome;


                break;

            case "splash":
                nextPos = Vector3.down;
                destination = transform.position + nextPos;
                canMove = false;
                transform.position = Vector3.MoveTowards(transform.position, destination, speed * Time.deltaTime);


                break;
            case "outtaBounds":
                
                break;
            default:
                Debug.Log("hey, how'd you end up dead anyway?");
                break;
        }
        alive = false;

        GameOverScript.instance.InvokeMeDaddy();

        /////////////////////////////////////////////////
        ///          ALLISON, CHECK HERE
        ///          
        /// whatever you want to happen on player death put here.
        /// if you make your menu controller script into a Singleton
        /// you can just have this function call that script's function,
        /// or something like that
        /// 
        ////////////////////////////////////////////////
    }
}
