using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridMoveScript : MonoBehaviour
{
    //gets relative directions
    private Vector3 up = Vector3.zero,
        right = new Vector3(0, 90, 0),
        down = new Vector3(0, 180, 0),
        left = new Vector3(0, 270, 0),
        currentDir = Vector3.zero;

    //to be filled in to see which way to face and which way to go
    private Vector3 nextPos, destination, diection;

    private float speed = 4f;
    private float rayLength = 1f;

    private bool canMove;
    private bool alive;

    [SerializeField]
    private GameObject player;

    void Start()
    {
        alive = true;
        currentDir = up;
        nextPos = Vector3.forward;
        destination = transform.position;
    }

    void Update()
    {
        if (alive)
        {
            Move();
            Camera.main.gameObject.transform.position = player.transform.position + new Vector3(1f, 6f, 0.83f);
        }
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
                if (currentDir == up || currentDir == down)
                {
                    destination = transform.position + nextPos;
                    destination.x = Mathf.Round(destination.x);
                    //Debug.Log(destination.x);
                    canMove = false;
                }
                else
                {
                    destination = transform.position + nextPos;

                    canMove = false;
                }
            }
            
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

    //Checks to see if you can move there
    private bool Valid()
    {
        Ray myRay = new Ray(transform.position + new Vector3(0, .25f, 0), transform.forward);
        RaycastHit hit;

        Debug.DrawRay(myRay.origin, myRay.direction, Color.red);

        if(Physics.Raycast(myRay, out hit, rayLength))
        {
            if(hit.collider.tag == "Wall")
            {
                return false;
            }
        }
        return true;
    }

    //Collider, use this when hitting an obstacle
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Hey I'm colliding over hea!");

        if(other.gameObject.tag == "Killer")
        {
            Debug.Log("you ded man");
            this.transform.localScale = new Vector3(1.5f, .3f, 1.5f);
            alive = false;
            //Put a death transition here
        }
    }

}
