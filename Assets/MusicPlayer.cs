using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    public static int timeMode;

    void Awake()
    {
        SetupSingleton();
        SetTimeMode();
    }

    private void SetTimeMode()
    {
        timeMode = MusicVolume.timeMode;
    }

    // asdasdasda


    private void SetupSingleton()
    {
        if (FindObjectsOfType(GetType()).Length > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public int GetTimeMode()
    {
        return timeMode;
    }

}
