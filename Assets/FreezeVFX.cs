using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeVFX : MonoBehaviour
{
    void Start()
    {
        GetComponent<ParticleSystem>().Stop();
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
