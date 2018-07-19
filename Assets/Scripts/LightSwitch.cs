using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSwitch : MonoBehaviour {

    [SerializeField] GameObject darkMask;
    [SerializeField] Sprite disabledSwitch;

    Collider2D switchCollider;
    SpriteRenderer spriteRend;


	// Use this for initialization
	void Start () {
        switchCollider = GetComponent<Collider2D>();
        spriteRend = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
        if (!switchCollider.IsTouchingLayers(LayerMask.GetMask("Player"))) return;

        if(Input.GetKeyDown(KeyCode.UpArrow)) {
            Destroy(darkMask);
            switchCollider.enabled = false;
            spriteRend.sprite = disabledSwitch;
        }
		
	}
}
