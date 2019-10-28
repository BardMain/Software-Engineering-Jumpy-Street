using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HoldCharacterSelect : MonoBehaviour
{
    public static HoldCharacterSelect CharacterSelect;

    public int characterSelected = -1;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        CharacterSelect = this;
    }

    public void SetCharacter(int selection)
    {
        characterSelected = selection;
        SceneManager.LoadScene(1); //Load the gameplay scene
    }
}
