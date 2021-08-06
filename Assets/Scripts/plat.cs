using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class plat : MonoBehaviour
{
    [SerializeField] public GameObject targetPose;
    [SerializeField] public float speed;
    Spawner spawner;
    
    GameObject player;
    EdgeCollider2D objectCollider;
    float critical;
    

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerMovement>().gameObject;
        objectCollider = gameObject.GetComponent<EdgeCollider2D>();
        critical = objectCollider.transform.position.y + objectCollider.offset.y;
    }

    // Update is called once per frame
    void Update()
    {

    }
    
    private void FixedUpdate()
    {
        
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(targetPose.transform.position.x, transform.position.y),
            speed * Time.fixedDeltaTime);
        
        
        
    }
}