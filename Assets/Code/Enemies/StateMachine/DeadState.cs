using System;
using Code.Enemies.Animation;
using Code.StateMachine;
using UnityEngine;
using UnityEngine.AI;

namespace Code.Enemies.StateMachine
{
	public class DeadState : IState
	{
		private readonly NavMeshAgent _agent;
		private readonly Collider _collider;
		private readonly EnemyAnimation _animation;
		private readonly Action _onDeath;
		
		public bool CanExit => true;

		public DeadState(NavMeshAgent agent, Collider collider, EnemyAnimation animation, Action onDeath)
		{
			_agent = agent;
			_collider = collider;
			_animation = animation;
			_onDeath = onDeath;
		}

		public void Enter()
		{
			_agent.ResetPath();
			_animation.Death();
			_agent.enabled = false;
			_collider.enabled = false;
			_onDeath?.Invoke();
		}
		
		public void Update(float deltaTime) { }

		public void Exit()
		{
			_agent.enabled = true;
			_collider.enabled = true;
		}
	}
}