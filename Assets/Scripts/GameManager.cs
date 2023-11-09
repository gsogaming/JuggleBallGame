using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    [Header("Game Loop Related")]
    [Tooltip("Is the game over or not")]
    public bool isGameOver = false;
    [Tooltip("has the game started or not")]
    public bool hasGameStarted = false;

    [Header("Manu GameObjects")]
    public GameObject gameOverScreen;
    public GameObject startGameButton;
    public GameObject scoreText;
    public GameObject homeScreenButton;
    public GameObject buyButton;
    public GameObject bottomMenu;    


    [Header("In Game referances")]
    public GameObject ball;
    private Rigidbody ballrb;
    private Transform ballTransform;
    private BallControl ballScript;
    private FeetController feetScript;
    public AudioSource soundEffects;
    public AudioSource music;
    public AudioClip gameOverSound;
    public AudioClip startGameSound;

    private Vector3 originalBallPos;

    // Start is called before the first frame update
    void Start()
    {
        isGameOver = false;
        ballrb = ball.GetComponent<Rigidbody>();
        ballTransform = ball.GetComponent<Transform>();
        originalBallPos = ballTransform.position;
        ballScript = ball.GetComponent<BallControl>();
        feetScript = FindObjectOfType<FeetController>().GetComponent<FeetController>();

        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (!hasGameStarted)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }

    }

    public void RestartGame()
    {
            
        ballScript.score = 0;         
        StartTheGame();
        music.Play();
    }

    public void StartTheGame()
    {
        isGameOver = false;
        hasGameStarted = true;

        ballTransform.position = originalBallPos;

        soundEffects.clip = startGameSound;
        soundEffects.Play();

        startGameButton.SetActive(false);
        gameOverScreen.SetActive(false);

        homeScreenButton.SetActive(false);

        buyButton.SetActive(false);

        bottomMenu.SetActive(false);

        scoreText.GetComponent<TextMeshProUGUI>().text = "Score: " + 0;
        scoreText.SetActive(true);

        ballrb.AddForce(transform.up * 50, ForceMode.Impulse);

        feetScript.leftFootStaminaBar.gameObject.SetActive(true);
        feetScript.rightFootStaminaBar.gameObject.SetActive(true);

    }

    public void GameOver()
    {
        isGameOver = true;        
        gameOverScreen.SetActive(true);

        music.Stop();
        soundEffects.clip = gameOverSound;
        soundEffects.Play();

        homeScreenButton.SetActive(true);

        feetScript.leftFootStaminaBar.gameObject.SetActive(false);
        feetScript.rightFootStaminaBar.gameObject.SetActive(false);
        
    }

    public void HomeScreenActivation()
    {
        bottomMenu.SetActive(true);
        startGameButton.SetActive(false);

        feetScript.leftFootStaminaBar.gameObject.SetActive(false);
        feetScript.rightFootStaminaBar.gameObject.SetActive(false);
    }



    
}
