using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetSpeed : MonoBehaviour
{
    public GameObject platform;
    public GameObject trapObject;

    private bool isRunning=true;
    [SerializeField] public float speed;
    plat Plat;
    plat[] plats;
    Trap trap;
    Trap[] traps;
    PlayerMovement Player;
    BackGroundMovement[] bgMovement;
    // Start is called before the first frame update
    void Start()
    {
        
        Plat = platform.GetComponent<plat>();
        trap = trapObject.GetComponent<Trap>();
        Player = FindObjectOfType<PlayerMovement>();
        bgMovement = FindObjectsOfType<BackGroundMovement>();
        StartCoroutine("Set");
    }

    // Update is called once per frame
    void Update()
    {
        Plat.speed = speed  ;
        foreach (var item in bgMovement)
        {
            if (item.tag == "foreground")
            {
                item.movementSpeed = speed ;
            }

        }
        Player.jumpSpeed = speed * speed / 2;
    }
    // new stuff Sprint 3
    public void SetFreeze(float freeze)
    {
        plat []platf = FindObjectsOfType<plat>();
        foreach (var item in platf)
        {
            item.speed = 0f;
        }
        foreach (var item in bgMovement)
        {
            item.movementSpeed = 0f;
        }
        Player.jumpSpeed = 0;
        isRunning = false;
        speed = freeze;
        
    }

    public float GetSpeed()
    {
        return speed;
    }
    IEnumerator Set()
    {
        while (isRunning)
        {
            
            if (speed < 5f) speed += 0.05f;
            plats = FindObjectsOfType<plat>();
            foreach (var item in plats)
            {
                item.speed = speed;
            }
            yield return new WaitForSeconds(2f);
        }
    }   
}
