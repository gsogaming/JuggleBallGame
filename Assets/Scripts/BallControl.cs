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
    public float maxBallVelocity;

    public int score;
    public TextMeshProUGUI scoreText;

    public AudioSource soundEffect;
    public AudioClip[] ballSounds;
    

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
        
    }

    void FixedUpdate()
    {
        Vector3 gravity = globalGravity * gravityScale * Vector3.up;
        if (ballRb.velocity.y <= 0)
        {
            ballRb.AddForce(gravity * 2.5f, ForceMode.Acceleration);
            
        }
        else
        {

            ballRb.AddForce(gravity * 1.5f, ForceMode.Acceleration);          
            
        }

        if (ballRb.velocity.y >= maxBallVelocity)
        {
            ballRb.velocity = new Vector3(ballRb.velocity.x, maxBallVelocity,ballRb.velocity.z);
            Debug.Log(ballRb.velocity);
        }
        else if (ballRb.velocity.y <= -maxBallVelocity)
        {
            ballRb.velocity = new Vector3(ballRb.velocity.x, -maxBallVelocity, ballRb.velocity.z);
            Debug.Log(ballRb.velocity);
        }

        

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Foot") && !gamemanager.isGameOver)
        {
            score++;
            scoreText.text = "Score: " + score.ToString();            
        }

        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground") && !gamemanager.isGameOver && gamemanager.hasGameStarted)
        {
            gamemanager.GameOver();
        }

        if (collision.gameObject.CompareTag("Foot") && !gamemanager.isGameOver)
        {
            soundEffect.clip = ballSounds[Random.Range(0, ballSounds.Length)];
            soundEffect.Play();           

            ballRb.drag = 2;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Foot") && !gamemanager.isGameOver)
        {
            ballRb.drag = 0;
        }
    }

}
