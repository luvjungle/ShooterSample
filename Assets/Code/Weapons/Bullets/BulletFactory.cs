using Code.Utils;
using UnityEngine;
using VContainer;

namespace Code.Weapons.Bullets
{
	public class BulletFactory
	{
		private readonly Pool<BulletController> _bulletPool;

		public BulletFactory(IObjectResolver resolver)
		{
			_bulletPool = new Pool<BulletController>(resolver);
		}

		public BulletController CreateBullet(BulletController prefab, Vector3 spawnPosition, Vector3 direction)
		{
			var bullet = _bulletPool.TakeFromPool(prefab, spawnPosition, Quaternion.LookRotation(direction));
			return bullet;
		}
	}
}