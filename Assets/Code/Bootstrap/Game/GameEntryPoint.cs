using System.Threading;
using Code.Bootstrap.Loading;
using Code.Cameras;
using Code.Enemies.Spawner;
using Code.InputHandling;
using Code.Player;
using Code.UI.WeaponSelect;
using Cysharp.Threading.Tasks;
using UnityEngine;
using VContainer.Unity;

namespace Code.Bootstrap.Game
{
	public class GameEntryPoint : IAsyncStartable
	{
		private readonly PlayerView _playerPrefab;
		private readonly Transform _playerSpawnPoint;
		private readonly CameraManager _cameraManager;
		private readonly PlayerController _playerController;
		private readonly LoadingView _loadingView;
		private readonly EnemySpawner _enemySpawner;
		private readonly WeaponSelectController _weaponSelectController;
		private readonly IInputHandler _inputHandler;

		public GameEntryPoint(PlayerView playerPrefab, Transform playerSpawnPoint, CameraManager cameraManager,
			PlayerController playerController, LoadingView loadingView, EnemySpawner enemySpawner,
			WeaponSelectController weaponSelectController, IInputHandler inputHandler)
		{
			_playerPrefab = playerPrefab;
			_playerSpawnPoint = playerSpawnPoint;
			_cameraManager = cameraManager;
			_playerController = playerController;
			_loadingView = loadingView;
			_enemySpawner = enemySpawner;
			_weaponSelectController = weaponSelectController;
			_inputHandler = inputHandler;
		}
		
		public async UniTask StartAsync(CancellationToken ct)
		{
			_inputHandler.Enable(false);
			SpawnPlayer();
			
			bool fadedOut = false;
			_loadingView.FadeOut(() => fadedOut = true);
			await UniTask.WaitUntil(() => fadedOut, cancellationToken: ct);

			_weaponSelectController.Open();
			await UniTask.WaitUntil(() => !_weaponSelectController.Opened, cancellationToken: ct);
			
			_enemySpawner.Init();
			_inputHandler.Enable(true);
		}

		private void SpawnPlayer()
		{
			PlayerView playerView =
				GameObject.Instantiate(_playerPrefab, _playerSpawnPoint.position, _playerSpawnPoint.rotation);

			_playerController.Init(playerView);
			_cameraManager.SetPlayerFollow(playerView.transform);
		}
	}
}