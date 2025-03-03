using System;
using UnityEngine;

namespace Code.Enemies.Animation
{
	public class EnemyAnimationEvents : MonoBehaviour
	{
		public event Action OnAttack;
		
		public void AttackEvent() => OnAttack?.Invoke();
	}
}