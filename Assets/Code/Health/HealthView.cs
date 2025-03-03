using UnityEngine;
using UnityEngine.UI;

namespace Code.Health
{
	public class HealthView : MonoBehaviour
	{
		[SerializeField] private Image _fill;
		[SerializeField] private CanvasGroup _canvasGroup;
		
		public Image Fill => _fill;
		public CanvasGroup CanvasGroup => _canvasGroup;

		public void SetDefault(bool showAtStart, float fill)
		{
			_canvasGroup.alpha = showAtStart ? 1f : 0f;
			_fill.fillAmount = fill;
		}
	}
}