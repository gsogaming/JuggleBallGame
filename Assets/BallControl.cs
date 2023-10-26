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


    // Start is called before the first frame update
    void OnEnable()
    {
        ballRb = GetComponent<Rigidbody>();
        ballRb.useGravity = false;
    }

    void FixedUpdate()
    {
        Vector3 gravity = globalGravity * gravityScale * Vector3.up;
        ballRb.AddForce(gravity, ForceMode.Acceleration);
    }

    private void OnCollisionExit (Collision collision)
    {
        if (collision.gameObject.CompareTag("Foot"))
        {
            score++;
            scoreText.text = "Score :" + score.ToString();
        }
    }
}
