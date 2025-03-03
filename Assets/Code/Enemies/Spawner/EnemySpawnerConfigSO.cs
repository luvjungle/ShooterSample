using UnityEngine;

namespace Code.Enemies.Spawner
{
	[CreateAssetMenu(fileName = "EnemySpawnerConfig", menuName = "Configs/Enemy Spawner")]
	public class EnemySpawnerConfigSO : ScriptableObject
	{
		[SerializeField] private EnemyController _prefab;
		[SerializeField] private float _spawnInterval;
		[SerializeField] private AnimationCurve _healthCurve;
		[SerializeField] private AnimationCurve _enemyAmountCurve;
		[SerializeField] private AnimationCurve _damageCurve;
		
		public EnemyController Prefab => _prefab;
		public float SpawnInterval => _spawnInterval;
		public AnimationCurve HealthCurve => _healthCurve;
		public AnimationCurve EnemyAmountCurve => _enemyAmountCurve;
		public AnimationCurve DamageCurve => _damageCurve;
	}
}