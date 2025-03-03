using Code.Player.Animation;
using Code.Weapons.Bullets;
using Code.Weapons.Targeting.Weapon;
using UnityEngine;

namespace Code.Weapons
{
	public class WeaponController
	{
		private readonly IWeaponTargetingModel _model;
		private readonly BulletFactory _bulletFactory;
		private readonly Transform _weaponParent;
		private readonly IHandIkModel _handIkModel;

		private float _shootCooldown;
		private IGenericBulletModel _bulletModel;
		private WeaponView _currentView;

		public WeaponController(IWeaponTargetingModel model, BulletFactory bulletFactory,
			Transform weaponParent, IHandIkModel handIkModel)
		{
			_model = model;
			_bulletFactory = bulletFactory;
			_weaponParent = weaponParent;
			_handIkModel = handIkModel;
		}

		public void SetWeapon(WeaponConfigSO config)
		{
			if (_currentView != null)
				GameObject.Destroy(_currentView.gameObject);

			var weaponModel = new WeaponModel(config);
			_bulletModel = weaponModel;
			_model.Weapon = weaponModel;
			_currentView = GameObject.Instantiate(_model.Weapon.Prefab, _weaponParent);
		}

		public void Update(float deltaTime)
		{
			_shootCooldown -= deltaTime;

			if (!CanShoot())
				return;

			_shootCooldown = _model.Weapon.ShootCooldown;

			Shoot();
		}

		private bool CanShoot()
		{
			return _shootCooldown <= 0 &&
			       _model.Target != null &&
			       _model.Weapon != null &&
			       (_handIkModel == null || _handIkModel.IkActive);
		}

		private void Shoot()
		{
			_bulletFactory.CreateBullet(_bulletModel.BulletPrefab, _currentView.ShootPoint.position,
				GetBulletDirection()).Shoot(_bulletModel);

			_currentView.OnShoot();
		}

		private Vector3 GetBulletDirection()
		{
			var random = Random.insideUnitCircle;
			random = new Vector2(random.x * _model.Weapon.Spread.x, random.y * _model.Weapon.Spread.y);
			
			var direction = _model.Target.ShootPoint.position - _currentView.ShootPoint.position;
			direction = Quaternion.AngleAxis(random.x, Vector3.up) *
			            Quaternion.AngleAxis(random.y, Vector3.right) * direction;

			return direction.normalized;
		}
	}
}