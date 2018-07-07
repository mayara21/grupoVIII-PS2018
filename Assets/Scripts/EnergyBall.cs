using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyBall : MonoBehaviour {

    Rigidbody2D rigidBody;
    Collider2D ballCollider;

	private void Start()
	{
        rigidBody = GetComponent<Rigidbody2D>();
        ballCollider = GetComponent<Collider2D>();
	}

	private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }


	private void OnTriggerEnter2D(Collider2D collision)
	{
        print(collision.gameObject.tag);
        if (collision.gameObject.CompareTag("Suction Area")) return;
        if(collision.gameObject.CompareTag("My Player")) {
            //adicionar na pontuação do player
            Destroy(gameObject);
        }
	}

	private void OnTriggerStay2D(Collider2D collision)
	{
		
	}

	/*private void OnTriggerExit2D(Collider2D collision)
	{
        //print(collision.gameObject.tag);
        if(collision.gameObject.CompareTag("Suction Area")) {
            rigidBody.velocity = new Vector2(0f, 0f);
            rigidBody.gravityScale = 1f;
            ballCollider.isTrigger = false;
        }
	}*/
}
