using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitHallway : MonoBehaviour {

    [SerializeField] float levelLoadDelay = 1f;

    bool isLoadingScene = false;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        if (!isLoadingScene && collision.gameObject.CompareTag("My Player"))
        {
            isLoadingScene = true;
            StartCoroutine(LoadNextScene(collision, currentSceneIndex));
        }

    }

    private IEnumerator LoadNextScene(Collider2D collision, int currentSceneIndex)
    {
        print("Entrei aqui");
        yield return new WaitForSeconds(levelLoadDelay);

        SceneManager.LoadScene(currentSceneIndex + 1);
        isLoadingScene = false;

    }

}
