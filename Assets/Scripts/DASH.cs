using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DASH : MonoBehaviour
{

    public Vector3 moveDirection;

    public const float maxDashTime = 1.0f;
    public float dashDistance = 10;
    public float dashStoppingSpeed = 0.1f;
    float currentDashTime = maxDashTime;
    float dashSpeed = 6;
    private Transform cameraTransform;
    CharacterController controller;


    void Start()
    {
        cameraTransform = Camera.main.transform;
    }
    private void Awake()
    {
        controller = GetComponent<CharacterController>();
       
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            currentDashTime = 0;
            
        }
        if (currentDashTime < maxDashTime)
        {
            moveDirection = cameraTransform .forward * dashDistance;
            currentDashTime += dashStoppingSpeed;
        }
        else
        {
            moveDirection = Vector3.down;
        }
        controller.Move(moveDirection * Time.deltaTime * dashSpeed);
       
    }
}
