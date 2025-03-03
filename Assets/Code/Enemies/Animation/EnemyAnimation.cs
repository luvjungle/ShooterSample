using UnityEngine;

namespace Code.Enemies.Animation
{
	public class EnemyAnimation
	{
		private readonly Animator _animator;
		
		private readonly int _move = Animator.StringToHash("Move");
		private readonly int _attack = Animator.StringToHash("Attack");
		private readonly int _death = Animator.StringToHash("Death");
		private readonly int _forceIdle = Animator.StringToHash("ForceIdle");

		public EnemyAnimation(Animator animator)
		{
			_animator = animator;
		}

		public void SetDefault() => _animator.SetTrigger(_forceIdle);

		public void SetRun(bool run) => _animator.SetBool(_move, run);
		
		public void SetAttack(bool attack) => _animator.SetBool(_attack, attack);

		public void Death() => _animator.SetTrigger(_death);
	}
}