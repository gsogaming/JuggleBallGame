using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FeetController : MonoBehaviour
{

    public GameObject rightFoot, leftFoot;
    private Vector3 originalPosRight;
    private Vector3 originalPosLeft;
    private Vector3 originalRotRight;
    private Vector3 originalRotLeft;
    private bool rightFootSelected;
    private bool leftFootSelected;
    private Vector3 targetPos;
    public GameObject orientationObject;

    Rigidbody rightFootRb;
    Rigidbody leftFootRb;

    Collider rightFootCol;
    Collider leftFootCol;

    private GameManager gameManager;

    public Slider leftFootStamina, rightFootStamina;
    public float staminaReloadRate;
    public float staminaDepleteRate = 10;
    public float maxStamina;
    private float leftStamina;
    private float rightStamina;
    private bool leftStaminaDepleted;
    private bool rightStaminaDepleted;
    public GameObject LeftStamDepletionWarning;
    public GameObject rightStamDepletionWarning;
   

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

        leftStamina = maxStamina;
        rightStamina = maxStamina;
        UpdateStaminaUI();



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

        if (Input.GetMouseButtonUp(0) || gameManager.isGameOver )
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

        StaminaControl();
        UpdateStaminaUI();


    }

    private void FixedUpdate()
    {
        if (rightFootSelected && !rightStaminaDepleted)
        {
            rightFootRb.MovePosition(targetPos);
            leftFoot.transform.position = originalPosLeft;

        }
        else if (leftFootSelected && !leftStaminaDepleted)
        {
            leftFootRb.MovePosition(targetPos);            
            rightFoot.transform.position = originalPosRight;
        }

    }

    void StaminaControl()
    {
        if (leftFootSelected)
        {
            leftStamina -= staminaDepleteRate * Time.deltaTime;
            rightStamina += staminaReloadRate * Time.deltaTime;
        }
        
        if (rightFootSelected)
        {
            rightStamina -= staminaDepleteRate * Time.deltaTime;
            leftStamina += staminaReloadRate * Time.deltaTime;
        }

        if (!rightFootSelected && !leftFootSelected)
        {
            leftStamina += staminaReloadRate * Time.deltaTime;
            rightStamina += staminaReloadRate * Time.deltaTime;
        }

        if (rightStamina <= 1)
        {
            rightStaminaDepleted = true;
            rightStamDepletionWarning.SetActive(true);
        }

        if (leftStamina <= 1)
        {
            leftStaminaDepleted = true;
            LeftStamDepletionWarning.SetActive(true);
        }

        if (rightStamina >= 99)
        {
            rightStaminaDepleted = false;
            rightStamDepletionWarning.SetActive(false);
        }

        if (leftStamina >= 99)
        {
            leftStaminaDepleted = false;
            LeftStamDepletionWarning.SetActive(false);
        }

        rightStamina = Mathf.Clamp(rightStamina, 0, 100);
        leftStamina = Mathf.Clamp(leftStamina, 0, 100);
        Debug.Log(rightStamina);
    }

    

    void UpdateStaminaUI()
    {
        leftFootStamina.value = leftStamina / maxStamina;
        rightFootStamina.value = rightStamina / maxStamina;
    }


}
