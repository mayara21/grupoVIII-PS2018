using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerBox : MonoBehaviour {

    [SerializeField] AudioClip activatedSFX;

    [SerializeField] TextAsset text;
    [SerializeField] TextBoxManager textBox;

    [SerializeField] int startLine;
    [SerializeField] int endLine;
    //[SerializeField] bool destroyAfterAppearing;

    Collider2D boxCollider;

    public bool isActivated = false;

	// Use this for initialization
	void Start () {
        boxCollider = GetComponent<Collider2D>();
	}
	
	// Update is called once per frame
	void Update () {

        if (!boxCollider.IsTouchingLayers(LayerMask.GetMask("Player"))) return;

        if(Input.GetKeyDown(KeyCode.UpArrow)) {

            AudioSource.PlayClipAtPoint(activatedSFX, Camera.main.transform.position);
            RestartTextBox();
            isActivated = true;
        }
		
	}

    private void RestartTextBox() {
        textBox.ReloadScript(text);
        textBox.currentLine = startLine;
        textBox.endLine = endLine;
        textBox.EnableTextBox();

    }

	private void OnTriggerExit2D(Collider2D collision)
	{
        if(collision.gameObject.CompareTag("My Player")) {
            boxCollider.enabled = false;
            textBox.DisableTextBox();
        }

	}


}
