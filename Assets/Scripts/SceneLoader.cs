using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class SceneLoader : MonoBehaviour
{
    public GameObject SettingsMenu;

    public void LoadPlayScene()
    {
        FindObjectOfType<GameSession>().ResetGame();
        SceneManager.LoadScene(1);
    }

    public void LoadStart()
    {
        SceneManager.LoadScene(1);
    }
    public void LoadMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void LoadSettings()
    {
        SettingsMenu.SetActive(true);
    }
    public void ExitSettings()
    {
        SettingsMenu.SetActive(false);
    }
    public bool isSettingsActive()
    {
        if (SettingsMenu.activeInHierarchy == true)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
