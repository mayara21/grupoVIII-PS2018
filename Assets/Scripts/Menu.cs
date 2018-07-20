using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour {

    [SerializeField] AudioClip menuButtonSFX;

    public void StartFirstScene() {
        SceneManager.LoadScene(1);
        ResetPrefs();
    }

    public void QuitGame() {
        StartCoroutine(WaitToQuit());
    }

    private IEnumerator WaitToQuit() {
        yield return new WaitForSeconds(1f);
        Application.Quit();
    }

    public void ContinueGame() {
        var savedScene = PlayerPrefs.GetInt("Current Scene Index");
        SceneManager.LoadScene(savedScene);
    }

    private void ResetPrefs() {
        GameSession.ResetGameState();
        //GameSession session = FindObjectOfType<GameSession>();
        //session.amountOfShots = 0;
        //session.playerLives = 3;
        //session.gotTape = false;
        //session.gotFlipFlop = false;
    }

    public void playSound() {
        AudioSource.PlayClipAtPoint(menuButtonSFX, Camera.main.transform.position);
    }
}
