using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeetController : MonoBehaviour
{

    public GameObject rightFoot, leftFoot;
    public GameObject orientationObject;
    private Vector3 originalPosRight;
    private Vector3 originalPosLeft;
    private Vector3 originalRotRight;
    private Vector3 originalRotLeft;
    private bool rightFootSelected;
    private bool leftFootSelected;
    private Vector3 targetPos;

    Rigidbody rightFootRb;
    Rigidbody leftFootRb;

    Collider rightFootCol;
    Collider leftFootCol;

    private GameManager gameManager;

    private void Start()
    {
        originalPosRight = rightFoot.transform.position;
        originalPosLeft = leftFoot.transform.position;

        originalRotLeft = leftFoot.transform.rotation.eulerAngles;
        originalRotRight = rightFoot.transform.rotation.eulerAngles;

        rightFootRb = rightFoot.GetComponent<Rigidbody>();
        leftFootRb = leftFoot.GetComponent<Rigidbody>();

        rightFootCol = rightFoot.GetComponent<Collider>();
        leftFootCol = leftFoot.GetComponent<Collider>();

        gameManager = FindObjectOfType<GameManager>().GetComponent<GameManager>();



    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && Input.mousePosition.x < Screen.width / 2 && !gameManager.isGameOver && gameManager.hasGameStarted)
        {
            rightFootSelected = false;  
            leftFootSelected = true;
            
        }
        else if (Input.GetMouseButtonDown(0) && Input.mousePosition.x >= Screen.width / 2 && !gameManager.isGameOver && gameManager.hasGameStarted)
        {
            leftFootSelected = false;
            rightFootSelected = true;
            
        }

        if (Input.GetMouseButtonUp(0) || gameManager.isGameOver)
        {
            rightFootSelected = false;
            leftFootSelected = false;

            leftFoot.transform.position = originalPosLeft;
            rightFoot.transform.position = originalPosRight;

            leftFoot.transform.rotation = Quaternion.Euler(originalRotLeft);
            rightFoot.transform.rotation = Quaternion.Euler(originalRotRight);
        }

        if (rightFootSelected)
        {
            rightFoot.transform.LookAt(orientationObject.transform);
        }
        else if (leftFootSelected)
        {
            leftFoot.transform.LookAt(orientationObject.transform);
        }

        rightFootCol.enabled = rightFootSelected;
        leftFootCol.enabled = leftFootSelected;

        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 10;
        targetPos = Camera.main.ScreenToWorldPoint(mousePos);
        if (targetPos.y <= -3.6f)
        {
            targetPos.y = -3.6f;
        }
        Debug.Log(targetPos);

        
    }

    private void FixedUpdate()
    {
        if (rightFootSelected)
        {
            rightFootRb.MovePosition(targetPos);            
            leftFoot.transform.position = originalPosLeft;  
            
        }
        else if (leftFootSelected)
        {
            leftFootRb.MovePosition(targetPos);
            //leftFoot.transform.LookAt(orientationObject.transform);
            rightFoot.transform.position = originalPosRight;
        }

    }

    
}
