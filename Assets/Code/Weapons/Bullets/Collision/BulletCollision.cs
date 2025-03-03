using UnityEngine;

namespace Code.Weapons.Bullets.Collision
{
	public class BulletCollision
	{
		private static readonly Collider[] _hitColliders = new Collider[1];

		private readonly Transform _transform;

		private IGenericBulletModel _model;

		public BulletCollision(Transform transform)
		{
			_transform = transform;
		}

		public void SetDefault(IGenericBulletModel model) => _model = model;

		public BulletCollisionResult? CheckOverlap()
		{
			var colliders = Physics.OverlapSphereNonAlloc(_transform.position,
				Mathf.Max(0.05f, _model.Radius), _hitColliders, _model.HitLayerMask.value,
				QueryTriggerInteraction.Ignore);

			if (colliders == 0)
				return null;

			return new BulletCollisionResult()
				{ Collider = _hitColliders[0], Point = _transform.position, Normal = -_transform.forward };
		}

		public BulletCollisionResult? CheckHit(Vector3 translation)
		{
			bool hit;
			RaycastHit hitInfo;
			
			if (_model.Radius > 0.05f)
			{
				hit = Physics.SphereCast(_transform.position, _model.Radius, translation.normalized,
					out hitInfo, translation.magnitude, _model.HitLayerMask.value, QueryTriggerInteraction.Ignore);
			}
			else
			{
				hit = Physics.Raycast(_transform.position, translation.normalized, out hitInfo, translation.magnitude,
					_model.HitLayerMask.value, QueryTriggerInteraction.Ignore);
			}

			if (!hit)
				return null;

			return new BulletCollisionResult()
				{ Collider = hitInfo.collider, Point = hitInfo.point, Normal = hitInfo.normal };
		}
	}
}