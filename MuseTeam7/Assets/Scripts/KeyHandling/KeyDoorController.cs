using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace KeySystem
{
	public class KeyDoorController : MonoBehaviour
	{
		private Animator _doorAnim;
		private bool doorOpen = false;

		[Header("Animation Names")]
		[SerializeField] private string _openAnimationName = "DoorOpen";
		[SerializeField] private string _closeAnimationName = "DoorClose";

		[SerializeField] private int _timeToShowUI = 1;
		[SerializeField] private GameObject _showDoorLockedUI = null;

		[SerializeField] private KeyInventory _keyInventory = null;
		
		[SerializeField] private int _waitTimer = 1;
		[SerializeField] private bool pauseInteraction = false;
	
		private void Awake()
		{
			_doorAnim = gameObject.GetComponent<Animator>(); 
		}

		private IEnumerator PauseDoorInteraction()
		{
			pauseInteraction = true;
			yield return new WaitForSeconds(_waitTimer);
			pauseInteraction = false;
		}

		public void PlayAnimation()
		{
			if (_keyInventory.hasfirstKey) // looks for the first key in inventory and if so then plays animation
			{
				OpenDoor();
			}
			else
			{
				StartCoroutine(showDoorLocked());
			}

			if (_keyInventory.hassecondKey) // looks for the second key in inventory and if so then plays animation
			{
				OpenDoor();
			}
			else
			{
				StartCoroutine(showDoorLocked());
			}

			if (_keyInventory.hasthirdKey) // looks for the third key in inventory and if so then plays animation
			{
				OpenDoor();
			}
			else
			{
				StartCoroutine(showDoorLocked());
			}
		}

		void OpenDoor()
		{
			if (!doorOpen && !pauseInteraction)
			{
				_doorAnim.Play(_openAnimationName, 0, 0.0f);
				doorOpen = true;
				StartCoroutine(PauseDoorInteraction());
			}

			else if (doorOpen && !pauseInteraction)
			{
				_doorAnim.Play(_closeAnimationName, 0, 0.0f);
				doorOpen = false;
				StartCoroutine(PauseDoorInteraction());
			}
		}

		IEnumerator showDoorLocked()
		{
			_showDoorLockedUI.SetActive(true);
			yield return new WaitForSeconds(_timeToShowUI);
			_showDoorLockedUI.SetActive(false);
		}

	}
}
