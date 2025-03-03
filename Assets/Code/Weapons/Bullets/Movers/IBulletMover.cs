using UnityEngine;

namespace Code.Weapons.Bullets.Movers
{
	public interface IBulletMover
	{
		void SetDefault(IGenericBulletModel model);
		void Move(float deltaTime);
		Vector3 GetFrameTranslation();
	}
}