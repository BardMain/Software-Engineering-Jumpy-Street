using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMoveScript : MonoBehaviour
{
    public float acceptableDist;
    private float currentDist;

    // Start is called before the first frame update
    void Start()
    {
        if(acceptableDist == 0f)
        {
            acceptableDist = 1.35f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        currentDist = PlayerDistance();
        if(currentDist > acceptableDist)
        {
            Vector3 newPos = transform.position;
            newPos.z = GridMoveScript.instance.transform.position.z - acceptableDist;

            transform.position = newPos;
        }
    }

    private float PlayerDistance()
    {
        float distance = 0;

        distance = GridMoveScript.instance.transform.position.z - transform.position.z;

        return(distance);
    }
}
