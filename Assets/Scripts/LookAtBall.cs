using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtBall : MonoBehaviour
{
    public Transform ball;
    public float rotationSpeed;

    private Quaternion initialRotation;

    private GameManager gameManager;
    

    // Start is called before the first frame update
    void Start()
    {
        initialRotation = transform.rotation;
        gameManager = FindObjectOfType<GameManager>().GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.isGameOver) // Check your game manager's 'isGameOver' variable
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, initialRotation, Time.deltaTime * rotationSpeed);
        }
        else
        {
            Vector3 directionToBall = ball.position - transform.position;
            Quaternion targetRotation = Quaternion.LookRotation(-directionToBall);

            // Gradually interpolate the rotation
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
        }
    }
}
