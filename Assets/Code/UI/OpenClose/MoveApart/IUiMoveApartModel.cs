using UnityEngine;

namespace Code.UI.OpenClose.MoveApart
{
	public interface IUiMoveApartModel
	{
		public Vector2 OpenedSizeDelta { get; }
		public Vector2 ClosedSizeDelta { get; }
		public float AnimationDuration { get; }
	}
}