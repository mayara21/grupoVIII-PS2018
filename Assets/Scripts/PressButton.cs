using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressButton : MonoBehaviour {

    [SerializeField] MovingPlatform platform;
    [SerializeField] float platformSpeed = 1f;
    [SerializeField] Sprite disabledButton;

    Collider2D buttonCollider;
    SpriteRenderer spriteRend;

	// Use this for initialization
	void Start () {
        //platformRB = platform.GetComponent<Rigidbody2D>();
        buttonCollider = GetComponent<Collider2D>();
        spriteRend = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
        if (!buttonCollider.IsTouchingLayers(LayerMask.GetMask("Player"))) return;

        if(Input.GetKeyDown(KeyCode.UpArrow)) {
            print("Entrei");
            platform.GetComponent<MovingPlatform>().speed = platformSpeed;
            buttonCollider.enabled = false;
            spriteRend.sprite = disabledButton;
        }
		
	}

}
