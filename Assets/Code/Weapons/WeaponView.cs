using UnityEngine;

namespace Code.Weapons
{
	public class WeaponView : MonoBehaviour
	{
		[SerializeField] private Transform _shootPoint;
		[SerializeField] private ParticleSystem _muzzleFlash;

		public Transform ShootPoint => _shootPoint;

		public void OnShoot()
		{
			if (_muzzleFlash != null)
				_muzzleFlash.Play(true);
		}
	}
}