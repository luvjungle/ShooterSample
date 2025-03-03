using Code.Enemies.Animation;
using Code.StateMachine;
using UnityEngine.AI;

namespace Code.Enemies.StateMachine
{
	public class IdleState : IState
	{
		private readonly NavMeshAgent _agent;
		private readonly EnemyAnimation _animation;
		
		public bool CanExit => true;

		public IdleState(NavMeshAgent agent, EnemyAnimation animation)
		{
			_agent = agent;
			_animation = animation;
		}

		public void Enter()
		{
			_agent.ResetPath();
			_animation.SetRun(false);
			_animation.SetAttack(false);
		}
		
		public void Update(float deltaTime) { }
		
		public void Exit() { }
	}
}