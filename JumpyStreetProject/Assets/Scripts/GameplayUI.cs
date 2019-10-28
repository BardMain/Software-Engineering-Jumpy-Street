using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayUI : MonoBehaviour
{
    void Update()
    {
        
    }
    void OnDeath()
    {
        GameObject.Find("scorePanel").SetActive(true);
    }
}
