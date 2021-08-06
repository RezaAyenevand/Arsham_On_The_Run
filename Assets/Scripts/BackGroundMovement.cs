using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundMovement : MonoBehaviour
{
    [SerializeField] public float movementSpeed;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }
    private void Move()
    {
        transform.position = new Vector2(transform.position.x - (movementSpeed * Time.deltaTime), transform.position.y);

    }
}
