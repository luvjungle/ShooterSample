using System;
using Code.Cameras;
using Code.Enemies;
using Code.Health;
using Code.InputHandling;
using Code.Movement.CharacterControllerInput;
using Code.Player.Animation;
using Code.UI.Restart;
using Code.Utils.PlayerLoop;
using Code.Utils.PlayerLoop.Interfaces;
using Code.Weapons;
using Code.Weapons.Bullets;
using Code.Weapons.Targeting;
using Code.Weapons.Targeting.Weapon;
using DG.Tweening;

namespace Code.Player
{
	public class PlayerController : IDisposable, IUpdate
	{
		private readonly IInputHandler _inputHandler;
		private readonly CameraManager _cameraManager;
		private readonly LoopUpdater _updater;
		private readonly EnemyList _enemyList;
		private readonly BulletFactory _bulletFactory;
		private readonly PlayerConfigSO _config;
		private readonly RestartUiController _restartUi;

		private PlayerModel _model;
		private PlayerAnimation _animation;
		private CharacterInputMovement _movement;
		private PlayerView _view;
		private CharacterControllerCollision _collision;
		private WeaponTargeting _targeting;
		private HealthController _health;
		private WeaponController _weapon;
		private HandIkController _handIkController;

		public IDamageable Damageable => _view;

		public PlayerController(IInputHandler inputHandler, CameraManager cameraManager,
			LoopUpdater updater, EnemyList enemyList, BulletFactory bulletFactory, PlayerConfigSO config, RestartUiController restartUi)
		{
			_inputHandler = inputHandler;
			_cameraManager = cameraManager;
			_updater = updater;
			_enemyList = enemyList;
			_bulletFactory = bulletFactory;
			_config = config;
			_restartUi = restartUi;
		}

		public void Init(PlayerView view)
		{
			_view = view;
			_model = new PlayerModel(_config, _view);
			_view.Init(_model);

			_movement = new CharacterInputMovement(_inputHandler, _model, _cameraManager.MainCamera.transform, _view.CharacterController);
			_animation = new PlayerAnimation(_view.Animator, _view.transform, _model);
			_collision = new CharacterControllerCollision(_model, "Ground");
			_targeting = new WeaponTargeting(_enemyList.Enemies, _model);
			_health = new HealthController(_model, _view.HealthView, _updater, _cameraManager.MainCamera.transform);
			_weapon = new WeaponController(_model, _bulletFactory, _view.WeaponParent, _model);
			_handIkController = new HandIkController(_view.HandIkConstraint, _view.HandIkTarget, _model, _model);

			_health.SetDefault();
			_updater.Add(this);
			_view.OnDamage += _health.TakeDamage;
			_view.OnControllerHit += _model.OnControllerHitInvoke;
			_model.OnDeath += OnDeath;
		}

		public void Tick(float deltaTime)
		{
			_movement.Move(deltaTime);
			_animation.Update(deltaTime);
			_targeting.Update();
			_handIkController.Update(deltaTime);
			_weapon.Update(deltaTime);
		}

		public void SetWeapon(WeaponConfigSO weapon) => _weapon.SetWeapon(weapon);

		private void OnDeath()
		{
			_inputHandler.Enable(false);
			_updater.Remove(this);
			_handIkController.SetDefault();
			_animation.OnDeath();
			_view.CharacterController.enabled = false;
			_view.OnControllerHit -= _model.OnControllerHitInvoke;
			_restartUi.OpenWithDelay();
		}

		public void Dispose()
		{
			_updater.Remove(this);
			_collision.Dispose();
			_health.Dispose();
			_view.OnDamage -= _health.TakeDamage;
			_view.OnControllerHit -= _model.OnControllerHitInvoke;
		}
	}
}