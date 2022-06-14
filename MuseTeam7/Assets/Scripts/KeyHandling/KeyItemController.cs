using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KeySystem
{
    public class KeyItemController : MonoBehaviour
    {
		[Header("1st Key")]
        [SerializeField] private bool firstDoor = false;
        [SerializeField] private bool firstKey = false;
		[Header("2nd Key")]
		[SerializeField] private bool secondDoor = false;
		[SerializeField] private bool secondKey = false;
		[Header("3rd Key")]
		[SerializeField] private bool thirdDoor = false;
		[SerializeField] private bool thirdKey = false;

		[Header("Key Inventory")]
		[SerializeField] private KeyInventory _keyInventory = null;

        private KeyDoorController doorObject;
		private void Start()
		{
			// gets the keydoor controller script component to check which door the player is interacting with
            if (firstDoor)
			{
                doorObject = GetComponent<KeyDoorController>();
			}
			
			if (secondDoor)
			{
				doorObject = GetComponent<KeyDoorController>();
			}
			
			if (thirdDoor)
			{
				doorObject = GetComponent<KeyDoorController>();
			}

		}

        public void ObjetInteraction() // checks which key the player is interacting with and then picks it up~
		{
            if (firstDoor)
			{
				doorObject.PlayAnimation();
			}
			else if (firstKey)
			{
				_keyInventory.hasfirstKey = true;
				gameObject.SetActive(false);
			}

			if (secondDoor)
			{
				doorObject.PlayAnimation();
			}
			else if (secondKey)
			{
				_keyInventory.hassecondKey = true;
				gameObject.SetActive(false);
			}


			if (thirdDoor)
			{
				doorObject.PlayAnimation();
			}
			else if (thirdKey)
			{
				_keyInventory.hasthirdKey = true;
				gameObject.SetActive(false);
			}
		}
	}
}