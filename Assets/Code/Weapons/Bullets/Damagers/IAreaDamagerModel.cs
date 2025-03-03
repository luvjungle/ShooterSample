using UnityEngine;

namespace Code.Weapons.Bullets.Damagers
{
	public interface IAreaDamagerModel
	{
		float AreaDamage { get; }
		float DamageRadius { get; }
		float ExplosionForce { get; }
		LayerMask HitLayerMask { get; }
	}
}