using System;
using Code.Enemies.Animation;
using Code.StateMachine;
using Code.Weapons.AnimationEventAttack;
using Code.Weapons.Targeting.MeleeWithTrigger;
using UnityEngine;
using UnityEngine.AI;

namespace Code.Enemies.StateMachine
{
	public class MeleeAttackState : IState
	{
		private readonly NavMeshAgent _agent;
		private readonly EnemyAnimation _animation;
		private readonly IMeleeTriggerModel _model;
		private readonly IAnimationEventAttackModel _attackModel;
		private readonly Action _onAttack;

		public bool CanExit => true;

		public MeleeAttackState(NavMeshAgent agent, EnemyAnimation animation, IMeleeTriggerModel model, IAnimationEventAttackModel attackModel)
		{
			_agent = agent;
			_animation = animation;
			_model = model;
			_attackModel = attackModel;
			_onAttack = OnAttack;
		}

		public void Enter()
		{
			_agent.ResetPath();
			_agent.isStopped = true;
			_agent.updateRotation = false;
			_animation.SetAttack(true);

			_attackModel.OnAttack += _onAttack;
		}

		public void Update(float deltaTime)
		{
			var dir = _model.Target.Transform.position - _agent.transform.position;
			var planarDir = Vector3.ProjectOnPlane(dir, Vector3.up);
			var targetRot = Quaternion.LookRotation(planarDir);
			_agent.transform.rotation = Quaternion.Slerp(_agent.transform.rotation, targetRot, _model.RotateAttackLerp * deltaTime);
		}

		public void Exit()
		{
			_agent.isStopped = false;
			_agent.updateRotation = true;
			_animation.SetAttack(false);
			
			_attackModel.OnAttack -= _onAttack;
		}

		private void OnAttack()
		{
			_model.Target.TakeDamage(_attackModel.Damage);
		}
	}
}