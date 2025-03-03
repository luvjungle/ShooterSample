using Code.Cameras;
using Code.Enemies.Animation;
using Code.Enemies.StateMachine;
using Code.Health;
using Code.Movement.AgentMovement;
using Code.Player;
using Code.Utils.PlayerLoop;
using Code.Utils.PlayerLoop.Interfaces;
using Code.Weapons.Targeting;
using Code.Weapons.Targeting.MeleeWithTrigger;
using DG.Tweening;
using UnityEngine;
using VContainer;

namespace Code.Enemies
{
	public class EnemyController : MonoBehaviour, IUpdate, IDamageable
	{
		[SerializeField] private EnemyView _view;
		[SerializeField] private EnemyConfigSO _config;

		private EnemyList _list;
		private PlayerController _player;
		private LoopUpdater _updater;
		private EnemyModel _model;
		private CameraManager _camera;
		private EnemyAnimation _animation;
		private EnemyStateMachine _stateMachine;
		private MeleeTriggerTargeting _targeting;
		private AgentToPlayerMovement _movement;
		private HealthController _health;

		private bool _init;

		Transform IDamageable.Transform => _view.transform;
		Transform IDamageable.ShootPoint => _view.ShootPoint;
		bool IDamageable.Dead => _model.Health <= 0;
		void IDamageable.TakeDamage(float damage) => _health.TakeDamage(damage);

		[Inject]
		public void Construct(EnemyList list, PlayerController player, LoopUpdater updater, CameraManager cameraManager)
		{
			_list = list;
			_player = player;
			_updater = updater;
			_camera = cameraManager;
		}

		public void Init()
		{
			if (_init) return;
			_init = true;

			_model = new EnemyModel(_config);
			_animation = new EnemyAnimation(_view.Animator);
			_targeting = new MeleeTriggerTargeting(_view.TriggerView, _model);
			_movement = new AgentToPlayerMovement(_player.Damageable, _model, _view.Agent.transform);
			_health = new HealthController(_model, _view.HealthView, _updater, _camera.MainCamera.transform);

			_stateMachine = new EnemyStateMachine(_model);
			_stateMachine.RegisterState(new MoveToTargetState(_model, _animation, _view.Agent));
			_stateMachine.RegisterState(new MeleeAttackState(_view.Agent, _animation, _model, _model));
			_stateMachine.RegisterState(new IdleState(_view.Agent, _animation));
			_stateMachine.RegisterState(new DeadState(_view.Agent, _view.Collider, _animation, OnDeath));
		}

		public void SetDefault(float health, float damage)
		{
			gameObject.SetActive(true);
			_model.SetDefault(health, damage, _view.Agent);
			_health.SetDefault();
			_animation.SetDefault();
			_stateMachine.ChangeState<IdleState>();

			_list.Add(this);
			_updater.Add(this);
			_view.EnemyAnimationEvents.OnAttack += _model.OnAttackInvoke;
		}

		public void Tick(float deltaTime)
		{
			_movement.Update();
			_targeting.Update();
			_stateMachine.Update(deltaTime);
		}

		private void OnDeath()
		{
			RemoveSubscriptions();
			DOVirtual.DelayedCall(_config.BodyRemoveTime, () => gameObject.SetActive(false));
		}

		private void RemoveSubscriptions()
		{
			_list.Remove(this);
			_updater.Remove(this);
			_view.EnemyAnimationEvents.OnAttack -= _model.OnAttackInvoke;
		}

		private void OnDestroy()
		{
			RemoveSubscriptions();
			_targeting.Dispose();
			_health.Dispose();
		}
	}
}