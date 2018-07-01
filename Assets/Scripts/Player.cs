using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    [SerializeField] float speed = 7f;
    [SerializeField] float jumpSpeed = 7.5f;
    [SerializeField] float controlSpeed = 2 / 3f;

    bool isAlive;

    Rigidbody2D rigidBody;
    Collider2D collider2D;
    Animator animator;


	// Use this for initialization
	void Start () {
        rigidBody = GetComponent<Rigidbody2D>();
        collider2D = GetComponent<Collider2D>();
        animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        TouchingPlug();
        Move();
        Jump();
        FlipSpriteX();
	}

    private void TouchingPlug() {
        if (!collider2D.IsTouchingLayers(LayerMask.GetMask("Plug"))) return;

        if(Input.GetKeyDown(KeyCode.UpArrow)) {
            SwitchDimension();
        }
    }

	private void Move() {
        Vector2 playerSpeed;
        bool isWalking = false;
        playerSpeed = rigidBody.velocity;

        bool isJumping = Mathf.Abs(rigidBody.velocity.y) > 0.0;

        if(Input.GetKey(KeyCode.RightArrow)) {
            if (isJumping) playerSpeed.x = speed * controlSpeed;
            else playerSpeed.x = speed;
            isWalking = true;
        }

        else if(Input.GetKey(KeyCode.LeftArrow)) {
            if (isJumping) playerSpeed.x = -speed * controlSpeed;
            else playerSpeed.x = -speed;
            isWalking = true;
        }

        /*else if(Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.LeftArrow)){
            playerSpeed.x = Mathf.Sign(rigidBody.velocity.x) * speed / 3;
        }*/

        rigidBody.velocity = playerSpeed;

        animator.SetBool("Walking", isWalking && !isJumping);
	}


    private void Jump() {
        if (!collider2D.IsTouchingLayers(LayerMask.GetMask("Ground")) || rigidBody.velocity.y != 0.0f) return;

        if(Input.GetKeyDown(KeyCode.Space)) {
            Vector2 jumpSpeedToAdd = new Vector2(0f, jumpSpeed);
            rigidBody.velocity += jumpSpeedToAdd;
        }
    }


    private void FlipSpriteX() {
        bool playerHasHorizontalSpeed = Mathf.Abs(rigidBody.velocity.x) > 0.0;

        if(playerHasHorizontalSpeed) {
            transform.localScale = new Vector2(Mathf.Sign(rigidBody.velocity.x), Mathf.Sign(rigidBody.gravityScale));
        }
    }

    private void FlipSpriteY() {
        transform.localScale = new Vector2(1f, Mathf.Sign(rigidBody.gravityScale));
    }

    private void SwitchDimension() {
        transform.position = new Vector3(transform.position.x, -transform.position.y, transform.position.z);
        rigidBody.gravityScale *= -1;
        jumpSpeed *= -1;
        FlipSpriteY();
    }

}
