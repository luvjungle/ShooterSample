using System;
using UnityEngine;

namespace Code.Player
{
	[CreateAssetMenu(fileName = "PlayerConfig", menuName = "Configs/Player Config")]
	public class PlayerConfigSO : ScriptableObject
	{
		[SerializeField] private float _startHealth;
		[SerializeField] private float _maxSpeed;
		[SerializeField] private float _rotateLerp;
		[SerializeField] private float _pushRbForce;
		[SerializeField] private AnimationConfig _animationConfig;

		public float StartHealth => _startHealth;
		public float MaxSpeed => _maxSpeed;
		public float RotateLerp => _rotateLerp;
		public float PushRbForce => _pushRbForce;
		public AnimationConfig AnimationSettings => _animationConfig;

		[Serializable]
		public class AnimationConfig
		{
			public float SpeedChangeLerp;
			public float HandIkActivateSpeed;
		}
	}
}