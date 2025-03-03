using Code.Weapons.Targeting;
using UnityEngine;

namespace Code.Utils
{
	public static class ColliderExtensions
	{
		public static bool TryGetDamageable(this Collider collider, out IDamageable damageable)
		{
			return collider.attachedRigidbody?.TryGetComponent(out damageable) ??
			       collider.TryGetComponent(out damageable);
		}
	}
}