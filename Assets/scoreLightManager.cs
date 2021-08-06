using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;


public class scoreLightManager : MonoBehaviour
{
    UnityEngine.Experimental.Rendering.Universal.Light2D scoreLight;
    public GameObject playerLO;
    UnityEngine.Experimental.Rendering.Universal.Light2D playerLight;
    void Start()
    {
        scoreLight = playerLO.GetComponent<UnityEngine.Experimental.Rendering.Universal.Light2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(playerLO.transform.position, scoreLight.transform.position) < playerLight.pointLightOuterRadius + scoreLight.pointLightOuterRadius)
        {
            Debug.Log("Hey");
            scoreLight.intensity = 10;
            scoreLight.pointLightOuterRadius = 1f;
        }
    }
}
