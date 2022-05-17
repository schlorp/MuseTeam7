using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    [SerializeField] private LayerMask PickupLayer;
    [SerializeField] private Camera PlayerCam;
    [SerializeField] private float throwingForce; 
    [SerializeField] private float PickupRange;
    [SerializeField] private Transform Hand;

    private Rigidbody CurrentObjrb;
    private Collider CurrentObjCol;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
		{
            Ray Pickupray = new Ray(PlayerCam.transform.position, PlayerCam.transform.forward);

            if (Physics.Raycast(Pickupray, out RaycastHit hitInfo, PickupRange, PickupLayer))
			{
                if (CurrentObjrb)
				{
                    CurrentObjrb.isKinematic = false;
                    CurrentObjCol.enabled = true;

                    CurrentObjrb = hitInfo.rigidbody;
                    CurrentObjCol = hitInfo.collider;

                    CurrentObjrb.isKinematic = true;
                    CurrentObjCol.enabled = false;
				}
                else
				{
                    CurrentObjrb = hitInfo.rigidbody;
                    CurrentObjCol = hitInfo.collider;

                    CurrentObjrb.isKinematic = true;
                    CurrentObjCol.enabled = false;
				}
                return;
			}

            if (CurrentObjrb)
			{
                CurrentObjrb.isKinematic = false;
                CurrentObjCol.enabled = true;

                CurrentObjrb = null;
                CurrentObjCol = null;
			}
		}

        if (Input.GetKeyDown(KeyCode.Q))
		{
            if (CurrentObjrb)
			{
                CurrentObjrb.isKinematic = false;
                CurrentObjCol.enabled = true;

                CurrentObjrb.AddForce(PlayerCam.transform.forward * throwingForce, ForceMode.Impulse);

                CurrentObjrb = null;
                CurrentObjCol = null;
			}
		}

        if (CurrentObjrb)
		{
            CurrentObjrb.position = Hand.position;
            CurrentObjrb.rotation = Hand.rotation;
		}

    }
}
