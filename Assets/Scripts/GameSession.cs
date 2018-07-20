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

    [SerializeField] AudioClip itemSFX;

    [SerializeField] int initialShots = 0;
    [SerializeField] int score = 0;

    [SerializeField] int hallwayIndex1 = 3;
    [SerializeField] int hallwayIndex2 = 5;


    public int playerLives;
    public int amountOfShots;
    public bool gotTape = false;
    public bool gotFlipFlop = false;



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
        if (PlayerPrefs.HasKey("Lives")) playerLives = PlayerPrefs.GetInt("Lives");
        else playerLives = player.GetComponent<Player>().Lives;

        //print(playerLives);

        if (PlayerPrefs.HasKey("Amount of Shots")) amountOfShots = PlayerPrefs.GetInt("Amount of Shots");
        else amountOfShots = initialShots;

        if (PlayerPrefs.HasKey("Score")) score = PlayerPrefs.GetInt("Score");

        UpdateDisplay();
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
            //ResetGameSession();
        }
    }

    public void ProcessGetLife() {
        playerLives += 1;
        lifeDisplay.sprite = lifeSprite[playerLives];
        AudioSource.PlayClipAtPoint(itemSFX, Camera.main.transform.position);
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
        AudioSource.PlayClipAtPoint(itemSFX, Camera.main.transform.position);
    }

    private void TakeLife(int damage) {
        playerLives -= damage;
        //lifeDisplay.
        lifeDisplay.sprite = lifeSprite[playerLives];
    }

    public void ProcessScoreIncrease(int value, Collider2D collision) {
        score += value;
        scoreText.text = score.ToString();
        if(collision != null && collision.gameObject.CompareTag("Batteries")) AudioSource.PlayClipAtPoint(itemSFX, Camera.main.transform.position);
    }

    public void ProcessTapeCaught() {
        tapeDisplay.sprite = tapeCaught;
        gotTape = true;
        AudioSource.PlayClipAtPoint(itemSFX, Camera.main.transform.position);
    }

    public void ProcessFlipFlipCaught() {
        flipFlipDisplay.sprite = flipFlopCaught;
        gotFlipFlop = true;
        AudioSource.PlayClipAtPoint(itemSFX, Camera.main.transform.position);
    }

    public void ProcessSuccessGame(GameObject whiteMask, Cinemachine.CinemachineVirtualCamera vcam) {
        print("Entrei aqui");
        FindObjectOfType<Player>().canMove = false;
        StartCoroutine(ActivateMask(whiteMask));
        StartCoroutine(WaitForEffect(whiteMask, vcam));
        StartCoroutine(FinalCutscene());
    }

    private IEnumerator ActivateMask(GameObject whiteMask) {
        yield return new WaitForSeconds(.7f);
        whiteMask.SetActive(true);
    }

    private IEnumerator WaitForEffect(GameObject whiteMask, Cinemachine.CinemachineVirtualCamera vcam) {
        yield return new WaitForSeconds(3.3f);
        whiteMask.SetActive(false);
        vcam.gameObject.SetActive(true);
    }

    private IEnumerator FinalCutscene() {
        yield return new WaitForSeconds(2f);
        var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex + 1);
    }

    /*private void ResetGameSession() {
        SceneManager.LoadScene(0);
        Destroy(gameObject);
    }*/

    public void SaveGameState() {
        print("Estou salvando!!");
        PlayerPrefs.SetInt("Lives", playerLives);
        PlayerPrefs.SetInt("Amount of Shots", amountOfShots);
        PlayerPrefs.SetInt("Score", score);

        if (gotTape) PlayerPrefs.SetInt("Tape", 1);
        else PlayerPrefs.SetInt("Tape", 0);

        if (gotFlipFlop) PlayerPrefs.SetInt("Flip Flop", 1);
        else PlayerPrefs.SetInt("Flip Flop", 0);

        var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        //print(currentSceneIndex);
        if(currentSceneIndex == hallwayIndex1 || currentSceneIndex == hallwayIndex2) {
            PlayerPrefs.SetInt("Current Scene Index", currentSceneIndex + 1);
        }
        else PlayerPrefs.SetInt("Current Scene Index", currentSceneIndex);

        print(PlayerPrefs.GetInt("Lives"));

        PlayerPrefs.Save();
    }

    public void LoadOnDeath() {
        print("Vamos carregar após a morte");
        playerLives = Player.maxLives;
        PlayerPrefs.SetInt("Lives", playerLives);
        PlayerPrefs.Save();
        LoadSavedGameState();
    }

    public void LoadOnContinue() {
        playerLives = PlayerPrefs.GetInt("Lives");
        LoadSavedGameState();
    }

    private void LoadSavedGameState() {
        
        amountOfShots = PlayerPrefs.GetInt("Amount of Shots");
        score = PlayerPrefs.GetInt("Score");

        if (PlayerPrefs.GetInt("Tape") == 1) {
            gotTape = true;
        }
        else gotTape = false;

        if (PlayerPrefs.GetInt("Flip Flop") == 1) {
            gotFlipFlop = true;
        }
        else gotFlipFlop = false;

        SceneManager.LoadScene(PlayerPrefs.GetInt("Current Scene Index"));

        UpdateDisplay();
    }

    private void UpdateDisplay() {
        //print("Atualizando o display");
        shotsDisplay.sprite = shotSprite[amountOfShots];
        lifeDisplay.sprite = lifeSprite[playerLives];
        scoreText.text = score.ToString();

        if(gotFlipFlop) flipFlipDisplay.sprite = flipFlopCaught;
        if(gotTape) tapeDisplay.sprite = tapeCaught;
    }

    public static void ResetGameState() {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
    }

}
