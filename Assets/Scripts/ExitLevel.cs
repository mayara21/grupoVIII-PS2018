using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitLevel : MonoBehaviour {

    [SerializeField] float levelLoadDelay = 1f;
    [SerializeField] int firstLevelIndex = 2;
    [SerializeField] int secondLevelIndex = 3;

    [SerializeField] TextAsset text;
    [SerializeField] TextBoxManager textBox;

    [SerializeField] int startLine;
    [SerializeField] int endLine;

    bool isLoadingScene = false;


	private void OnTriggerEnter2D(Collider2D collision)
	{
        var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        if(!isLoadingScene && collision.gameObject.CompareTag("My Player") && canPassLevel(currentSceneIndex)) {
            print("Oi");
            isLoadingScene = true;
            StartCoroutine(LoadNextScene(collision, currentSceneIndex));
        }

        else if (!canPassLevel(currentSceneIndex)) {
            RestartTextBox(collision);
        }

	}

    private IEnumerator LoadNextScene(Collider2D collision, int currentSceneIndex) {
        print("Agora entrei aqui");

        yield return new WaitForSeconds(levelLoadDelay);

        SceneManager.LoadScene(currentSceneIndex + 1);
        isLoadingScene = false;

    }

    private bool canPassLevel(int currentSceneIndex) {
        if(currentSceneIndex == firstLevelIndex) {
            if (!FindObjectOfType<GameSession>().gotFlipFlop) return false;
        }

        if(currentSceneIndex == secondLevelIndex) {
            if (!FindObjectOfType<GameSession>().gotTape) return false;
        }

        return true;
    }


    void RestartTextBox(Collider2D collision)
    {

        //print(collision.gameObject.tag);
        if (collision.gameObject.CompareTag("My Player"))
        {
            textBox.ReloadScript(text);
            textBox.currentLine = startLine;
            textBox.endLine = endLine;
            textBox.EnableTextBox();
        }

        //if (destroyAfterAppearing) Destroy(gameObject);
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("My Player"))
        {
            textBox.DisableTextBox();
        }
    }


}
