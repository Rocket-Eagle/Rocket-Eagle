using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class BirdController : MonoBehaviour
{

    Rigidbody2D rigidBody;
    public Vector2 startingVelocity = new Vector2(5,0);
    public Vector2 penalty = new Vector2(5,0);
    public Vector2 horizontalAcceleration = new Vector2(1, 0);

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        rigidBody.velocity = startingVelocity;
    }

    // Update is called once per frame
    void Update()
    {
        if (rigidBody.velocity.x < startingVelocity.x) {
            rigidBody.velocity = rigidBody.velocity + horizontalAcceleration * Time.deltaTime;
        }
        
    }

    void OnTriggerEnter2D(Collider2D col) {
        if (col.gameObject.tag == "Pipe") {
            rigidBody.position = rigidBody.position - penalty;
            rigidBody.velocity = new Vector2(0,0);
        }
    }
}
