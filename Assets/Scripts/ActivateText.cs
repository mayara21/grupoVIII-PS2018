using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateText : MonoBehaviour {

    [SerializeField] TextAsset text;
    [SerializeField] TextBoxManager textBox;

    [SerializeField] int startLine;
    [SerializeField] int endLine;
    [SerializeField] bool destroyAfterAppearing;
    //[SerializeField] bool stopPlayer;


    void RestartTextBox(Collider2D collision) {

        //print(collision.gameObject.tag);
        if (collision.gameObject.CompareTag("My Player"))
        {
            textBox.ReloadScript(text);
            textBox.currentLine = startLine;
            textBox.endLine = endLine;
            textBox.EnableTextBox();
        }

        if (destroyAfterAppearing) {
            Destroy(gameObject);
        }
    }

	private void OnTriggerStay2D(Collider2D collision)
	{
        RestartTextBox(collision);
	}

	private void OnTriggerEnter2D(Collider2D collision)
    {
        RestartTextBox(collision);
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
        if (collision.gameObject.CompareTag("My Player")) {
            textBox.DisableTextBox();
        }
	}
}
