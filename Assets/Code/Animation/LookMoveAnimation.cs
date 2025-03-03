using UnityEngine;

namespace Code.Animation
{
	public class LookMoveAnimation
	{
		private readonly Animator _animator;
		private readonly Transform _animationTarget;
		private readonly ILookMoveAnimationModel _model;
		
		private readonly int _speed = Animator.StringToHash("Speed");
		private readonly int _fwdDot = Animator.StringToHash("FwdDot");
		private readonly int _rightDot = Animator.StringToHash("RightDot");

		public LookMoveAnimation(Animator animator, Transform animationTarget, ILookMoveAnimationModel model)
		{
			_animator = animator;
			_animationTarget = animationTarget;
			_model = model;
		}

		public void Update(float deltaTime)
		{
			_animator.SetFloat(_speed, _model.NormalizedSpeed);
			SetDot(_animationTarget.forward, _fwdDot, deltaTime);
			SetDot(_animationTarget.right, _rightDot, deltaTime);
		}

		private void SetDot(Vector3 lhs, int animHash, float deltaTime)
		{
			float dot = Vector3.Dot(lhs, _model.MoveDirection);
			var targetDot = Mathf.Lerp(_animator.GetFloat(animHash), dot,
				_model.SpeedChangeLerp * deltaTime);
			
			_animator.SetFloat(animHash, targetDot);
		}
	}
}