using Code.Enemies.Animation;
using Code.Health;
using Code.Utils;
using UnityEngine;
using UnityEngine.AI;

namespace Code.Enemies
{
	public class EnemyView : MonoBehaviour
	{
		[SerializeField] private NavMeshAgent _agent;
		[SerializeField] private Animator _animator;
		[SerializeField] private Transform _shootPoint;
		[SerializeField] private TriggerView _triggerView;
		[SerializeField] private EnemyAnimationEvents _animationEvents;
		[SerializeField] private HealthView _healthView;
		[SerializeField] private Collider _collider;

		public NavMeshAgent Agent => _agent;
		public Animator Animator => _animator;
		public Transform Transform => transform;
		public Transform ShootPoint => _shootPoint;
		public TriggerView TriggerView => _triggerView;
		public EnemyAnimationEvents EnemyAnimationEvents => _animationEvents;
		public HealthView HealthView => _healthView;
		public Collider Collider => _collider;
	}
}