using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{

    public float speed = 1f;
    [SerializeField] float minimalYPos = 2.3f;
    [SerializeField] float maximalYPos = 4.5f;

    bool isActive = true;
    bool isGoingUp = true;

    Rigidbody2D rigidBody;

    // Use this for initialization
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isActive)
        {
            Move();
        }
    }

    private void Move() {
        if(isGoingUp && this.transform.position.y < maximalYPos) {
            rigidBody.velocity = new Vector2(0f, speed);
        }

        print(isGoingUp);
        if(transform.position.y >= maximalYPos) {
            isGoingUp = false;
        }
        print(isGoingUp);

        if(!isGoingUp && this.transform.position.y > minimalYPos) {
            rigidBody.velocity = new Vector2(0f, -speed);
        }

        print(isGoingUp);

        print(transform.position.y);
        print(minimalYPos);
        if(transform.position.y <= minimalYPos) {
            isGoingUp = true;
        }
        print(isGoingUp);
    }

}
