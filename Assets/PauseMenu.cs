using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject PauseMenuPanel;
    public static bool isPaused;
    float timeOfScale;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isPaused)
            {
                Pause();
            }
            else
            {
                Resume();
            }
        }
    }
    private void Pause()
    {
        PauseMenuPanel.SetActive(true);
        timeOfScale = Time.timeScale;
        Time.timeScale = 0f;
        isPaused = true;
        FindObjectOfType<lightcurve>().isPlaying = false;
    }
    public void Resume()
    {
        PauseMenuPanel.SetActive(false);
        Time.timeScale = timeOfScale;
        isPaused = false;
        FindObjectOfType<lightcurve>().isPlaying = true;
    }
    public void Menu()
    {
        FindObjectOfType<GameSession>().ResetGame();
        isPaused = false;
        FindObjectOfType<lightcurve>().isPlaying = false;
        FindObjectOfType<SceneLoader>().LoadMenu();
    }
    public void Quit()
    {
        Application.Quit();
    }
}
