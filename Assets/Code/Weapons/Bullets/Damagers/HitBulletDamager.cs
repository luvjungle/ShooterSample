using Code.Utils;
using Code.Weapons.Bullets.Collision;
using Code.Weapons.Targeting;
using UnityEngine;

namespace Code.Weapons.Bullets.Damagers
{
	public class HitBulletDamager : IBulletDamager
	{
		public void ApplyDamage(BulletCollisionResult collisionResult, IGenericBulletModel model)
		{
			ApplyDamage(collisionResult.Collider, model);
		}
		
		public void ApplyDamage(Collider collider, IHitDamagerModel model)
		{
			if (!collider.TryGetDamageable(out IDamageable damageable))
				return;
			
			damageable.TakeDamage(model.HitDamage);
		}
	}
}