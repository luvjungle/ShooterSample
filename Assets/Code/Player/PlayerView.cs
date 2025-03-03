using System;
using Code.Health;
using Code.Weapons.Targeting;
using UnityEngine;
using UnityEngine.Animations.Rigging;

namespace Code.Player
{
	public class PlayerView : MonoBehaviour, IDamageable
	{
		[SerializeField] private Animator _animator;
		[SerializeField] private CharacterController _characterController;
		[SerializeField] private Transform _shootPoint;
		[SerializeField] private HealthView _healthView;
		[SerializeField] private Transform _weaponParent;
		[SerializeField] private Transform _targetingRaycastPoint;
		[SerializeField] private TwoBoneIKConstraint _handIkConstraint;
		[SerializeField] private Transform _handIkTarget;
		
		public Animator Animator => _animator;
		public CharacterController CharacterController => _characterController;
		public event Action<ControllerColliderHit> OnControllerHit;
		public Transform Transform => transform;
		public Transform ShootPoint => _shootPoint;
		public HealthView HealthView => _healthView;
		public Transform WeaponParent => _weaponParent;
		public Transform TargetingRaycastPoint => _targetingRaycastPoint;
		public TwoBoneIKConstraint HandIkConstraint => _handIkConstraint;
		public Transform HandIkTarget => _handIkTarget;
		
		public bool Dead => _healthModel.Health <= 0;
		public event Action<float> OnDamage;

		private IHealthModel _healthModel;

		public void Init(IHealthModel healthModel)
		{
			_healthModel = healthModel;
			OnDamage = null;
			OnControllerHit = null;
		}

		private void OnControllerColliderHit(ControllerColliderHit hit) => OnControllerHit?.Invoke(hit);
		
		public void TakeDamage(float damage) => OnDamage?.Invoke(damage);
	}
}