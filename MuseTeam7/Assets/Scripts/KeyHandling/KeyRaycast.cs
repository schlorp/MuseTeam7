using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace KeySystem
{
    public class KeyRaycast : MonoBehaviour
    {
        [SerializeField] private int _rayLength = 5;
        [SerializeField] private LayerMask _layerMaskInteract;
        [SerializeField] private string _excluseLayerName = null;

        private KeyItemController _raycastObject;
        [SerializeField] private KeyCode openDoorKey = KeyCode.Y;

        [SerializeField] private Image _crosshair = null;
        private bool _isCrosshairActive;
        private bool _doOnce;

        private string _interactableTag = "InteractiveObject";

		private void Update()
		{
            RaycastHit hit;
            Vector3 fwd = transform.TransformDirection(Vector3.forward);

            int mask = 1 << LayerMask.NameToLayer(_excluseLayerName) | _layerMaskInteract.value;

            if (Physics.Raycast(transform.position, fwd, out hit, _rayLength, mask))
			{
                if (hit.collider.CompareTag(_interactableTag))
				{
                    if (!_doOnce)
					{
                        _raycastObject = hit.collider.gameObject.GetComponent<KeyItemController>();
                        CrosshairChange(true);
					}
                    _isCrosshairActive = true;
                    _doOnce = true;

                    if (Input.GetKeyDown(openDoorKey))
					{
                        _raycastObject.ObjetInteraction();
					}

				}
			}
            else
			{
                if(_isCrosshairActive)
				{
                    CrosshairChange(false);
                    _doOnce = false;
				}
			}
		}
        void CrosshairChange(bool on)
		{
            if (on && !_doOnce)
			{
                _crosshair.color = Color.red;
			}
			else
			{
                _crosshair.color = Color.white;
                _isCrosshairActive = false;
			}
		}
	}
}