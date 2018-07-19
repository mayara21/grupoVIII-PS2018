using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnLevel : MonoBehaviour {
    [SerializeField] float levelLoadDelay = 1.2f;

	private void OnTriggerEnter2D(Collider2D collision) {
        print("Entrei aqui");
        StartCoroutine(LoadPriorLevel());
	}

    private IEnumerator LoadPriorLevel() {
        yield return new WaitForSecondsRealtime(levelLoadDelay);

        var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex - 1);
    }

}
