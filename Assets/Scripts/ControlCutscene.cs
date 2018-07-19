using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ControlCutscene : MonoBehaviour {

    [SerializeField] Cinemachine.CinemachineVirtualCamera[] cameras;
    [SerializeField] int  currentCamera = 0;
    [SerializeField] float waitingTime = .8f;

	private void Start()
	{
        if (cameras.Length > 0) cameras[0].gameObject.SetActive(true);
	}


	// Update is called once per frame
	void LateUpdate () {
        if(Input.GetKeyDown(KeyCode.Return)) {
            currentCamera++;
            if(currentCamera < cameras.Length) {
                cameras[currentCamera - 1].gameObject.SetActive(false);
                cameras[currentCamera].gameObject.SetActive(true);
            }

            else {
                StartCoroutine(LoadGame());
            }
        }
	}

    private IEnumerator LoadGame() {
        yield return new WaitForSeconds(waitingTime);

        var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex + 1);

    }
}

