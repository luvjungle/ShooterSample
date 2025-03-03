using Code.Weapons.Bullets;
using UnityEngine;

namespace Code.Weapons
{
	[CreateAssetMenu(fileName = "WeaponConfig", menuName = "Configs/Weapon Config")]
	public class WeaponConfigSO : ScriptableObject
	{
		[Header("UI")] 
		[SerializeField] private string _name;
		[SerializeField] private string _description;
		[SerializeField] private Sprite _icon;
		
		[Header("Weapon")]
		[SerializeField] private WeaponView _prefab;
		[SerializeField] private float _shootCooldown;
		[SerializeField] private float _range;
		[SerializeField] private Vector2 _spread;
		
		[Header("Bullet")]
		[SerializeField] private BulletController _bulletPrefab;
		[SerializeField] private float _damage;
		[SerializeField] private float _damageRadius;
		[SerializeField] private float _bulletSpeed;
		[SerializeField] private float _bulletLifeTime;
		[SerializeField] private float _bulletRadius;
		[SerializeField] private float _explosionForce;
		[SerializeField] private ParticleSystem _hitParticleSystem;
		[SerializeField] private LayerMask _hitLayerMask;
		
		public LayerMask HitLayerMask => _hitLayerMask;
		public WeaponView Prefab => _prefab;
		public BulletController BulletPrefab => _bulletPrefab;
		public float Damage => _damage;
		public float DamageRadius => _damageRadius;
		public float BulletSpeed => _bulletSpeed;
		public float BulletLifeTime => _bulletLifeTime;
		public float BulletRadius => _bulletRadius;
		public float ExplosionForce => _explosionForce;
		public float ShootCooldown => _shootCooldown;
		public float Range => _range;
		public Vector2 Spread => _spread;
		public ParticleSystem HitParticleSystem => _hitParticleSystem;
		
		public string Name => _name;
		public string Description => _description;
		public Sprite Icon => _icon;
	}
}