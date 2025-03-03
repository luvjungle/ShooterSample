using UnityEngine;

namespace Code.InputHandling
{
	public class VirtualJoystickInput : IInputHandler
	{
		private readonly Joystick _joystick;
		
		public VirtualJoystickInput(Joystick joystick)
		{
			_joystick = joystick;
		}
		
		public Vector2 GetMovementInput()
		{
			return _joystick.Direction;
		}

		public void Enable(bool enable) => _joystick.gameObject.SetActive(enable);
	}
}