using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameSession : MonoBehaviour {

    [SerializeField] Player player;
    [SerializeField] TMPro.TMP_Text scoreText;
    [SerializeField] Image lifeDisplay;
    [SerializeField] Image shotsDisplay;
    [SerializeField] Image tapeDisplay;
    [SerializeField] Image flipFlipDisplay;

    [SerializeField] Sprite tapeCaught;
    [SerializeField] Sprite flipFlopCaught;
    [SerializeField] Sprite[] lifeSprite;
    [SerializeField] Sprite[] shotSprite;

    [SerializeField] int initialShots = 0;
    [SerializeField] int score = 10;


    int playerLives;
    int amountOfShots;

	private void Awake() {
        int numGameSessions = FindObjectsOfType<GameSession>().Length;
        if(numGameSessions > 1) {
            Destroy(gameObject);
        }
        else {
            DontDestroyOnLoad(gameObject);
        }
	}

	// Use this for initialization
	void Start () {
        playerLives = player.GetComponent<Player>().Lives;
        //print(playerLives);
        amountOfShots = initialShots;
        scoreText.text = score.ToString();
        //lifeSprite = new Sprite[4];
        //shotSprite = new Sprite[4];
	}
	
    public void ProcessPlayerDamage(int damage) {
        //print(playerLives);
        if(playerLives > 1) {
            TakeLife(damage);
        }

        else {
            print("GAME OVER!!");
            ResetGameSession();
        }
    }

    public void ProcessGetLife() {
        playerLives += 1;
        lifeDisplay.sprite = lifeSprite[playerLives];
    }

    public void ProcessUseShot() {
        amountOfShots -= 1;
        shotsDisplay.sprite = shotSprite[amountOfShots];
    }

    public void ProcessGetMoreShots() {
        //print("Pegar mais tiros!");
        amountOfShots += 1;
        //print(amountOfShots);
        shotsDisplay.sprite = shotSprite[amountOfShots];
    }

    private void TakeLife(int damage) {
        playerLives -= damage;
        //lifeDisplay.
        lifeDisplay.sprite = lifeSprite[playerLives];
    }

    public void ProcessScoreIncrease(int value) {
        score += value;
        scoreText.text = score.ToString();
    }

    public void ProcessTapeCaught() {
        tapeDisplay.sprite = tapeCaught;
    }

    public void ProcessFlipFlipCaught() {
        flipFlipDisplay.sprite = flipFlopCaught;
    }

    private void ResetGameSession() {
        SceneManager.LoadScene(0);
        Destroy(gameObject);
    }

}
