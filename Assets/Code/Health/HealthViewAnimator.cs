using DG.Tweening;

namespace Code.Health
{
	public class HealthViewAnimator
	{
		private readonly HealthView _view;
		private readonly Sequence _showSequence;
		
		private Tween _fillTween;

		public HealthViewAnimator(HealthView view)
		{
			_view = view;
			
			_showSequence = DOTween.Sequence();
			_showSequence.AppendCallback(() => _view.CanvasGroup.alpha = 1);
			_showSequence.AppendInterval(2);
			_showSequence.Append(_view.CanvasGroup.DOFade(0, 0.1f));
			_showSequence.SetAutoKill(false);
			_showSequence.Pause();
		}

		public void AnimateView(float endValue)
		{
			_showSequence.Restart();
			_fillTween?.Kill();
			_fillTween = _view.Fill.DOFillAmount(endValue, 0.2f);
		}
	}
}