using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseFollow : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    private Rigidbody rb;
    private Vector3 targetPosition;
    private Vector3 originalPos;
    [SerializeField] bool rightFoot;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        
        originalPos = transform.position;
        if (this.gameObject.tag == "RightFoot")
        {
            rightFoot = true;
        }
        else
        {
            rightFoot = false;
        }
        
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0) && rightFoot)
        {            
            
            // Get the mouse position in screen coordinates
            Vector3 mousePos = Input.mousePosition;

            // Set the Z position to a constant value to ensure the object doesn't get too close or too far
            mousePos.z = 10;

            // Convert the screen coordinates to world coordinates
            targetPosition = Camera.main.ScreenToWorldPoint(mousePos);
        }
        else if(Input.GetMouseButton(1) && !rightFoot)
        {
            // Get the mouse position in screen coordinates
            Vector3 mousePos = Input.mousePosition;

            // Set the Z position to a constant value to ensure the object doesn't get too close or too far
            mousePos.z = 10;

            // Convert the screen coordinates to world coordinates
            targetPosition = Camera.main.ScreenToWorldPoint(mousePos);
        }
        else
        {
            transform.position = originalPos;
        }
        
    }
        

    private void FixedUpdate()
    {
        rb.MovePosition(targetPosition);
    }
}
