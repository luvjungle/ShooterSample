using DG.Tweening;
using UnityEngine;

namespace Code.UI.OpenClose.MoveApart
{
	public class MoveApartOpenClose : IUiOpenClose
	{
		private readonly IUiMoveApartView _view;
		private readonly IUiMoveApartModel _model;

		public bool Opened { get; private set; }

		private Sequence _openCloseSequence;

		public MoveApartOpenClose(IUiMoveApartView view, IUiMoveApartModel model)
		{
			_view = view;
			_model = model;
			
			_view.GameObject.SetActive(false);
		}

		public void Open()
		{
			Opened = true;

			_view.GameObject.SetActive(true);
			_view.Background.sizeDelta = _model.ClosedSizeDelta;
			_view.Group.alpha = 0;
			_view.Group.interactable = true;

			_openCloseSequence?.Kill();
			_openCloseSequence = DOTween.Sequence();

			_openCloseSequence.Append(_view.Background.DOSizeDelta(
				new Vector2(_model.OpenedSizeDelta.x, _model.ClosedSizeDelta.y), _model.AnimationDuration));

			_openCloseSequence.Append(_view.Background.DOSizeDelta(_model.OpenedSizeDelta, _model.AnimationDuration));

			_openCloseSequence.Append(_view.Group.DOFade(1, _model.AnimationDuration));
		}

		public void Close()
		{
			_view.Group.interactable = false;

			_openCloseSequence?.Kill();
			_openCloseSequence = DOTween.Sequence();

			_openCloseSequence.Append(_view.Group.DOFade(0, _model.AnimationDuration));

			_openCloseSequence.Append(_view.Background.DOSizeDelta(
				new Vector2(_model.OpenedSizeDelta.x, _model.ClosedSizeDelta.y), _model.AnimationDuration));

			_openCloseSequence.Append(_view.Background.DOSizeDelta(_model.ClosedSizeDelta, _model.AnimationDuration));

			_openCloseSequence.AppendCallback(() =>
			{
				_view.GameObject.SetActive(false);
				Opened = false;
			});
		}
	}
}