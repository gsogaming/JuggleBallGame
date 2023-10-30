using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public bool isGameOver = false;
    public bool hasGameStarted = false;

    public AudioSource startTheGameSound;

    public GameObject gameOverScreen;
    public GameObject startGameButton;

    // Start is called before the first frame update
    void Start()
    {
        isGameOver = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isGameOver)
        {
            gameOverScreen.SetActive(true);
        }

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
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void StartTheGame()
    {
        hasGameStarted = true;
        startTheGameSound.Play();
        startGameButton.SetActive(false);
    }
}
