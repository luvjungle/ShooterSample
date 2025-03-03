using UnityEngine;
using UnityEngine.AI;

namespace Code.Movement.CharacterControllerInput
{
	public class CharacterControllerCollision
	{
		private readonly ICharacterMovementModel _model;
		private readonly string _groundTag;

		public CharacterControllerCollision(ICharacterMovementModel model, string groundTag)
		{
			_model = model;
			_groundTag = groundTag;

			_model.OnControllerHit += OnControllerColliderHit;
		}

		private void OnControllerColliderHit(ControllerColliderHit hit)
		{
			if (hit.collider.CompareTag(_groundTag))
				return;

			// Disabled because NavMeshObstacle on PlayerView moves them.
			// if (TryMoveAgent(hit))
			// 	return;

			TryPushRigidbody(hit);
		}

		private bool TryMoveAgent(ControllerColliderHit hit)
		{
			if (!hit.collider.TryGetComponent(out NavMeshAgent agent))
				return false;

			agent.Move(-hit.normal * hit.moveLength);
			return true;
		}

		private bool TryPushRigidbody(ControllerColliderHit hit)
		{
			if (!hit.rigidbody || hit.rigidbody.isKinematic)
				return false;

			if (hit.moveDirection.y < -0.3f)
				return false;

			Vector3 pushDir = new Vector3(_model.MoveDirection.x, 0.0f, _model.MoveDirection.z);
			hit.rigidbody.AddForce(pushDir * _model.PushRbForce * _model.NormalizedSpeed * Time.deltaTime,
				ForceMode.Impulse);

			return true;
		}

		public void Dispose()
		{
			_model.OnControllerHit -= OnControllerColliderHit;
		}
	}
}