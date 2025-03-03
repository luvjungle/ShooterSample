using Code.Animation;
using UnityEngine;

namespace Code.Player.Animation
{
	public class PlayerAnimation
	{
		private readonly Animator _animator;
		private readonly LookMoveAnimation _lookMove;
		
		private readonly int _death = Animator.StringToHash("Death");

		public PlayerAnimation(Animator animator, Transform animationTarget, ILookMoveAnimationModel model)
		{
			_animator = animator;
			_lookMove = new LookMoveAnimation(animator, animationTarget, model);
		}

		public void Update(float deltaTime)
		{
			_lookMove.Update(deltaTime);
		}

		public void OnDeath()
		{
			_animator.SetTrigger(_death);
		}
	}
}