using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    [Header("In Game referances")]
    public GameObject ball;
    private Rigidbody ballrb;
    private Transform ballTransform;
    private BallControl ballScript;
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
        scoreText.SetActive(true);
        homeScreenButton.SetActive(false);
        buyButton.SetActive(false);        
        ballrb.AddForce(transform.up * 50, ForceMode.Impulse);
    }

    public void GameOver()
    {
        isGameOver = true;        
        gameOverScreen.SetActive(true);
        music.Stop();
        soundEffects.clip = gameOverSound;
        soundEffects.Play();
    }

    
}
