using UnityEngine;

namespace Code.Weapons.Targeting
{
	public interface IDamageable
	{
		Transform Transform { get; }
		Transform ShootPoint { get; }
		bool Dead { get; }
		void TakeDamage(float damage);
	}
}