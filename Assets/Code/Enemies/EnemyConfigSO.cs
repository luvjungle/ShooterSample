using UnityEngine;

namespace Code.Enemies
{
	[CreateAssetMenu(fileName = "EnemyConfig", menuName = "Configs/Enemy Config")]
	public class EnemyConfigSO : ScriptableObject
	{
		[Header("Movement")]
		[SerializeField] private float _moveSpeed;
		[SerializeField] private float _rotateSpeed;
		[SerializeField] private float _stopOffset;
		[SerializeField] private float _pathUpdateDelay;
		
		[Header("Attack")]
		[SerializeField] private float _rotateAttackLerp;
		[SerializeField] private LayerMask _enemyLayer;
		
		[Header("Death")] 
		[SerializeField] private float _bodyRemoveTime;
		
		public float MoveSpeed => _moveSpeed;
		public float RotateSpeed => _rotateSpeed;
		public float RotateAttackLerp => _rotateAttackLerp;
		public float PathUpdateDelay => _pathUpdateDelay;
		public float StopOffset => _stopOffset;
		public LayerMask EnemyLayer => _enemyLayer;
		public float BodyRemoveTime => _bodyRemoveTime;
	}
}