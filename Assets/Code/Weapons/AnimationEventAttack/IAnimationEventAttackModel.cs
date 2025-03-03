using System;

namespace Code.Weapons.AnimationEventAttack
{
	public interface IAnimationEventAttackModel
	{
		event Action OnAttack;
		float Damage { get; }
	}
}