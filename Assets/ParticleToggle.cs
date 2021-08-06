using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleToggle : MonoBehaviour
{
    //ParticleSystem.EmissionModule em;
    
    void Start()
    {
        GetComponent<ParticleSystem>().Play();
        transform.parent = Camera.main.transform;
       
    }

    public void ActivateParticle()
    {
        GetComponent<ParticleSystem>().Play();
        
    }
    public void DeactivateParticle()
    {
        GetComponent<ParticleSystem>().Stop();
        
        
    }
}
