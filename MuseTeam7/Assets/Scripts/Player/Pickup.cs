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

    private Rigidbody _CurrentObjrb;
    private Collider _CurrentObjCol;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
		{
            Ray Pickupray = new Ray(_PlayerCam.transform.position, _PlayerCam.transform.forward);

            if (Physics.Raycast(Pickupray, out RaycastHit hitInfo, _PickupRange, _PickupLayer))
			{
                if (_CurrentObjrb)
				{
                    _CurrentObjrb.isKinematic = false;
                    _CurrentObjCol.enabled = true;

                    _CurrentObjrb = hitInfo.rigidbody;
                    _CurrentObjCol = hitInfo.collider;

                    _CurrentObjrb.isKinematic = true;
                    _CurrentObjCol.enabled = false;
				}
                else
				{
                    _CurrentObjrb = hitInfo.rigidbody;
                    _CurrentObjCol = hitInfo.collider;

                    _CurrentObjrb.isKinematic = true;
                    _CurrentObjCol.enabled = false;
				}
                return;
			}

            if (_CurrentObjrb)
			{
                _CurrentObjrb.isKinematic = false;
                _CurrentObjCol.enabled = true;

                _CurrentObjrb = null;
                _CurrentObjCol = null;
			}
		}

        if (Input.GetKeyDown(KeyCode.Q))
		{
            if (_CurrentObjrb)
			{
                _CurrentObjrb.isKinematic = false;
                _CurrentObjCol.enabled = true;

                _CurrentObjrb.AddForce(_PlayerCam.transform.forward * _throwingForce, ForceMode.Impulse);

                _CurrentObjrb = null;
                _CurrentObjCol = null;
			}
		}

        if (_CurrentObjrb)
		{
            _CurrentObjrb.position = _Hand.position;
            _CurrentObjrb.rotation = _Hand.rotation;
		}

    }
}
