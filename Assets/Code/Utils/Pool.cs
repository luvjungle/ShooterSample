using System.Collections.Generic;
using UnityEngine;
using VContainer;

namespace Code.Utils
{
	public class Pool<T> where T : Component
	{
		private readonly IObjectResolver _resolver;
		private readonly Dictionary<GameObject, SinglePool<T>> _objects = new();

		public Pool(IObjectResolver resolver)
		{
			_resolver = resolver;
		}

		public Pool() { }
		
		public List<T> List(T prefab) => _objects.GetValueOrDefault(prefab.gameObject)?.Pool;
		
		public SinglePool<T> GetPool(T prefab)
		{
			if (!_objects.ContainsKey(prefab.gameObject))
				_objects.Add(prefab.gameObject, new SinglePool<T>(prefab, _resolver));

			return _objects[prefab.gameObject];
		}

		public T TakeFromPool(T prefab, Vector3 pos = default, Quaternion rot = default)
		{
			if (!prefab) return null;

			if (!_objects.ContainsKey(prefab.gameObject))
				_objects.Add(prefab.gameObject, new SinglePool<T>(prefab, _resolver));

			var pool = _objects[prefab.gameObject];
			return pool.TakeFromPool(pos, rot);
		}
	}
}