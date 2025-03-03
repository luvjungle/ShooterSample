using Code.Weapons.Targeting;
using UnityEngine;

namespace Code.Movement.AgentMovement
{
	public class AgentToPlayerMovement
	{
		private readonly IDamageable _player;
		private readonly IAgentMovementModel _model;
		private readonly Transform _agent;

		public AgentToPlayerMovement(IDamageable player, IAgentMovementModel model, Transform agent)
		{
			_player = player;
			_model = model;
			_agent = agent;
		}

		public void Update()
		{
			if (_player.Dead)
			{
				_model.MoveTarget = null;
			}
			else
			{
				var dir = (_player.Transform.position - _agent.position).normalized;
				_model.MoveTarget = _player.Transform.transform.position - dir * _model.StopOffset;
			}
		}
	}
}