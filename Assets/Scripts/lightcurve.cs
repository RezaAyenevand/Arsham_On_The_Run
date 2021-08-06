using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.Experimental.Rendering.Universal;
public class lightcurve : MonoBehaviour
{
    [SerializeField] Transform player;

    [SerializeField] float speedBoost;

    [SerializeField]
    private Transform[] routes;

    private int routeToGo;

    private float Tparam;

    private Vector2 lightPosition;

    [SerializeField]
    private float speedModifier;

    [SerializeField]
    private float speed;

    [SerializeField]
    private float timeToNormalize;

    [SerializeField]
    private float freezeTimeScale;

    [SerializeField]
    private CinemachineVirtualCamera camera;
    public bool isPlaying = true;

    UnityEngine.Experimental.Rendering.Universal.Light2D light;

    private bool couroutineAllowed;
    private PlayerMovement playerScript;
    private GameSession gamesession;
    private Vector2 handOffset;


    FreezeVFX []freeze;


    // Start is called before the first frame update
    void Start()
    {
        freeze = FindObjectsOfType<FreezeVFX>();
        handOffset = transform.parent.position - this.transform.position;
        Time.timeScale = 1;
        light = this.GetComponent<UnityEngine.Experimental.Rendering.Universal.Light2D>();
        speedModifier = speed;
        routeToGo = 0;
        Tparam = 0;
        couroutineAllowed = true;
        light.pointLightOuterRadius = 4f;
        playerScript = player.GetComponent<PlayerMovement>();
        gamesession = FindObjectOfType<GameSession>();
    }

    // New Stuff for Sprint3
    public void PlayDeathAnimation()
    {
        GetComponent<Animator>().SetBool("isDead", true);
        
    }





    // Update is called once per frame
    void Update()
    {
        if (isPlaying)
        {
            if (couroutineAllowed && Input.GetButtonDown("Fire1") && playerScript.remainingLights > 0 && gamesession.GetSliderValue() == 1)
            {
                // new for Sprint 3
                foreach (FreezeVFX child in freeze)
                {
                    child.ActivateParticle();
                }
                FindObjectOfType<ParticleToggle>().DeactivateParticle();
                StartCoroutine(GoByRoute(routeToGo));
                gamesession.subLightScore();
                gamesession.EmptySliderValue();
            }
        }
    }

    private IEnumerator GoByRoute(int routeNumber)
    {
        
        Time.timeScale = freezeTimeScale;
        light.pointLightOuterRadius = 8f;
        light.intensity = 2.5f;
        yield return new WaitForFixedUpdate();
        couroutineAllowed = false;

        Vector2 p0 = (Vector2)player.position - handOffset;
        Vector2 p1 = routes[routeNumber].GetChild(1).position;
        Vector2 p2 = routes[routeNumber].GetChild(2).position;
        Vector2 p3 = routes[routeNumber].GetChild(3).position;

        //StartCoroutine()
        float threshhold = 0.01f;

        while (Tparam < 1)
        {
            if (Tparam > threshhold && Tparam < 0.4f)
            {
                speedModifier += speedBoost * Tparam * 2;
                threshhold += 0.02f;
            }
            this.Tparam += Time.deltaTime * speedModifier;
            lightPosition = Mathf.Pow(1 - Tparam, 3) * p0 +
                3 * Mathf.Pow(1 - Tparam, 2) * Tparam * p1 +
                3 * (1 - Tparam) * Mathf.Pow(Tparam, 2) * p2 +
                Mathf.Pow(Tparam, 3) * ((Vector2)player.position - handOffset);

            transform.position = lightPosition;
            yield return new WaitForEndOfFrame();
        }
        transform.position = (Vector2)player.position - handOffset;

        Tparam = 0;

        routeToGo += 1;

        if (routeToGo > routes.Length - 1)
        {
            routeToGo = 0;
        }

        speedModifier = speed;
        //Time.timeScale = 1f;
        StartCoroutine(NormalizeTime());
        // new for Sprint 3
        foreach (FreezeVFX child in freeze)
        {
            child.DeactivateParticle();
        }

        light.pointLightOuterRadius = 4f;
        light.intensity = 2f;

    }

    private IEnumerator boostLight()
    {
        while (Tparam >= 0.5f)
        {
            speedModifier += (speedModifier * Tparam);
            yield return new WaitForSeconds(0.5f);
        }
    }

    public IEnumerator NormalizeTime()
    {
        float spareTime = Time.time;
        float time = 0.1f;
        float accumulative = ((1 - Time.timeScale) * time) / timeToNormalize;
        while (Time.timeScale <= 0.98f)
        {
            Debug.Log("Hey");
            Time.timeScale += accumulative;
            yield return new WaitForSeconds(time);
        }
        Time.timeScale = 1;
        couroutineAllowed = true;
    }
}
