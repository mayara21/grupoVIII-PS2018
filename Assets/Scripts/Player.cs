using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    [SerializeField] float speed = 7f;
    [SerializeField] float jumpSpeed = 7.5f;
    [SerializeField] float controlXSpeedJump = 2 / 3f;

    [SerializeField] Cinemachine.CinemachineVirtualCamera cameraDown;
    //[SerializeField] Cinemachine.CinemachineVirtualCamera vcam;

    bool isAlive;

    Rigidbody2D rigidBody;
    Collider2D playerCollider2D;
    Animator animator;


	// Use this for iniltialization
	void Start () {
        rigidBody = GetComponent<Rigidbody2D>();
        playerCollider2D = GetComponent<Collider2D>();
        animator = GetComponent<Animator>();

        controlXSpeedJump *= speed; 
	}

    // Update is called once per frame
    void Update() {
        IsTouchingPlug();
        Move();
        Jump();
        FlipSpriteX();

        if (Input.GetKey(KeyCode.DownArrow) && playerCollider2D.IsTouchingLayers(LayerMask.GetMask("Suspended Platform"))) {
            playerCollider2D.isTrigger = true;
        }
    }


	private void LateUpdate() {
        cameraDown.enabled = false;
        //cameraDown.transform.position = new Vector3(vcam.transform.position.x, cameraDown.transform.position.y, cameraDown.transform.position.z);

        if (Input.GetKey(KeyCode.DownArrow) && playerCollider2D.IsTouchingLayers(LayerMask.GetMask("Ground"))) {
            cameraDown.enabled = true;
        }
    }


	private void OnTriggerExit2D(Collider2D collision) {
        playerCollider2D.isTrigger = false;
	}

    /*private void OnTriggerStay2D(Collider2D collision) {
        if (collision.gameObject.tag == "Plug" && Input.GetKeyDown(KeyCode.UpArrow)) {
            SwitchDimension();
        }
        print(collision.gameObject.tag);
    }*/

	private void IsTouchingPlug() {
        if (!playerCollider2D.IsTouchingLayers(LayerMask.GetMask("Plug"))) return;

        if(Input.GetKeyDown(KeyCode.UpArrow)) {
            SwitchDimension();
        }
    }

	private void Move() {
        bool isWalking = false;
        Vector2 playerSpeed = rigidBody.velocity;

        bool isJumping = Mathf.Abs(rigidBody.velocity.y) > 0.0;

        if (Input.GetKey(KeyCode.RightArrow))
        {
            if (isJumping) playerSpeed.x = controlXSpeedJump;
            else playerSpeed.x = speed;
            isWalking = true;
        }

        else if (Input.GetKey(KeyCode.LeftArrow)) {
            if (isJumping) playerSpeed.x = -controlXSpeedJump;
            else playerSpeed.x = -speed;
            isWalking = true;
        }

        rigidBody.velocity = playerSpeed;

        animator.SetBool("Walking", isWalking && !isJumping);
	}


    private void Jump() {
        if (!playerCollider2D.IsTouchingLayers(LayerMask.GetMask("Ground", "Floor Platform", "Suspended Platform")) 
            || rigidBody.velocity.y != 0.0f) return;

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
