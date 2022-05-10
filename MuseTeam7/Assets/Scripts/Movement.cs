using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    public CharacterController controller;

    public float speed = 12f;
    public float gravity = -19.62f;
    Vector3 velocity;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    bool isGrounded;


    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask); // checks if player is on the ground

        if(isGrounded && velocity.y < 0)
		{
            velocity.y = -2f;
		}

        float x = Input.GetAxis("Horizontal"); // gets the horizontal coords
        float z = Input.GetAxis("Vertical"); // gets the vertical coords

        Vector3 move = transform.right * x + transform.forward * z; // movement 

        controller.Move(move * speed * Time.deltaTime); // gives move speeds

        velocity.y += gravity * Time.deltaTime; // adds gravity to player model

        controller.Move(velocity * Time.deltaTime); // allows falling
    }
}
