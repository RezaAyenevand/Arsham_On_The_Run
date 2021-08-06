using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightScript : MonoBehaviour
{

    public Transform target;
    public Transform player;

    private bool isIdle;
    private bool isOnAir;
    private bool isThrowing;
    private bool isReturning;
    private bool isSlowDone;
    private Rigidbody2D lightRigid;
    // Start is called before the first frame update


    private float speed;
    private void Awake()
    {
        isIdle = true;
        isOnAir = false;
        isThrowing = false;
        isReturning = false;
        isSlowDone = false;
    }
    void Start()
    {
        lightRigid = GetComponent<Rigidbody2D>();
        lightRigid.bodyType = RigidbodyType2D.Kinematic;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (isIdle)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                isOnAir = true;
                isThrowing = true;
                isIdle = false;
                Fire();
            }
        }
        else if (isOnAir)
        {
            if (isThrowing)
            {
                transform.position = Vector2.MoveTowards(transform.position, new Vector2(target.position.x, player.position.y), speed * Time.fixedDeltaTime);
                //lightRigid.velocity = new Vector2(this.transform.position.x - player.position.x + 1f, (player.position.y - this.transform.position.y));
            }
            else if (isSlowDone)
            {
                transform.position = Vector2.MoveTowards(transform.position, new Vector2(player.position.x, player.position.y), speed * Time.fixedDeltaTime);
                //lightRigid.velocity = new Vector2(lightRigid.velocity.x + (player.position.x - this.transform.position.x) * Time.fixedDeltaTime, (player.position.y - this.transform.position.y));
            }
        }
    }

    private void Fire()
    {
        this.transform.parent = null;
        speed = 1f;
        StartCoroutine(boost());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Slow_Point" && isThrowing)
        {
            isSlowDone = true;
            isThrowing = false;
            StopCoroutine(boost());
            StartCoroutine(boostOff());
            speed = -speed;
        }
    }

    IEnumerator boost()
    {
        while (true)
        {
            if (speed <= 20f)
                speed += speed * 0.2f;
            yield return new WaitForSeconds(0.2f);
        }
    }
    IEnumerator boostOff()
    {
        while (true)
        {
            if (speed >= -20f)
                speed += 10 * 0.2f;
            Debug.Log("Hey boost off"); 
            yield return new WaitForSeconds(0.1f);
        }
    }
}