using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour {

    public void StartFirstScene() {
        SceneManager.LoadScene(1);
        ResetPrefs();
    }

    public void QuitGame() {
        Application.Quit();
    }

    private void ResetPrefs() {
        GameSession.ResetGameState();
        //GameSession session = FindObjectOfType<GameSession>();
        //session.amountOfShots = 0;
        //session.playerLives = 3;
        //session.gotTape = false;
        //session.gotFlipFlop = false;
    }
}
