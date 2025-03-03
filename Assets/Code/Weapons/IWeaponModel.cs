using UnityEngine;

namespace Code.Weapons
{
	public interface IWeaponModel
	{
		WeaponView Prefab { get; }
		float ShootCooldown { get; }
		float Range { get; }
		float ReleaseRange => Range + 1;
		Vector2 Spread { get; }
		LayerMask HitLayerMask { get; }
	}
}