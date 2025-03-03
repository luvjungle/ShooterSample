using System;

namespace Code.Weapons.Bullets
{
	public class BulletLifetime
	{
		private float _lifetime;

		public event Action OnTimerFinished;

		public void SetDefault(float lifetime)
		{
			_lifetime = lifetime;
		}

		public void Update(float deltaTime)
		{
			if (_lifetime < 0)
				return;
			
			_lifetime -= deltaTime;
			
			if (_lifetime < 0)
				OnTimerFinished?.Invoke();
		}
	}
}