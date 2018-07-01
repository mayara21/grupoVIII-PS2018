using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayerX : MonoBehaviour {
    Player player;

	// Use this for initialization
	void Start () {
        player = FindObjectOfType<Player>();
	}
	
	// Update is called once per frame
	void LateUpdate () {
        transform.position = new Vector3(player.transform.position.x, transform.position.y, transform.position.z);
	}
}
