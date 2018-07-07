using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SampleEnemy : MonoBehaviour {

    //[SerializeField] GameObject energyBallPrefab;
    //[SerializeField] Transform energyBallSpawn;
    [SerializeField] Player player;

    //[SerializeField] float energyBallSpeed = 1f;
    [SerializeField] float damage = 1;
    [SerializeField] float speed = 2f;
    [SerializeField] float postAttackDelay = 2f;
    [SerializeField] float paralizedTime = 3f;
    [SerializeField] float lives = 10f;
    [SerializeField] float postDamageImmuneTime = 1f;

    [SerializeField] float minimalXPos = 12f;
    [SerializeField] float maximalXPos = 20f;

    Rigidbody2D rigidBody2D;
    Collider2D enemyCollider;
    Animator animator;

    bool canMove = true;
    bool isAlive = true;
    bool takingDamage = false;
    bool canTakeDamage = true;

    GameObject energyBall = null;
    //int numEnergyBalls = 0;

	// Use this for initialization
	void Start () {
        rigidBody2D = GetComponent<Rigidbody2D>();
        enemyCollider = GetComponent<Collider2D>();
        animator = GetComponent<Animator>();

	}

	// Update is called once per frame
	private void Update()
	{
        if(lives < 0 && isAlive) {
            Die();
        }
	}

	void FixedUpdate () {
        if(isAlive) {
            HandleMovement();
        }
	}


	private void HandleMovement() {
        if (canMove) {
            if (IsFacingRight() && this.transform.position.x < maximalXPos) {
                rigidBody2D.velocity = new Vector2(speed, 0f);
            }

            else if (!IsFacingRight() && this.transform.position.x > minimalXPos){
                rigidBody2D.velocity = new Vector2(-speed, 0f);
            }

            else {
                SwitchMovement();
            }
        }

        else {
            rigidBody2D.velocity = new Vector2(0f, 0f);
        }

    }

	private bool IsFacingRight() {
        return transform.localScale.x > 0;
    }

    private void SwitchMovement() {
        transform.localScale = new Vector2(-(Mathf.Sign(rigidBody2D.velocity.x)), 1f);
    }


	private void OnCollisionEnter2D(Collision2D collision)
	{
        //print(collision.gameObject.tag);
        if(collision.gameObject.CompareTag("Wall")) {
            SwitchMovement();
        }

        if(collision.gameObject.CompareTag("My Player")) {
            StartCoroutine(PostAttackDelay());
        }

        if(collision.gameObject.CompareTag("Energy Ball")) {
            StartCoroutine(Paralized());
        }

        /*if(collision.gameObject.CompareTag("Suction Area")) {
            Transform col = collision.gameObject.GetComponentInParent<Transform>();
            SuckEnergyBall(col);
        }*/

        if(collision.gameObject.CompareTag("Suction Area")) {
            if(canTakeDamage && isAlive) {
                float takenDamage = collision.gameObject.GetComponentInParent<Player>().Damage;
                StartCoroutine(SuckEnergy(takenDamage));
            }
        }
	}


	private IEnumerator SuckEnergy(float takenDamage) {
        print("levando dano");
        lives -= takenDamage;
        canTakeDamage = false;
      
        yield return new WaitForSeconds(postDamageImmuneTime);
        canTakeDamage = true;
    }

	/*private void OnCollisionStay2D(Collision2D collision) {
        print(collision.gameObject.tag);
        if (collision.gameObject.CompareTag("Suction Area"))
        {
            Transform col = collision.gameObject.GetComponentInParent<Transform>();
            SuckEnergyBall(col);
        }
	}*/
    


	private IEnumerator PostAttackDelay() {
        canMove = false;
        yield return new WaitForSeconds(postAttackDelay);
        canMove = true;
    }

    private IEnumerator Paralized() {
        canMove = false;
        yield return new WaitForSeconds(paralizedTime);
        canMove = true;
    }

    private void Die() {
        print("Inimigo morto");
        isAlive = false;
        rigidBody2D.Sleep();
        enemyCollider.enabled = false;
        //animator.SetBool("Dead", !isAlive);
    }

    /*private void SuckEnergyBall(Transform col) {
        //var energyBall = (GameObject)Instantiate(energyBallPrefab, energyBallSpawn.position, energyBallSpawn.rotation);
        //print("Entrei no método");
        if (energyBall == null || energyBall.Equals(null)) {
            energyBallPrefab.GetComponent<Collider2D>().isTrigger = true;
            energyBall = (GameObject)Instantiate(energyBallPrefab, this.transform.position, this.transform.rotation);
            var dir = (col.position - this.transform.position).normalized;
            energyBall.GetComponent<Rigidbody2D>().velocity = new Vector2(dir.x * energyBallSpeed, 0f);

            //energyBall.GetComponent<Collider2D>().isTrigger = false;
            energyBallPrefab.GetComponent<Collider2D>().isTrigger = false;  
        }

    }*/


	/*private void OnTriggerStay2D(Collider2D collision) {
        print(collision.gameObject.tag);
        if(enemyCollider.IsTouchingLayers(LayerMask.GetMask("Suction Area"))) {
        }
	}*/

	/*private void OnTriggerExit2D(Collider2D collision) // para quando para de tocar o chão (caso inimigo esteja em cima de uma plataforma)
	{
        SwitchMovement();
	}*/

	/*private void OnTriggerEnter2D(Collider2D collision){
        if(enemyCollider2D.IsTouchingLayers(LayerMask.GetMask("Suction Area"))) {
            SuctionBall();
        }
    }

    private void SuctionBall() {
        var energyBall = (GameObject)Instantiate(energyBallPrefab, energyBallSpawn.position, energyBallSpawn.rotation);
        energyBall.GetComponent<Rigidbody2D>().velocity = new Vector2(-1 * player.transform.localScale.x * energyBallSpeed, 0f);
    }*/


}
