using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextBoxManager : MonoBehaviour {

    [SerializeField] GameObject textBox;
    //[SerializeField] TextMeshPro text;
    [SerializeField] TMPro.TMP_Text text;
    [SerializeField] TextAsset textFile;
    [SerializeField] Player player;
    [SerializeField] string[] textLines;

    public int currentLine;
    public int endLine;
    [SerializeField] bool isActive;
    [SerializeField] bool stopPlayer;

	// Use this for initialization
	void Start () {
        if(textFile != null) {
            textLines = (textFile.text.Split('\n'));
        }

        if(isActive) {
            EnableTextBox();
        }
	}
	
	// Update is called once per frame
	void Update () {

        if (!isActive) return;

        text.text = textLines[currentLine];

        if(Input.GetKeyDown(KeyCode.Return)) {
            currentLine += 1;
        }

        if(currentLine > endLine) {
            print(currentLine);
            print(endLine);
            DisableTextBox();
        }
	}

    public void DisableTextBox() {
        textBox.transform.parent.gameObject.SetActive(false);
        //textBox.SetActive(false);
        isActive = false;

        player.canMove = true;
    }

    public void EnableTextBox() {
        textBox.transform.parent.gameObject.SetActive(true);
        //textBox.SetActive(true);
        isActive = true;

        if(stopPlayer) {
            player.canMove = false;
        }
    }

    public void ReloadScript(TextAsset text) {
        if(text != null) {
            textLines = new string[1];
            textLines = (text.text.Split('\n'));
        }
    }
}
