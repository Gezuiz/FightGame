using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    
    public void PlayGame ()
    {
        SceneManager.LoadScene("GameMode Selection");
    }

    public void PlayDuel()
    {
        SceneManager.LoadScene("Arena1");
    }

    public void PlayShowdown()
    {
        SceneManager.LoadScene("CharacterSelect1");
    }
    public void BackGameMode()
    {
        SceneManager.LoadScene("GameMode Selection");
    }

    public void PlayMatch()
    {
        SceneManager.LoadScene("Arena1");
    }

    public void QuitGame()
    {
        Debug.Log("QUIT");
        Application.Quit();
    }
}
