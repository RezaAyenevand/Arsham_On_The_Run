using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class Spawner : MonoBehaviour
{

    [SerializeField] public GameObject[] gameObjects;
    [SerializeField] public TextAsset file;
    [SerializeField] public GameObject spawnPoint;
    [SerializeField] public float levelOffset = 1.5f;



    string[] lines;
    string[] tempLines;
    string fullString;
    string[] splitter = new string[] { "," };
    int counter = 0;
    float rubbyXCollider;
    public GameObject lastSpawn;
    public GameObject ground;
    public GameObject lastPlat;

    // Start is called before the first frame update
    void Start()
    {
        lastPlat = FindObjectOfType<plat>().gameObject;
        rubbyXCollider = FindObjectOfType<PlayerMovement>().GetComponent<BoxCollider2D>().size.x;
        splitLine();
        tempLines = lines[counter].Split(splitter, StringSplitOptions.RemoveEmptyEntries);
        Vector2 position = new Vector2(this.transform.position.x, levelOffset * float.Parse(tempLines[1]));
        lastSpawn = Instantiate(gameObjects[int.Parse(tempLines[2])], position, Quaternion.identity);
        if(float.Parse(tempLines[2]) == 0f)
        {
            lastPlat = lastSpawn;
        }
        counter ++;
        tempLines = lines[counter].Split(splitter, StringSplitOptions.RemoveEmptyEntries);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        if (this.transform.position.x - lastSpawn.transform.position.x >= float.Parse(tempLines[0]) * rubbyXCollider)
        {
            if (float.Parse(tempLines[2]) == 1f)
            {
                Vector2 position;
                if (float.Parse(tempLines[1]) == 0)
                {
                    position = new Vector2(this.transform.position.x, levelOffset * 0 + 0.1f);
                }
                else
                {
                    position = new Vector2(this.transform.position.x, levelOffset * float.Parse(tempLines[1]) + 0.25f);
                }
                lastSpawn = Instantiate(gameObjects[int.Parse(tempLines[2])], position, Quaternion.identity);
                //Debug.Log(tempLines[0] + "," + tempLines[1] + "," + tempLines[2]);
                if (counter >= lines.Length - 1)
                {
                    counter = -1;
                }
                counter++;
                tempLines = lines[counter].Split(splitter, StringSplitOptions.RemoveEmptyEntries);
            }
            else if (float.Parse(tempLines[2]) == 3f)
            {
                Vector2 position;
                if (float.Parse(tempLines[1]) == 0)
                {
                    position = new Vector2(this.transform.position.x, levelOffset * 0 + 0.1f);
                }
                else
                {
                    position = new Vector2(this.transform.position.x, levelOffset * float.Parse(tempLines[1]) + 0.25f);
                }
                lastSpawn = Instantiate(gameObjects[int.Parse(tempLines[2])], position, Quaternion.identity);
                //Debug.Log(tempLines[0] + "," + tempLines[1] + "," + tempLines[2]);
                if (counter >= lines.Length - 1)
                {
                    counter = -1;
                }
                counter++;
                tempLines = lines[counter].Split(splitter, StringSplitOptions.RemoveEmptyEntries);
            }
            else if (float.Parse(tempLines[2]) == 0f)
            {
                Vector2 position;
                position = new Vector2(this.transform.position.x, levelOffset * float.Parse(tempLines[1]));
                lastSpawn = Instantiate(gameObjects[int.Parse(tempLines[2])], position, Quaternion.identity);
                lastPlat = lastSpawn;
                //Debug.Log(tempLines[0] + "," + tempLines[1] + "," + tempLines[2]);
                if (counter >= lines.Length - 1)
                {
                    counter = -1;
                }
                counter++;
                tempLines = lines[counter].Split(splitter, StringSplitOptions.RemoveEmptyEntries);
            }
            else if (float.Parse(tempLines[2]) == 2f)
            {
                Vector2 position;
                position = new Vector2(this.transform.position.x, levelOffset * float.Parse(tempLines[1]));
                lastSpawn = Instantiate(gameObjects[int.Parse(tempLines[2])], position, Quaternion.identity);
                //Debug.Log(tempLines[0] + "," + tempLines[1] + "," + tempLines[2]);
                if (counter >= lines.Length - 1)
                {
                    counter = -1;
                }
                counter++;
                tempLines = lines[counter].Split(splitter, StringSplitOptions.RemoveEmptyEntries);
            }
        }
    }

    void splitLine()
    {
        fullString = file.text;
        lines = fullString.Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);
        for(int i = 0; i < lines.Length; i++)
        {
            
        }
    }

}