using System;
using Code.Weapons.Targeting.MeleeWithTrigger;
using UnityEngine;

namespace Code.Utils
{
	public class TriggerView : MonoBehaviour, ITriggerView
	{
		public event Action<Collider> TriggerEnter;
		public event Action<Collider> TriggerExit; 

		private void OnTriggerEnter(Collider other)
		{
			TriggerEnter?.Invoke(other);
		}
		
		private void OnTriggerExit(Collider other)
		{
			TriggerExit?.Invoke(other);
		}
	}
}