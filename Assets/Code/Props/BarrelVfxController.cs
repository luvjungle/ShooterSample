using Code.Utils;
using Code.Utils.PlayerLoop;
using Code.Utils.PlayerLoop.Interfaces;
using UnityEngine;

namespace Code.Props
{
	public class BarrelVfxController : ILateUpdate
	{
		private readonly BarrelConfigSO _config;
		private readonly LoopUpdater _updater;
		private readonly Transform _firePosition;
		private readonly Pool<ParticleSystem> _particleSystemPool;

		private ParticleSystem _fireVfx;
		private bool _fireStarted;

		public BarrelVfxController(BarrelConfigSO config, LoopUpdater updater, Transform firePosition,
			Pool<ParticleSystem> particleSystemPool)
		{
			_config = config;
			_updater = updater;
			_firePosition = firePosition;
			_particleSystemPool = particleSystemPool;
		}

		public void StartFire()
		{
			if (_fireStarted) return;
			_fireStarted = true;
			
			_fireVfx = _particleSystemPool.TakeFromPool(_config.FireVFX, _firePosition.position,
				Quaternion.LookRotation(Vector3.up));
			_fireVfx.Play(true);
			_updater.Add(this);
		}

		public void OnDeath()
		{
			_fireVfx.Stop(true);
			_updater.Remove(this);
			_particleSystemPool.TakeFromPool(_config.ExplodeVFX, _firePosition.position);
		}

		public void Tick(float deltaTime)
		{
			_fireVfx.transform.SetPositionAndRotation(_firePosition.position,
				Quaternion.LookRotation(Vector3.up));
		}
	}
}