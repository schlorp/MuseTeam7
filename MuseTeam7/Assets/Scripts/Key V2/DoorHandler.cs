using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorHandler : MonoBehaviour
{
	[Header("Index/Open")]
    public int index = -1;
    public bool open = false;
	[Header("rotation")]
    public float doorOpenAngle = 90f;
    public float doorCloseAngle = 0f;

	//smooth
    public float smooth = 2f;

    public void ChangeDoorState()
	{
        FindObjectOfType<audioManager>().Play("Open");
        open = !open;
	}


    void Update()
    {
         if (open) // open == true
        {
            Quaternion targetRotation = Quaternion.Euler(0, doorOpenAngle, 0);
            transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotation, smooth * Time.deltaTime);
            
        } 
         else
		{
            Quaternion targetRotation2 = Quaternion.Euler(0, doorCloseAngle, 0);
            transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotation2, smooth * Time.deltaTime);
		}
    }
}
