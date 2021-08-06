using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;

public class GameSession : MonoBehaviour
{
    [SerializeField] public TextAsset file;
    [SerializeField] public GameObject dayMode;
    [SerializeField] public GameObject nightMode;
   

    public Coroutine mulHandler;
    public Slider slider;
    public plat[] platforms;
    int addedScore;
    int score;
    int lightScore;
    public bool stopScore = false;
    public int Mul = 1;

    public bool gameStart = false;

    public void AddLightScore()
    {
        lightScore += 1;
        FindObjectOfType<PlayerMovement>().AddRemainingLight(1);
    }

    public void subLightScore()
    {
        lightScore -= 1;
        FindObjectOfType<PlayerMovement>().SubRemainingLight(1);
    }
    private void Awake()
    {
        Singleton();
       
    }

    private void ChooseBG()
    {
        //int mode = MusicVolume.timeMode; 
        //int mode = FindObjectOfType<MusicPlayer>().GetTimeMode();
        int mode = MusicPlayer.timeMode;
        Debug.Log(mode);
        if (mode == 1)
        {
                dayMode.SetActive(false);
                nightMode.SetActive(true);
        }
        else if (mode == 2)
        {
            nightMode.SetActive(false);
            dayMode.SetActive(true);
        }
        //string tag = file.text;
        //string []lines = tag.Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);
        //Debug.Log(lines[0]);
        //if (float.Parse(lines[0]) == 1f)
        //{
        //    Debug.Log("ASSSSSHOLE");
        //    dayMode.SetActive(false);
        //    nightMode.SetActive(true);
        //}
        //else if (float.Parse(lines[0]) == 2f)
        //{
        //    Debug.Log("BUTTTTTTHOLE");
        //    nightMode.SetActive(false);
        //    dayMode.SetActive(true);
        //}
    }

    private void Singleton()
    {
        int gameSessionNum = FindObjectsOfType<GameSession>().Length;
        if (gameSessionNum > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    public int GetScore()
    {
        return score;
    }

    public void AddScore(int amount)
    {
        score += (int)(Mul * Mul * amount);
    }

    public int GetLightScore()
    {
        return lightScore;
    }

    public void ResetGame()
    {
        Destroy(gameObject);
    }


    IEnumerator ScoreAdd()
    {
        yield return new WaitForFixedUpdate();
        while (!stopScore)
        {
            addedScore = (int)FindObjectOfType<SetSpeed>().GetSpeed();
            AddScore(addedScore);
            slider.value += 0.01f;
            if (slider.value >= 0.99)
            {
                Color color = new Color(0f, 247f, 255f);
                slider.gameObject.transform.Find("Fill Area").Find("Fill").GetComponent<Image>().color = color;
                FindObjectOfType<ParticleToggle>().ActivateParticle();
            }
            else if (slider.value <= 0.1)
            {
                Color color = new Color(76f, 139f, 143f);
                slider.gameObject.transform.Find("Fill Area").Find("Fill").GetComponent<Image>().color = color;

            }
            yield return new WaitForSeconds(0.1f);
        }


    }
    private void Start()
    {
        ChooseBG();
        StartCoroutine("setMul");
        StartCoroutine(ScoreAdd());
        slider.value = 1;
    }

    public void setLightScore(int score)
    {
        lightScore = score;
    }

    public void resetMul()
    {
        StopCoroutine("setMul");
        StartCoroutine("setMul");
        Mul = 1;
    }

    public float GetSliderValue()
    {
        return slider.value;
    }

    public void EmptySliderValue()
    {
        slider.value = 0;

    }

    public int getMul()
    {
        return Mul;
    }

    IEnumerator setMul()
    {
        while (true)
        {
            Mul++;
            
            yield return new WaitForSeconds(10);
        }
    }

    private void Update()
    {
        //if (gameStart && SceneManager.GetActiveScene().buildIndex == 1)
        //{
        //    score = 0;
        //    stopScore = false;
        //    gameStart = false;
        //    StartCoroutine(ScoreAdd());
        //}
    }
}
