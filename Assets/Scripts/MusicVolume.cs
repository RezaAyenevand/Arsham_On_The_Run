using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
public class MusicVolume : MonoBehaviour
{
    [SerializeField] public AudioSource music;
    [SerializeField] public Slider slider;
    public static int timeMode;

    //private void Awake()
    //{
    //    Singleton();

    //}
    //private void Singleton()
    //{
    //    int gameSessionNum = FindObjectsOfType<GameSession>().Length;
    //    if (gameSessionNum > 1)
    //    {
    //        Destroy(gameObject);
    //    }
    //    else
    //    {
    //        DontDestroyOnLoad(gameObject);
    //    }
    //}

    // Update is called once per frame
    void Update()
    {
        if (FindObjectOfType<SceneLoader>().isSettingsActive())
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                FindObjectOfType<SceneLoader>().ExitSettings();
            }
            SetVolume();
        }
        
    }
    public void SetVolume()
    {
        //music.volume = slider.value;
        FindObjectOfType<MusicPlayer>().GetComponent<AudioSource>().volume = slider.value;
    }
    public void SetNightMode()
    {
        timeMode = 1;
        Debug.Log("inVolume" + timeMode);
        //string path = "Assets/Resources/mode.txt";
        //StreamWriter writer = new StreamWriter(path, false);
        //writer.WriteLine("1");
        //writer.Flush();
        //writer.Close();
    }
    public void SetDayMode()
    {
        Debug.Log("inVolume" + timeMode);
        timeMode = 2;
        //string path = "Assets/Resources/mode.txt";
        //StreamWriter writer = new StreamWriter(path, false);
        //writer.WriteLine("2");
        //writer.Flush();
        //writer.Close();
    }
    public int GetTimeMode()
    {
        return timeMode;
    }
}
