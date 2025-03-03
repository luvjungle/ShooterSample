using System.Collections.Generic;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Code.Utils
{
	public class SinglePool<T> where T : Component
	{
		private readonly T _prefab;
		private readonly IObjectResolver _resolver;
		private readonly bool _hasResolver;
		
		public readonly List<T> Pool = new();

		public SinglePool(T prefab, IObjectResolver resolver)
		{
			_prefab = prefab;

			if (resolver != null)
			{
				_resolver = resolver;
				_hasResolver = true;
			}
		}

		public SinglePool(T prefab)
		{
			_prefab = prefab;
		}

		public T TakeFromPool(Vector3 pos = default, Quaternion rot = default)
		{
			for (int i = Pool.Count - 1; i >= 0; i--)
			{
				if (!Pool[i])
				{
					Pool.RemoveAt(i);
					continue;
				}

				if (Pool[i].gameObject.activeSelf) continue;

				Pool[i].transform.SetPositionAndRotation(pos, rot);
				Pool[i].gameObject.SetActive(true);
				return Pool[i];
			}

			T newObject;

			if (_hasResolver)
				newObject = _resolver.Instantiate(_prefab, pos, rot);
			else
				newObject = GameObject.Instantiate(_prefab, pos, rot);

			Pool.Add(newObject);
			return newObject;
		}
	}
}