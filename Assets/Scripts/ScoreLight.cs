using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreLight : MonoBehaviour
{

    private void Start()
    {
        Transform lastPlat = FindObjectOfType<Spawner>().lastPlat.transform; ;
        Vector2 lastLpos = transform.position - lastPlat.position;
        transform.parent = lastPlat;
        //transform.position = new Vector2(transform.parent.position.x, transform.parent.position.y + 0.7f);
        transform.localPosition = new Vector2(lastLpos.x, lastLpos.y + 0.7f);
    }   
    private void OnCollisionEnter2D(Collision2D otherCollider)
    {
        Destroy(gameObject);
        FindObjectOfType<GameSession>().AddLightScore();
        FindObjectOfType<GameSession>().AddScore(20);

    }

}
