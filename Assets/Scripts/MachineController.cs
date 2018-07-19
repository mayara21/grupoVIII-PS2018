using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineController : MonoBehaviour {

    [SerializeField] GameObject whiteMask;
    [SerializeField] Cinemachine.CinemachineVirtualCamera vcam;

    Collider2D machineCollider;

	// Use this for initialization
	void Start () {
        machineCollider = GetComponent<Collider2D>();
	}
	
	// Update is called once per frame
	void Update () {
        if (!machineCollider.IsTouchingLayers(LayerMask.GetMask("Player"))) return;
        isBeingActivated();
	}

    private void isBeingActivated() {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            print("Entrei");
            FindObjectOfType<GameSession>().ProcessSuccessGame(whiteMask, vcam);
        }
    }

	/*private void OnTriggerStay2D(Collider2D collision)
	{
        //print(collision.gameObject.name);
        if(collision.gameObject.CompareTag("My Player")) {
            if(Input.GetKeyDown(KeyCode.UpArrow)) {
                print("Entrei");
                FindObjectOfType<GameSession>().ProcessSuccessGame(whiteMask, vcam);
            }
        }
	}*/
}
