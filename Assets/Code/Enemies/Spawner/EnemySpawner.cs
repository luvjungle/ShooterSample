using System;
using Code.Utils;
using Code.Utils.PlayerLoop;
using Code.Utils.PlayerLoop.Interfaces;
using UnityEngine;
using VContainer;
using Random = UnityEngine.Random;

namespace Code.Enemies.Spawner
{
	public class EnemySpawner : IDisposable, IUpdate
	{
		private readonly EnemyList _list;
		private readonly Transform[] _spawnPoints;
		private readonly LoopUpdater _updater;
		private readonly IObjectResolver _resolver;
		private readonly SinglePool<EnemyController> _pool;
		private readonly EnemySpawnerConfigSO _config;

		private float _timer;
		private float _gameTime;
		private bool _initialized;

		public EnemySpawner(EnemyList list, LoopUpdater updater, Transform[] spawnPoints, EnemySpawnerConfigSO config,
			IObjectResolver resolver)
		{
			_list = list;
			_updater = updater;
			_spawnPoints = spawnPoints;
			_config = config;
			_pool = new SinglePool<EnemyController>(_config.Prefab, resolver);

			_updater.Add(this);
		}

		public void Init()
		{
			_initialized = true;
		}

		public void Tick(float deltaTime)
		{
			if (!_initialized)
				return;

			_gameTime += deltaTime;

			_timer -= deltaTime;
			if (_timer > 0)
				return;

			_timer = _config.SpawnInterval;

			if (_list.Enemies.Count >= _config.EnemyAmountCurve.Evaluate(_gameTime))
				return;

			var point = _spawnPoints[Random.Range(0, _spawnPoints.Length)];
			SpawnEnemy(point.position, point.rotation);
		}

		private void SpawnEnemy(Vector3 position, Quaternion rotation)
		{
			var controller = _pool.TakeFromPool(position, rotation);
			controller.Init();
			controller.SetDefault(_config.HealthCurve.Evaluate(_gameTime), _config.DamageCurve.Evaluate(_gameTime));
		}

		public void Dispose()
		{
			_updater.Remove(this);
		}
	}
}