using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
 
    [Header("General")]
    public float gravityScale = -20f;
    [Header("Movement")]
    public float walkSpeed = 5f;
    public float runSpeed = 10f;
    [Header("Jump")]
    public float jumpHeight = 1.9f;

    Vector3 moveInput = Vector3.zero;
    CharacterController characterController;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {

        if (characterController.isGrounded)
        {
        moveInput = new Vector3(
            Input.GetAxis("Horizontal"),
            0f,
            Input.GetAxis("Vertical"));

            moveInput = Vector3.ClampMagnitude(moveInput, 1f);
            

            if (Input.GetButton("Sprint"))
            {
                moveInput = transform.TransformDirection(moveInput) * runSpeed; // Local to Global, no scale
            }
            else
            {
                moveInput = transform.TransformDirection(moveInput) * walkSpeed; // Local to Global, no scale
            }

            if (Input.GetButtonDown("Jump"))
            {
                moveInput.y = Mathf.Sqrt(jumpHeight + -2f * gravityScale);
            }


        }

        moveInput.y += gravityScale * Time.deltaTime;
        characterController.Move(moveInput * Time.deltaTime);
    }

    
}
