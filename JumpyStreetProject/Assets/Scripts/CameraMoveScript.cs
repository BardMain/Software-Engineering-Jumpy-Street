﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMoveScript : MonoBehaviour
{
    public float acceptableDist;
    public float deathDist;
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
            newPos.z = PlayerControlScript.instance.transform.position.z - acceptableDist;

            transform.position = newPos;
        }
        if(currentDist < deathDist)
        {
            PlayerControlScript.instance.Death("outtaBounds");
        }
    }

    private float PlayerDistance()
    {
        float distance = 0;

        distance = PlayerControlScript.instance.transform.position.z - transform.position.z;

        return(distance);
    }
}