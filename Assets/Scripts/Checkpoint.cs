using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour {

    [SerializeField] Checkpoint otherCheckPoint;

    [SerializeField] Sprite thisActivated;
    [SerializeField] Sprite otherActivated;

    [SerializeField] AudioClip checkpointSFX;

    SpriteRenderer spriteRend;
    SpriteRenderer otherSpriteRend;

    public bool isActivated = false;

	// Use this for initialization
	void Start () {
        spriteRend = GetComponent<SpriteRenderer>();
        otherSpriteRend = otherCheckPoint.GetComponent<SpriteRenderer>();
	}


	private void OnTriggerEnter2D(Collider2D collision)
	{
        if(!isActivated && collision.gameObject.CompareTag("My Player")) {
            isActivated = true;
            otherCheckPoint.isActivated = true;

            AudioSource.PlayClipAtPoint(checkpointSFX, Camera.main.transform.position);

            FindObjectOfType<GameSession>().SaveGameState();

            spriteRend.sprite = thisActivated;
            otherSpriteRend.sprite = otherActivated;
        }
	}
}
