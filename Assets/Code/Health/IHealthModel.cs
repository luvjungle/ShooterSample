using System;

namespace Code.Health
{
	public interface IHealthModel
	{
		float Health { get; set; }
		float MaxHealth { get; }
		Action OnDeath { get; set; }
	}
}