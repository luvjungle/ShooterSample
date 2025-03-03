using System.Collections.Generic;
using Code.Utils.PlayerLoop.Interfaces;

namespace Code.Utils.PlayerLoop
{
	public class LoopUpdateList
	{
		private readonly List<ILoopUpdate> _list = new();
		
		public void Add(ILoopUpdate target)
		{
			if (!_list.Contains(target))
				_list.Add(target);
		}

		public void Remove(ILoopUpdate target)
		{
			_list.Remove(target);
		}
		
		public void Update(float deltaTime)
		{
			for (int i = _list.Count - 1; i >= 0; i--)
				_list[i].Tick(deltaTime);
		}
	}
}