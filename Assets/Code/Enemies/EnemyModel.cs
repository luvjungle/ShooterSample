using System;
using Code.Health;
using Code.Movement.AgentMovement;
using Code.Weapons.AnimationEventAttack;
using Code.Weapons.Targeting;
using Code.Weapons.Targeting.MeleeWithTrigger;
using UnityEngine;
using UnityEngine.AI;

namespace Code.Enemies
{
	public class EnemyModel : IMeleeTriggerModel, IAgentMovementModel, IAnimationEventAttackModel, IHealthModel
	{
		public float RotateAttackLerp => _config.RotateAttackLerp;
		public LayerMask EnemyLayer => _config.EnemyLayer;
		public IDamageable Target { get; set; }
		
		public Vector3? MoveTarget { get; set; }
		public float StopOffset => _config.StopOffset;
		public float PathUpdateDelay => _config.PathUpdateDelay;
		
		public event Action OnAttack;
		public float Damage { get; private set; }
		
		public float Health { get; set; }
		public float MaxHealth { get; private set; }
		public Action OnDeath { get; set; }

		private readonly EnemyConfigSO _config;

		public EnemyModel(EnemyConfigSO config)
		{
			_config = config;
		}

		public void SetDefault(float health, float damage, NavMeshAgent agent)
		{
			agent.speed = _config.MoveSpeed;
			agent.angularSpeed = _config.RotateSpeed;
			Health = health;
			MaxHealth = health;
			Damage = damage;
			Target = null;
			MoveTarget = null;
		}
		
		public void OnAttackInvoke() => OnAttack?.Invoke();
	}
}