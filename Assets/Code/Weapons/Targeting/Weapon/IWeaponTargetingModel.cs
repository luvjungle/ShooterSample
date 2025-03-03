using UnityEngine;

namespace Code.Weapons.Targeting.Weapon
{
	public interface IWeaponTargetingModel
	{
		Transform Shooter { get; }
		Transform TargetingRaycastPoint { get; }
		IDamageable Target { get; set; }
		IWeaponModel Weapon { get; set; }
	}
}