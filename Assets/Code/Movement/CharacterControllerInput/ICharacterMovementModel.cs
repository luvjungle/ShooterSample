using System;
using UnityEngine;

namespace Code.Movement.CharacterControllerInput
{
	public interface ICharacterMovementModel
	{
		float MaxSpeed { get; }
		float NormalizedSpeed { get; set; }
		float CurrentSpeed => NormalizedSpeed * MaxSpeed;
		public Vector3 MoveDirection { get; set; }
		public Vector3? LookDirection { get; }
		float RotateLerp { get; }
		float PushRbForce { get; }
		event Action<ControllerColliderHit> OnControllerHit;
	}
}