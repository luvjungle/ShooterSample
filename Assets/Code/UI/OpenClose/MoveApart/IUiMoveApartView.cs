using UnityEngine;

namespace Code.UI.OpenClose.MoveApart
{
	public interface IUiMoveApartView
	{
		public RectTransform Background { get; }
		public CanvasGroup Group { get; }
		public GameObject GameObject { get; }
	}
}