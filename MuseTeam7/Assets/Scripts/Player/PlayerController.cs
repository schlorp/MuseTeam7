using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public CharacterController controller;

    [Header("Sprinting")]
    public float speed = 12f;
    public bool isSprinting = false;
    public float sprintMultiplier;

    [Header("Crouching")]
    public bool isCrouching = false;
    public float crouchMultiplier;
    public float standingHeight = 3.8f;
    public float crouchingHeight = 3.25f;

    [Header("Jumping")]
    public float gravity = -19.62f;
    public float jumpHeight = 3f;
    Vector3 velocity;

    [Header("GroundCheck")]
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    bool isGrounded;
    private void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //movement stuff 
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask); // checks if player is on the ground

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal"); // gets the horizontal coords
        float z = Input.GetAxis("Vertical"); // gets the vertical coords

        Vector3 move = transform.right * x + transform.forward * z; // movement 
        controller.Move(move * speed * Time.deltaTime); // gives move speeds

        if (Input.GetButtonDown("Jump") && isGrounded) // jump mechanic
        {
            Debug.Log("junp!");
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        // this is a working sprint function
        if (Input.GetKey(KeyCode.LeftShift) && isCrouching == false)
        {
            isSprinting = true;
        }
        else
        {
            isSprinting = false;
            speed = 12f;
        }

        if (Input.GetKey(KeyCode.C))
        {
            isCrouching = true;
        }
        else
        {
            isCrouching = false;
            speed = 12f;
        }

        if (isCrouching == true)
        {
            controller.height = crouchingHeight;
            speed *= crouchMultiplier;
        }
        else
        {
            controller.height = standingHeight;
        }

        if (isSprinting == true) // move speed while sprinting
        {
            speed *= sprintMultiplier;
            if (speed > 24f)
            {
                speed = 24f;
            }
        }

        velocity.y += gravity * Time.deltaTime; // adds gravity to player model
        controller.Move(velocity * Time.deltaTime); // allows falling
    }

}
