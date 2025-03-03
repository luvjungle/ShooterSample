using System;
using System.Collections.Generic;
using Code.Utils.PlayerLoop.Interfaces;
using UnityEngine;

namespace Code.Utils.PlayerLoop
{
	public class LoopUpdater : MonoBehaviour
	{
		private readonly Dictionary<Type, LoopUpdateList> _updates = new()
		{
			{ typeof(IUpdate), new LoopUpdateList() },
			{ typeof(ILateUpdate), new LoopUpdateList() },
			{ typeof(IFixedUpdate), new LoopUpdateList() },
		};

		public void Add(ILoopUpdate target)
		{
			GetUpdateList(target).Add(target);
		}

		public void Remove(ILoopUpdate target)
		{
			GetUpdateList(target).Remove(target);
		}

		private LoopUpdateList GetUpdateList(ILoopUpdate target)
		{
			if (target is IUpdate)
				return _updates[typeof(IUpdate)];
			
			if (target is ILateUpdate)
				return _updates[typeof(ILateUpdate)];
			
			if (target is IFixedUpdate)
				return _updates[typeof(IFixedUpdate)];
			
			return _updates[typeof(IUpdate)];
		}

		private void Update()
		{
			var list = _updates[typeof(IUpdate)];
			list.Update(Time.deltaTime);
		}

		private void FixedUpdate()
		{
			var list = _updates[typeof(IFixedUpdate)];
			list.Update(Time.deltaTime);
		}

		private void LateUpdate()
		{
			var list = _updates[typeof(ILateUpdate)];
			list.Update(Time.deltaTime);
		}
	}
}