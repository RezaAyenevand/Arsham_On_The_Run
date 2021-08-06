using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightManager : MonoBehaviour
{
    [SerializeField] public float speed;
    [Range(0, 1)] [SerializeField] public float boostSpeed;
    [Range(0, 1)] [SerializeField] public float offBoostSpeed;
    [SerializeField] public float speedThreshold;
    [SerializeField] public float vel;

    public GameObject player;
    private PlayerInput input;
    private Rigidbody2D rb;
    public Transform returnPoint;
    public float offsetValue;
    private float travelSpeed;
    public float timeTillStop;
    public Transform slowPoint;
    private float boostOffDistance;
    float speed0;
    Vector2 pos0;

    private bool onAir = false;
    private bool isReturning = false;
    private bool isSlow = false;
    private bool isBoostingOff = false;

    float timer, acc;


    private Vector3 fixPosition;


    // Start is called before the first frame update
    private void Awake()
    {
        onAir = false;
        rb = this.GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        input = player.GetComponent<PlayerInput>();
        fixPosition = transform.position - player.transform.position;
        boostOffDistance = returnPoint.position.x - slowPoint.position.x;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (input.firePressed && !onAir)
        {
            onAir = true;
            travelSpeed = speed;
            Fire();
        }
        else if (onAir)
        {
            if (!isReturning)
            {
                if (!isBoostingOff)
                {
                    transform.position = Vector2.MoveTowards(transform.position, new Vector2(returnPoint.position.x + 1000000000, transform.position.y), travelSpeed * Time.fixedDeltaTime);
                }
                if (!isBoostingOff && transform.position.x >= slowPoint.position.x)
                {
                    isBoostingOff = true;
                    StopCoroutine(boost());
                    timer = Time.time;
                    timeTillStop = timeTillStop;
                    acc = ((2 * (-(travelSpeed * timeTillStop))) / Mathf.Pow(timeTillStop, 2));
                    Debug.Log("boostoffD = " + boostOffDistance + "travel speed = " + travelSpeed + "tts = " + timeTillStop + "acc = " + acc);
                    speed0 = travelSpeed;
                    Debug.Log("x = " + speed0);

                    //StartCoroutine(boostOff());
                }
                else if (isBoostingOff)
                {
                    float remainTime = (Time.time - timer);
                    float remainDistance = returnPoint.transform.position.x - transform.position.x;
                    transform.position = new Vector2((((acc * Mathf.Pow(remainTime, 2) / 2)) + (speed0 * remainTime) + slowPoint.position.x), transform.position.y);
                    //travelSpeed = x + (acc * remainTime);
                    Debug.Log("11111111speed = " + travelSpeed);
                    Debug.Log("11111111remainDistance = " + remainDistance + "remainTime = " + remainTime + "acc = " + acc);

                    if (remainTime >= timeTillStop / 2)
                    {
                        isBoostingOff = false;
                        isReturning = true;
                        Debug.Log("22222222remainDistance = " + remainDistance + "remainTime = " + remainTime);
                        Debug.Log("2222222speed = " + travelSpeed);
                        timer = Time.time;
                        pos0 = transform.position;
                    }
                }
            }
            else if (isReturning)
            {
                float remainTime = (Time.time - timer);
                transform.position = new Vector2(((-speed0 * remainTime) + pos0.x), (-speed0 * remainTime) + pos0.y);
                //transform.position = Vector2.MoveTowards(transform.position, player.transform.position + fixPosition, x*Time.fixedDeltaTime);
                if (transform.position.x <= player.transform.position.x + offsetValue)
                {
                    isReturning = false;
                    onAir = false;
                }
            }

        }
    }

    private void Fire()
    {
        this.transform.parent = null;
        StartCoroutine(boost());
    }

    IEnumerator boost()
    {
        while (true)
        {
            if (travelSpeed <= speedThreshold)
                travelSpeed += travelSpeed * boostSpeed;
            yield return new WaitForSeconds(0.2f);
        }
    }

    //IEnumerator boostOff()
    //{
    //    float fixDecrease = travelSpeed * offBoostSpeed;
    //    while (true)
    //    {
    //        if (travelSpeed > 0)
    //            travelSpeed -= boostOffSpeed;
    //        yield return new WaitForSeconds(boostOffTimeScale);
    //    }
    //}

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.gameObject.tag == "Return_Point")
    //    {
    //        isReturning = true;
    //    }else if(collision.gameObject.tag == "Slow_Point")
    //    {
    //        isReturning = false;
    //        isSlow = true;
    //    }
    //}
}
