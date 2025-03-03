using System;
using System.Collections.Generic;

namespace Code.Weapons.Bullets.Damagers
{
	public class BulletDamagerFactory
	{
		private readonly Dictionary<Type, Stack<IBulletDamager>> _pool = new();
		
		public IBulletDamager Get(IGenericBulletModel model)
		{
			if (model.DamageRadius > 0.05f)
				return Get<AreaBulletDamager>();

			return Get<HitBulletDamager>();
		}

		private IBulletDamager Get<T>() where T : IBulletDamager, new()
		{
			if (!_pool.ContainsKey(typeof(T)))
				_pool.Add(typeof(T), new Stack<IBulletDamager>());

			if (_pool[typeof(T)].Count == 0)
				return new T();
			
			return _pool[typeof(T)].Pop();
		}

		public void Return<T>(T damager) where T : IBulletDamager
		{
			if (damager == null)
				return;
			
			if (!_pool.ContainsKey(typeof(T)))
				_pool.Add(typeof(T), new Stack<IBulletDamager>());
			
			_pool[typeof(T)].Push(damager);
		}
	}
}