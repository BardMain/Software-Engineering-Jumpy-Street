using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridMoveScript : MonoBehaviour
{
    //Rotation direction for x needs to be minus 90 since the model is not rotated
    private Vector3 up = Vector3.zero,
        right = new Vector3(0, 90, 0),
        down = new Vector3(0, 180, 0),
        left = new Vector3(0, 270, 0),
        currentDir = Vector3.zero;

    private Vector3 nextPos, destination, diection;

    private float speed = 4f;

    private bool canMove;

    private float rayLength = 1f;

    // Start is called before the first frame update
    void Start()
    {
        currentDir = up;
        nextPos = Vector3.forward;
        destination = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, destination, speed * Time.deltaTime);

        if(Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            nextPos = Vector3.forward;
            currentDir = up;
            canMove = true;
        }
        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            nextPos = Vector3.back;
            destination.x = Rounded(destination.x);
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

        if (Vector3.Distance(destination, transform.position) <= .0001f)
        {
            transform.localEulerAngles = currentDir;
            if (canMove && Valid())
            {
                if (currentDir == up || currentDir == down)
                {
                    destination = transform.position + nextPos;
                    destination.x = Rounded(destination.x);
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

    private float Rounded(float target)
    {
        target = Mathf.Round(target);
        return target;
    }

}
