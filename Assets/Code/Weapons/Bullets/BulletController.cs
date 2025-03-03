using Code.Utils;
using Code.Utils.PlayerLoop;
using Code.Utils.PlayerLoop.Interfaces;
using Code.Weapons.Bullets.Collision;
using Code.Weapons.Bullets.Damagers;
using Code.Weapons.Bullets.Movers;
using UnityEngine;
using VContainer;

namespace Code.Weapons.Bullets
{
	public class BulletController : MonoBehaviour, IFixedUpdate
	{
		[SerializeField] private BulletView _view;

		private IBulletMover _mover;
		private IBulletDamager _damager;
		private IGenericBulletModel _model;
		private LoopUpdater _updater;
		private BulletCollision _collision;
		private BulletDamagerFactory _damagerFactory;
		private Pool<ParticleSystem> _particleSystemPool;

		private readonly BulletLifetime _lifeTime = new();

		[Inject]
		public void Construct(LoopUpdater updater, BulletDamagerFactory damagerFactory,
			Pool<ParticleSystem> particleSystemPool)
		{
			_updater = updater;
			_damagerFactory = damagerFactory;
			_particleSystemPool = particleSystemPool;
			_mover = new RbForwardBulletMover(_view.Rigidbody);
			_collision = new BulletCollision(transform);
			_lifeTime.OnTimerFinished += KillBullet;
		}

		public void Shoot(IGenericBulletModel model)
		{
			_model = model;
			_lifeTime.SetDefault(model.LifeTime);
			_mover.SetDefault(model);
			_collision.SetDefault(model);
			_damager = _damagerFactory.Get(model);
			_updater.Add(this);
			_view.OnShoot();

			ProcessCollisionResult(_collision.CheckOverlap());
		}

		private void ProcessCollisionResult(BulletCollisionResult? result)
		{
			if (!result.HasValue)
				return;

			_damager.ApplyDamage(result.Value, _model);
			
			_particleSystemPool.TakeFromPool(_model.HitParticleSystem, result.Value.Point,
				Quaternion.LookRotation(result.Value.Normal));
			
			KillBullet();
		}

		public void Tick(float deltaTime)
		{
			_mover.Move(deltaTime);
			_lifeTime.Update(deltaTime);

			var translation = _mover.GetFrameTranslation();
			ProcessCollisionResult(_collision.CheckHit(translation));
		}

		private void KillBullet()
		{
			_damagerFactory.Return(_damager);
			gameObject.SetActive(false);
			_updater.Remove(this);
		}
	}
}