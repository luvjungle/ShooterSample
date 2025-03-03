using Code.Weapons.Bullets.Damagers;
using UnityEngine;

namespace Code.Weapons.Bullets
{
	public interface IGenericBulletModel : IAreaDamagerModel, IHitDamagerModel
	{
		BulletController BulletPrefab { get; }
		float Speed { get; }
		float Radius { get; }
		float LifeTime { get; }
		ParticleSystem HitParticleSystem { get; }
	}
}