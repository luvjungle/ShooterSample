using System;
using Code.Animation;
using Code.Health;
using Code.Movement.CharacterControllerInput;
using Code.Player.Animation;
using Code.Weapons;
using Code.Weapons.Targeting;
using Code.Weapons.Targeting.Weapon;
using UnityEngine;

namespace Code.Player
{
	public class PlayerModel : ICharacterMovementModel, ILookMoveAnimationModel, IWeaponTargetingModel, IHealthModel, IHandIkModel
	{
		public float MaxSpeed => _config.MaxSpeed;
		public float NormalizedSpeed { get; set; }
		public Vector3 MoveDirection { get; set; }
		public Vector3? LookDirection => Target?.Transform.position;
		public float RotateLerp => _config.RotateLerp;
		public float PushRbForce => _config.PushRbForce;
		public event Action<ControllerColliderHit> OnControllerHit;
		
		public float SpeedChangeLerp => _config.AnimationSettings.SpeedChangeLerp;
		
		public Transform Shooter { get; }
		public Transform TargetingRaycastPoint { get; }
		public IDamageable Target { get; set; }
		public IWeaponModel Weapon { get; set; }

		public float Health { get; set; }
		public float MaxHealth => _config.StartHealth;
		public Action OnDeath { get; set; }

		public float ConstraintActivateSpeed => _config.AnimationSettings.HandIkActivateSpeed;
		public bool IkActive { get; set; }

		private readonly PlayerConfigSO _config;

		public PlayerModel(PlayerConfigSO config, PlayerView view)
		{
			_config = config;
			TargetingRaycastPoint = view.TargetingRaycastPoint;
			Shooter = view.transform;
			Health = config.StartHealth;
		}

		public void OnControllerHitInvoke(ControllerColliderHit hit) => OnControllerHit?.Invoke(hit);
	}
}