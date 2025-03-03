using System;
using Code.Utils;
using Code.Utils.PlayerLoop;
using UnityEngine;

namespace Code.Health
{
	public class HealthController : IDisposable
	{
		private readonly IHealthModel _model;
		private readonly HealthView _view;
		private readonly LookAtCamera _lookAtCamera;
		private readonly HealthViewAnimator _animator;

		public HealthController(IHealthModel model, HealthView view, LoopUpdater updater, Transform camera)
		{
			_model = model;
			_view = view;

			_lookAtCamera = new LookAtCamera(updater, camera, view.transform);
			_animator = new HealthViewAnimator(view);
		}

		public void SetDefault()
		{
			_view.SetDefault(false, _model.Health / _model.MaxHealth);
		}

		public void TakeDamage(float damage)
		{
			if (_model.Health <= 0)
				return;

			_model.Health -= damage;
			_animator.AnimateView(_model.Health / _model.MaxHealth);

			if (_model.Health <= 0)
				_model.OnDeath?.Invoke();
		}

		public void Dispose()
		{
			_lookAtCamera.Dispose();
		}
	}
}