using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpener : MonoBehaviour
{
    public float interactDistance = 5f;

    void Update()
    {
            if (Input.GetKeyDown(KeyCode.F))
		    {
            
            Ray ray = new Ray(transform.position, transform.forward);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, interactDistance))
			{
                if (hit.collider.CompareTag("Door"))
				{
                    DoorHandler doorScript = hit.collider.transform.parent.GetComponent<DoorHandler>();
                    if (doorScript == null) return;

                    if (Inventory.keys[doorScript.index] == true)
                    {
                        doorScript.ChangeDoorState();
                    }
				}
                else if (hit.collider.CompareTag("Key"))
				{
                    Inventory.keys[hit.collider.GetComponent<Key>().index] = true; // looks what index key has and changes the bool to true
                    Destroy(hit.collider.gameObject);
				}
			}
		}    
    }
}
