using Code.Cameras;
using Code.Enemies;
using Code.Enemies.Spawner;
using Code.InputHandling;
using Code.Player;
using Code.UI.Restart;
using Code.UI.WeaponSelect;
using Code.Utils;
using Code.Utils.PlayerLoop;
using Code.Weapons.Bullets;
using Code.Weapons.Bullets.Damagers;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Code.Bootstrap.Game
{
	public class GameScope : LifetimeScope
	{
		[Header("Player")]
		[SerializeField] private PlayerView _playerPrefab;
		[SerializeField] private Transform _playerSpawnPoint;
		[SerializeField] private PlayerConfigSO _playerConfigSO;

		[Header("Enemies")]
		[SerializeField] private Transform[] _enemySpawnPoints;
		[SerializeField] private EnemySpawnerConfigSO _enemySpawnerConfig;

		[Header("Systems")]
		[SerializeField] private Joystick _virtualJoystick;
		[SerializeField] private CameraManager _cameraManager;

		[Header("UI")]
		[SerializeField] private WeaponSelectView _weaponSelectView;
		[SerializeField] private WeaponSelectConfigSO _weaponSelectConfig;
		[SerializeField] private RestartUiView _restartUiView;
		[SerializeField] private RestartUiConfigSO _restartUiConfig;

		protected override void Configure(IContainerBuilder builder)
		{
			builder.RegisterComponent(_cameraManager);

			builder.Register<PlayerController>(Lifetime.Singleton).WithParameter(_playerConfigSO);

			builder.Register<EnemyList>(Lifetime.Singleton);
			builder.Register<EnemySpawner>(Lifetime.Singleton).WithParameter(_enemySpawnPoints)
				.WithParameter(_enemySpawnerConfig);

			builder.Register<BulletFactory>(Lifetime.Singleton);
			builder.Register<BulletDamagerFactory>(Lifetime.Singleton);

			builder.Register<Pool<ParticleSystem>>(Lifetime.Singleton);

			builder.Register<WeaponSelectController>(Lifetime.Singleton).WithParameter(_weaponSelectView)
				.WithParameter(new WeaponSelectModel(_weaponSelectConfig));

			builder.Register<RestartUiController>(Lifetime.Singleton).WithParameter(_restartUiView)
				.WithParameter(_restartUiConfig);

			builder.Register<VirtualJoystickInput>(Lifetime.Singleton).As<IInputHandler>()
				.WithParameter(_virtualJoystick);

			builder.RegisterComponentOnNewGameObject<LoopUpdater>(Lifetime.Singleton);

			builder.RegisterEntryPoint<GameEntryPoint>().WithParameter(_playerPrefab).WithParameter(_playerSpawnPoint);
		}
	}
}