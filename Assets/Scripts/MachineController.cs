using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineController : MonoBehaviour {

    [SerializeField] AudioClip machineActivating;
    [SerializeField] GameObject whiteMask;
    [SerializeField] Cinemachine.CinemachineVirtualCamera vcam;
    [SerializeField] PowerBox powerBox;

    [SerializeField] TextAsset text;
    [SerializeField] TextBoxManager textBox;

    [SerializeField] int startLine;
    [SerializeField] int endLine;

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
            if(!FindObjectOfType<GameSession>().gotFlipFlop) {
                startLine = 20;
                endLine = 20;
                RestartTextBox();
            }

            if(!powerBox.isActivated) {
                startLine = 21;
                endLine = 21;
                RestartTextBox();
            }

            if(powerBox.isActivated && FindObjectOfType<GameSession>().gotFlipFlop) {
                AudioSource.PlayClipAtPoint(machineActivating, Camera.main.transform.position);
                ActivateMachine();
            }
        }
    }

    private void ActivateMachine() {
        FindObjectOfType<GameSession>().ProcessSuccessGame(whiteMask, vcam);
    }


    private void RestartTextBox() {
        textBox.ReloadScript(text);
        textBox.currentLine = startLine;
        textBox.endLine = endLine;
        textBox.EnableTextBox();
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("My Player"))
        {
            textBox.DisableTextBox();
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
