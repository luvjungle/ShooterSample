using Code.Weapons.Bullets.Collision;

namespace Code.Weapons.Bullets.Damagers
{
	public interface IBulletDamager
	{
		void ApplyDamage(BulletCollisionResult collisionResult, IGenericBulletModel model);
	}
}