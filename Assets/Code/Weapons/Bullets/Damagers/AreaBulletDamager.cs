using System.Collections.Generic;
using Code.Utils;
using Code.Weapons.Bullets.Collision;
using Code.Weapons.Targeting;
using UnityEngine;

namespace Code.Weapons.Bullets.Damagers
{
	public class AreaBulletDamager : IBulletDamager
	{
		private readonly Collider[] _overlapColliders = new Collider[32];
		private readonly HashSet<Rigidbody> _rigidbodies = new(32);
		private readonly HashSet<IDamageable> _damageables = new(32);
		
		public void ApplyDamage(BulletCollisionResult collisionResult, IGenericBulletModel model)
		{
			ApplyDamage(collisionResult.Point, model);
		}

		public void ApplyDamage(Vector3 point, IAreaDamagerModel model)
		{
			_rigidbodies.Clear();
			_damageables.Clear();
			
			int overlaps = Physics.OverlapSphereNonAlloc(point, model.DamageRadius, _overlapColliders,
				model.HitLayerMask.value, QueryTriggerInteraction.Ignore);

			for (int i = 0; i < overlaps; i++)
			{
				TryAddRigidbody(_overlapColliders[i]);

				if (_overlapColliders[i].TryGetDamageable(out IDamageable damageable))
					_damageables.Add(damageable);
			}

			foreach (var damageable in _damageables)
				damageable.TakeDamage(model.AreaDamage);
			
			ApplyForce(model, point);
		}

		private void TryAddRigidbody(Collider collider)
		{
			var rb = collider.attachedRigidbody;
			if (rb && !rb.isKinematic)
				_rigidbodies.Add(collider.attachedRigidbody);
		}

		private void ApplyForce(IAreaDamagerModel model, Vector3 point)
		{
			foreach (var rb in _rigidbodies)
			{
				var distance = Vector3.Distance(rb.position, point);
				var forceMod = Mathf.Max(0, model.DamageRadius - distance) / model.DamageRadius;
				rb.AddExplosionForce(model.ExplosionForce * forceMod, point, model.DamageRadius, 1f, ForceMode.Impulse);
			}
		}
	}
}