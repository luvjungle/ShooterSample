using System;
using UnityEngine;

namespace Code.Weapons.Targeting.MeleeWithTrigger
{
	public interface ITriggerView
	{
		event Action<Collider> TriggerEnter;
		event Action<Collider> TriggerExit; 
	}
}