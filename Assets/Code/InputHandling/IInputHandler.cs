using UnityEngine;

namespace Code.InputHandling
{
	public interface IInputHandler
	{
		Vector2 GetMovementInput();
		public void Enable(bool enable);
	}
}