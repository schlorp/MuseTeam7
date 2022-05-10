using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float mouseSens = 100f;
    public Transform playerBody;

    float xRotation = 0f;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; // locks mouse in the window so you wont click outside the window
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSens * Time.deltaTime; // gets the X coordinate of mouse pos
        float mouseY = Input.GetAxis("Mouse Y") * mouseSens * Time.deltaTime; // gets the Y coordinate of mouse pos

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); // makes sure you can't over rotate the cam so you don't look behind you

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f); // y rotation
        playerBody.Rotate(Vector3.up * mouseX); // x rotation
    }
}
