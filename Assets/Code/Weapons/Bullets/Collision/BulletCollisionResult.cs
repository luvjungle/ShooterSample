using UnityEngine;

namespace Code.Weapons.Bullets.Collision
{
	public struct BulletCollisionResult
	{
		public Collider Collider;
		public Vector3 Point;
		public Vector3 Normal;
	}
}