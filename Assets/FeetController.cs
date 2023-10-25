using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeetController : MonoBehaviour
{    
    
    public GameObject rightFoot, leftFoot;
    public GameObject orientationObject;
    private Vector3 targetPosition;
    private Vector3 originalPosRight;
    private Vector3 originalRotRight;
    private Vector3 originalPosLeft;
    private Vector3 originalRotLeft;
    private bool rightFootSelected;
    private bool leftFootSelected;
    

  



    private void Start()
    {
        
        originalPosRight = rightFoot.transform.position;
        originalRotRight = rightFoot.transform.rotation.eulerAngles;
        originalPosLeft = leftFoot.transform.position;
        originalRotLeft = leftFoot.transform.rotation.eulerAngles;

              
    }


    // Update is called once per frame
    void Update()
    {
        ButtonControls();
        


    }

    private void ButtonControls()
    {
        if (Input.GetMouseButton(0))
        {

            // Get the mouse position in screen coordinates
            Vector3 mousePos = Input.mousePosition;

            // Set the Z position to a constant value to ensure the object doesn't get too close or too far
            mousePos.z = 10;

            // Convert the screen coordinates to world coordinates
            targetPosition = Camera.main.ScreenToWorldPoint(mousePos);
            rightFootSelected = false;
            leftFootSelected = true;

        }
        else if (Input.GetMouseButton(1))
        {
            // Get the mouse position in screen coordinates
            Vector3 mousePos = Input.mousePosition;

            // Set the Z position to a constant value to ensure the object doesn't get too close or too far
            mousePos.z = 10;

            // Convert the screen coordinates to world coordinates
            targetPosition = Camera.main.ScreenToWorldPoint(mousePos);
            rightFootSelected = true;
            leftFootSelected = false;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            
            leftFootSelected = false;
            leftFoot.transform.position = originalPosLeft;
            leftFoot.transform.rotation = Quaternion.Euler(originalRotLeft);
        }
        else if (Input.GetMouseButtonUp(1))
        {
            
            rightFootSelected = false;
            rightFoot.transform.position = originalPosRight;
            rightFoot.transform.rotation = Quaternion.Euler(originalRotRight);
        }

    }

    private void FixedUpdate()
    {
        if (rightFootSelected)
        {
            rightFoot.GetComponent<Rigidbody>().MovePosition(targetPosition);
            rightFoot.transform.LookAt(orientationObject.transform.position);
            rightFoot.GetComponent<Collider>().enabled = true;
            leftFoot.GetComponent<Collider>().enabled = false;


        }
        else if (leftFootSelected)
        {

            leftFoot.GetComponent<Rigidbody>().MovePosition(targetPosition);
            leftFoot.transform.LookAt(orientationObject.transform.position);
            leftFoot.GetComponent<Collider>().enabled = true; ;
            rightFoot.GetComponent<Collider>().enabled = false;
        }
       

    }
}
