using UnityEngine;

namespace Code.Weapons.Bullets
{
	public class BulletView : MonoBehaviour
	{
		[SerializeField] private Rigidbody _rigidbody;
		[SerializeField] private TrailRenderer _trailRenderer;
		
		public Rigidbody Rigidbody => _rigidbody;

		public void OnShoot()
		{
			if (_trailRenderer != null)
				_trailRenderer.Clear();
		}
	}
}