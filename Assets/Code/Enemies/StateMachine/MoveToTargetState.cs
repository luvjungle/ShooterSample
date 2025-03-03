using Code.Enemies.Animation;
using Code.Movement.AgentMovement;
using Code.StateMachine;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.AI;

namespace Code.Enemies.StateMachine
{
	public class MoveToTargetState : IState
	{
		private readonly IAgentMovementModel _model;
		private readonly EnemyAnimation _animation;
		private readonly NavMeshAgent _agent;

		private float _timer;
		private bool _onLinkMoving;
		
		public bool CanExit => !_onLinkMoving;

		public MoveToTargetState(IAgentMovementModel model, EnemyAnimation animation, NavMeshAgent agent)
		{
			_model = model;
			_animation = animation;
			_agent = agent;
		}

		public void Enter()
		{
			_timer = 0;
		}

		public void Update(float deltaTime)
		{
			_animation.SetRun(_agent.velocity.magnitude > 0.1f);
			ProcessLink();

			_timer -= deltaTime;
			if (_timer > 0)
				return;

			_timer = _model.PathUpdateDelay;
			TrySetDestination();
		}

		private void TrySetDestination()
		{
			if (_agent.pathPending || _onLinkMoving)
				return;

			if (_model.MoveTarget.HasValue)
				_agent.SetDestination(_model.MoveTarget.Value);
		}

		private void ProcessLink()
		{
			if (_onLinkMoving || !_agent.isOnOffMeshLink)
				return;

			ProcessLinkAsync().Forget();
		}

		private async UniTaskVoid ProcessLinkAsync()
		{
			_onLinkMoving = true;
			
			var data = _agent.currentOffMeshLinkData;
			var distance = Vector3.Distance(data.startPos, data.endPos);
			var height = data.endPos.y > data.startPos.y ? 0.15f : data.startPos.y - data.endPos.y + 0.15f;

			await _agent.transform.DOJump(data.endPos, height, 1, distance / _agent.speed).SetEase(Ease.Linear);
			
			_agent.CompleteOffMeshLink();
			_onLinkMoving = false;
		}

		public void Exit() { }
	}
}