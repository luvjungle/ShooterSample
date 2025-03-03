using Code.Weapons.Bullets.Damagers;
using UnityEngine;

namespace Code.Props
{
	[CreateAssetMenu(fileName = "BarrelConfig", menuName = "Configs/Props/Barrel Config")]
	public class BarrelConfigSO: ScriptableObject, IAreaDamagerModel
	{
		[SerializeField] private float _health;
		[SerializeField] private float _damage;
		[SerializeField] private float _damageRadius;
		[SerializeField] private float _explosionForce;
		[SerializeField] private LayerMask _hitLayerMask;
		[SerializeField] private ParticleSystem _fireVFX;
		[SerializeField] private ParticleSystem _explodeVFX;
		[SerializeField] private Material _explodedMaterial;
		
		public float Health => _health;
		
		public float AreaDamage => _damage;
		public float DamageRadius => _damageRadius;
		public float ExplosionForce => _explosionForce;
		public LayerMask HitLayerMask => _hitLayerMask;

		public ParticleSystem FireVFX => _fireVFX;
		public ParticleSystem ExplodeVFX => _explodeVFX;
		public Material ExplodedMaterial => _explodedMaterial;
	}
}