using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScoreDisplay : MonoBehaviour
{
    public Text scoreText;
    public Text lightNumber;
    public Text mulitiplierText;

    GameSession gameSession;
    // Start is called before the first frame update
    void Start()
    {
        gameSession = FindObjectOfType<GameSession>();
    }

    // Update is called once per frame
    void Update()
    {

        if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            if (gameSession.GetScore() > PlayerPrefs.GetInt("highscore"))
            {
                PlayerPrefs.SetInt("highscore", gameSession.GetScore());
            }
            int highScore = PlayerPrefs.GetInt("highscore");
            GameObject.FindGameObjectWithTag("Score_Text").GetComponent<Text>().text = "Score: " + gameSession.GetScore().ToString() + "\n" + "High Score:" + highScore;
        }
        else if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            scoreText.text = gameSession.GetScore().ToString();
            lightNumber.text = gameSession.GetLightScore().ToString();
            mulitiplierText.text = gameSession.getMul().ToString()+"X";
        }
    }
}
