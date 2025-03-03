using Cinemachine;
using Code.Utils;
using Code.Utils.PlayerLoop;
using Code.Weapons.Bullets.Damagers;
using Code.Weapons.Targeting;
using UnityEngine;
using VContainer;

namespace Code.Props
{
	public class Barrel : MonoBehaviour, IDamageable
	{
		[SerializeField] private BarrelConfigSO _config;
		[SerializeField] private Transform _fireVfxPosition;
		[SerializeField] private Renderer _renderer;
		[SerializeField] private CinemachineImpulseSource _impulseSource;

		private float _currentHealth;
		private BarrelVfxController _barrelVfx;
		private AreaBulletDamager _damager;

		public Transform Transform => transform;
		public Transform ShootPoint => transform;
		public bool Dead => _currentHealth <= 0;

		[Inject]
		public void Construct(Pool<ParticleSystem> particleSystemPool, LoopUpdater updater)
		{
			_barrelVfx = new BarrelVfxController(_config, updater, _fireVfxPosition, particleSystemPool);
			_damager = new AreaBulletDamager();
		}

		private void Awake()
		{
			_currentHealth = _config.Health;
		}

		public void TakeDamage(float damage)
		{
			if (Dead)
				return;

			_currentHealth -= damage;

			if (_currentHealth <= _config.Health * 0.5f)
				_barrelVfx.StartFire();

			if (Dead)
				OnDeath();
		}

		private void OnDeath()
		{
			_barrelVfx.OnDeath();

			var rndPos = Random.insideUnitCircle * 0.5f;
			var explosionPoint = transform.position + new Vector3(rndPos.x, 0, rndPos.y);

			_damager.ApplyDamage(explosionPoint, _config);
			_renderer.material = _config.ExplodedMaterial;
			_impulseSource.GenerateImpulse();
		}
	}
}