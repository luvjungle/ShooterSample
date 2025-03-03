using UnityEngine;

namespace Code.Movement.AgentMovement
{
	public interface IAgentMovementModel
	{
		Vector3? MoveTarget { get; set; }
		float StopOffset { get; }
		float PathUpdateDelay { get; }
	}
}