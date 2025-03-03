using System.Collections.Generic;
using Code.Weapons.Targeting;

namespace Code.Enemies
{
	public class EnemyList
	{
		private readonly List<IDamageable> _enemies = new();
		
		public IReadOnlyList<IDamageable> Enemies => _enemies;
		
		public void Add(IDamageable enemy)
		{
			if (!_enemies.Contains(enemy))
				_enemies.Add(enemy);
		}

		public void Remove(IDamageable enemy)
		{
			_enemies.Remove(enemy);
		}
	}
}