using Code.UI.OpenClose.MoveApart;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Code.UI.Restart
{
	public class RestartUiView : MonoBehaviour, IUiMoveApartView
	{
		[SerializeField] private RectTransform _background;
		[SerializeField] private CanvasGroup _group;
		[SerializeField] private Button _button;

		public RectTransform Background => _background;
		public CanvasGroup Group => _group;
		public GameObject GameObject => gameObject;

		public void AddButtonCallback(UnityAction callback)
		{
			_button.onClick.RemoveAllListeners();
			_button.onClick.AddListener(callback);
		}
	}
}