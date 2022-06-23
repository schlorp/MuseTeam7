using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    [SerializeField] private LayerMask _PickupLayer;
    [SerializeField] private Camera _PlayerCam;
    [SerializeField] private float _throwingForce; 
    [SerializeField] private float _PickupRange;
    [SerializeField] private Transform _Hand;

    private Rigidbody CurrentObjrb;
    private Collider CurrentObjCol;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
		{
            Ray Pickupray = new Ray(_PlayerCam.transform.position, _PlayerCam.transform.forward);

            if (Physics.Raycast(Pickupray, out RaycastHit hitInfo, _PickupRange, _PickupLayer))
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

                CurrentObjrb.AddForce(_PlayerCam.transform.forward * _throwingForce, ForceMode.Impulse);

                CurrentObjrb = null;
                CurrentObjCol = null;
			}
		}

        if (CurrentObjrb)
		{
            CurrentObjrb.position = _Hand.position;
            CurrentObjrb.rotation = _Hand.rotation;
		}

    }
}
