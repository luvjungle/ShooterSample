using Code.Weapons.Bullets;
using UnityEngine;

namespace Code.Weapons
{
	public class WeaponModel : IWeaponModel, IGenericBulletModel
	{
		public WeaponView Prefab => _config.Prefab;
		public BulletController BulletPrefab => _config.BulletPrefab;
		public float Speed => _config.BulletSpeed;
		public float Radius => _config.BulletRadius;
		public float LifeTime => _config.BulletLifeTime;
		public float AreaDamage => _config.Damage;
		public float HitDamage => _config.Damage;
		public float DamageRadius => _config.DamageRadius;
		public float ExplosionForce => _config.ExplosionForce;
		public float ShootCooldown => _config.ShootCooldown;
		public float Range => _config.Range;
		public Vector2 Spread => _config.Spread;
		public LayerMask HitLayerMask => _config.HitLayerMask;
		public ParticleSystem HitParticleSystem => _config.HitParticleSystem;

		private readonly WeaponConfigSO _config;

		public WeaponModel(WeaponConfigSO config)
		{
			_config = config;
		}
	}
}