using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coffee : MonoBehaviour {

	private void OnTriggerEnter2D(Collider2D collision)
	{
        /*if(collision.gameObject.CompareTag("Player")) {
            int lives = collision.gameObject.GetComponent<Player>().getLives()
            if()
        }*/
        if(collision.gameObject.GetComponent<Player>().Lives < Player.maxLives) {
            Destroy(gameObject);   
        }
	}
}
