using UnityEngine;

namespace Code.Weapons.Targeting.MeleeWithTrigger
{
	public interface IMeleeTriggerModel
	{
		float RotateAttackLerp { get; }
		LayerMask EnemyLayer { get; }
		IDamageable Target { get; set; }
	}
}