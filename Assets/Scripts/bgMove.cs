using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bgMove : MonoBehaviour
{
    [SerializeField] GameObject[] background;
    [SerializeField] GameObject[] foreground;
    [SerializeField] Camera mainCamera;
    [SerializeField] float movementSpeed;

    private float littleValue = 0.1f;
    private float fixPos;

    // Start is called before the first frame update
    void Start()
    {
        fixPos = Camera.main.transform.position.x - ((mainCamera.orthographicSize * mainCamera.aspect));
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }
    private void Move()
    {
        int j;
        for (int i = 0; i < background.Length; i++)
        {
            if ((background[i].transform.position.x + (background[i].GetComponent<SpriteRenderer>().bounds.size.x) / 2) < fixPos)
            {
                if (i == background.Length - 1)
                {
                    j = 0;
                }
                else
                {
                    j = i + 1;
                }
                background[i].transform.position = new Vector2(background[j].transform.position.x
                    + (background[i].GetComponent<SpriteRenderer>().bounds.size.x / 2) +
                    (background[j].GetComponent<SpriteRenderer>().bounds.size.x / 2) - littleValue,
                    background[i].transform.position.y);
            }
        }
        for (int i = 0; i < foreground.Length; i++)
        {
            if ((foreground[i].transform.position.x + (foreground[i].GetComponent<SpriteRenderer>().bounds.size.x) / 2) < fixPos)
            {
                if (i == foreground.Length - 1)
                {
                    j = 0;
                }
                else
                {
                    j = i + 1;
                }
                foreground[i].transform.position = new Vector2(foreground[j].transform.position.x
                    + (foreground[i].GetComponent<SpriteRenderer>().bounds.size.x / 2) +
                    (foreground[j].GetComponent<SpriteRenderer>().bounds.size.x / 2) - littleValue,
                    foreground[i].transform.position.y);
            }
        }
    }
}
