using UnityEngine;

namespace Code.Animation
{
	public interface ILookMoveAnimationModel
	{
		float SpeedChangeLerp { get; }
		float NormalizedSpeed { get; }
		Vector3 MoveDirection { get; }
	}
}