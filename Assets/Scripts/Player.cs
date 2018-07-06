using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    [SerializeField] float speed = 7f;
    [SerializeField] float jumpSpeed = 7.5f;
    [SerializeField] float controlXSpeedJump = 2 / 3f;
    [SerializeField] float energyBallSpeed = 8f;
    [SerializeField] int temporaryDamage = 1;
    [SerializeField] float knockBackThrustX = 30f;
    [SerializeField] float knockBackThrustY = 100f;
    [SerializeField] float slowMoveSpeed = 3f;

    [SerializeField] GameObject energyBallPrefab;
    [SerializeField] Transform energyBallSpawn;
    [SerializeField] GameObject suctionArea;

    [SerializeField] Cinemachine.CinemachineVirtualCamera cameraDown;
    //[SerializeField] Cinemachine.CinemachineVirtualCamera vcam;

    int lives = 20;
    bool isAlive = true;
    bool canTakeDamage = true;
    bool canMove = true;
    bool isSucking = false;

    Rigidbody2D rigidBody;
    Collider2D playerCollider2D;
    Animator animator;


	// Use this for iniltialization
	void Start () {
        rigidBody = GetComponent<Rigidbody2D>();
        playerCollider2D = GetComponent<Collider2D>();
        animator = GetComponent<Animator>();
  	}

    // Update is called once per frame
    void Update() {
        if(!isSucking) {
            FlipSpriteX();
        }
    }

	private void FixedUpdate() {
        IsTouchingPlug();

        if(canMove && !isSucking) {
            Move();
        }

        if(isSucking) {
            SlowMove();
        }

        if(!isSucking) {
            Jump();
        }

        if (Input.GetKey(KeyCode.DownArrow) && playerCollider2D.IsTouchingLayers(LayerMask.GetMask("Suspended Platform")))
        {
            playerCollider2D.isTrigger = true;
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            Fire();
        }

        suctionArea.GetComponent<Collider2D>().enabled = false;
        isSucking = false;

        if (Input.GetKey(KeyCode.X))
        {
            SuckEnergyBall();
        }
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
        if(collision.gameObject.CompareTag("Enemy") && canTakeDamage) {
            Transform col = collision.gameObject.transform;
            StartCoroutine(KnockBack(col));
            StartCoroutine(TakeDamage());
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

	private void OnTriggerStay2D(Collider2D collision)
	{
        //print(collision.gameObject.tag);
	}

	private IEnumerator TakeDamage()
    {
        lives -= temporaryDamage;
        if(lives <= 0) {
            Destroy(gameObject);
        }
        canTakeDamage = false;
        //animator.SetBool("Immune", !canTakeDamage);
        yield return new WaitForSecondsRealtime(4f);

        canTakeDamage = true;
        //animator.SetBool("Immune", canTakeDamage);

    }

    private IEnumerator KnockBack(Transform col) {
        canMove = false;
        //print(transform.up);
        rigidBody.AddForce(new Vector2((this.transform.position - col.position).normalized.x * knockBackThrustX, knockBackThrustY));
        yield return new WaitForSeconds(.5f);
        canMove = true;
    }


	private void IsTouchingPlug() {
        if (!playerCollider2D.IsTouchingLayers(LayerMask.GetMask("Plug"))) return;

        if(Input.GetKeyDown(KeyCode.UpArrow)) {
            SwitchDimension();
        }
    }

    private void SuckEnergyBall() {
        suctionArea.GetComponent<Collider2D>().enabled = true;
        isSucking = true;
    }

    private void SlowMove() {
        bool isWalking = false;
        Vector2 playerSpeed = rigidBody.velocity;

        bool isJumping = Mathf.Abs(rigidBody.velocity.y) > 0.0;

        if (Input.GetKey(KeyCode.RightArrow))
        {
            playerSpeed.x = slowMoveSpeed;
            isWalking = true;
        }

        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            playerSpeed.x = -slowMoveSpeed;
            isWalking = true;
        }

        rigidBody.velocity = playerSpeed;

        //animator.SetBool("Walking while Vacuuming", isWalking && isSucking);
    }

    private void Fire() {
        var energyBall = (GameObject)Instantiate(energyBallPrefab, energyBallSpawn.position, energyBallSpawn.rotation);
        energyBall.GetComponent<Rigidbody2D>().velocity = new Vector2(transform.localScale.x * energyBallSpeed, 0f);

        Destroy(energyBall, 3.0f);
    }

	private void Move() {
        bool isWalking = false;
        Vector2 playerSpeed = rigidBody.velocity;

        bool isJumping = Mathf.Abs(rigidBody.velocity.y) > 0.0;

        if (Input.GetKey(KeyCode.RightArrow))
        {
            if (isJumping) playerSpeed.x = controlXSpeedJump * speed;
            else playerSpeed.x = speed;
            isWalking = true;
        }

        else if (Input.GetKey(KeyCode.LeftArrow)) {
            if (isJumping) playerSpeed.x = -controlXSpeedJump * speed;
            else playerSpeed.x = -speed;
            isWalking = true;
        }

        rigidBody.velocity = playerSpeed;

        animator.SetBool("Walking", isWalking && !isJumping);
	}


    private void Jump() {
        if (!playerCollider2D.IsTouchingLayers(LayerMask.GetMask("Ground", "Floor Platform", "Suspended Platform")) 
            || rigidBody.velocity.y != 0f) return;

        if(Input.GetKeyDown(KeyCode.Space)) {
            Vector2 jumpSpeedToAdd = new Vector2(0f, jumpSpeed);
            rigidBody.velocity += jumpSpeedToAdd;
        }
    }


    private void FlipSpriteX() {
        bool playerHasHorizontalSpeed = Mathf.Abs(rigidBody.velocity.x) > 0.0;

        if(playerHasHorizontalSpeed) {
            transform.localScale = new Vector2(Mathf.Sign(rigidBody.velocity.x), transform.localScale.y);
        }
    }

    private void FlipSpriteY() {
        transform.localScale = new Vector2(1f, -1 * transform.localScale.y);
    }

    private void SwitchDimension() {
        transform.position = new Vector3(transform.position.x, -transform.position.y, transform.position.z);
        rigidBody.gravityScale *= -1;
        jumpSpeed *= -1;

        FlipSpriteY();
    }

}
