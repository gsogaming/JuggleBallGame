using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BallControl : MonoBehaviour
{


    Rigidbody ballRb;
    public float gravityScale = 1.0f;
    public static float globalGravity = -9.81f;

    private int score;
    public TextMeshProUGUI scoreText;

    public AudioSource soundEffect;
    public AudioSource music;
    public AudioClip[] ballSounds;
    public AudioClip gameOverSound;

    private GameManager gamemanager;


    // Start is called before the first frame update
    void OnEnable()
    {
        ballRb = GetComponent<Rigidbody>();
        ballRb.useGravity = false;
        gamemanager = FindObjectOfType<GameManager>().GetComponent<GameManager>();

    }

    private void Start()
    {
        ballRb.AddForce(transform.up * 50, ForceMode.Impulse);
    }

    void FixedUpdate()
    {
        Vector3 gravity = globalGravity * gravityScale * Vector3.up;
        if (ballRb.velocity.y <= 1)
        {
            ballRb.AddForce(gravity * 1.5f, ForceMode.Acceleration);
        }
        else
        {
            ballRb.AddForce(gravity, ForceMode.Acceleration);
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Foot") && !gamemanager.isGameOver)
        {
            score++;
            scoreText.text = "Score :" + score.ToString();

            
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground") && !gamemanager.isGameOver)
        {
            gamemanager.isGameOver = true;
            music.Stop();
            soundEffect.clip = gameOverSound;
            soundEffect.Play();
        }

        if (collision.gameObject.CompareTag("Foot") && !gamemanager.isGameOver)
        {
            soundEffect.clip = ballSounds[Random.Range(0, ballSounds.Length)];
            soundEffect.Play();
        }
    }

}
