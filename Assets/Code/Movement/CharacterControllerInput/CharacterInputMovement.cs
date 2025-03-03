using Code.InputHandling;
using UnityEngine;

namespace Code.Movement.CharacterControllerInput
{
	public class CharacterInputMovement
	{
		private readonly IInputHandler _inputHandler;
		private readonly ICharacterMovementModel _model;
		private readonly Transform _camera;
		private readonly CharacterController _characterController;

		private Transform _characterTransform => _characterController.transform;

		public CharacterInputMovement(IInputHandler inputHandler, ICharacterMovementModel model, Transform camera,
			CharacterController characterController)
		{
			_inputHandler = inputHandler;
			_model = model;
			_camera = camera;
			_characterController = characterController;
		}

		public void Move(float deltaTime)
		{
			var input = _inputHandler.GetMovementInput();
			_model.NormalizedSpeed = input.magnitude;
			Vector3 moveVector = GetMoveVector(input);

			_characterController.SimpleMove(moveVector);
			_model.MoveDirection = moveVector.normalized;

			Rotate(moveVector, deltaTime);
		}

		private Vector3 GetMoveVector(Vector2 input)
		{
			if (input == Vector2.zero)
				return Vector3.zero;

			var planarCamFwd = Vector3.ProjectOnPlane(_camera.forward, Vector3.up).normalized;
			var dir = planarCamFwd * input.y + _camera.transform.right * input.x;
			return dir * _model.MaxSpeed;
		}

		private void Rotate(Vector3 moveVector, float deltaTime)
		{
			var rotationDir = GetLookAt(moveVector);
			if (rotationDir != Vector3.zero)
			{
				var targetRotation = Quaternion.LookRotation(rotationDir);
				_characterTransform.rotation =
					Quaternion.Slerp(_characterTransform.rotation, targetRotation, _model.RotateLerp * deltaTime);
			}
		}

		private Vector3 GetLookAt(Vector3 moveVector)
		{
			if (!_model.LookDirection.HasValue)
				return moveVector.normalized;

			Vector3 dirToTarget = _model.LookDirection.Value - _characterTransform.position;
			Vector3 planarDir = Vector3.ProjectOnPlane(dirToTarget, Vector3.up);
			return planarDir.normalized;
		}
	}
}