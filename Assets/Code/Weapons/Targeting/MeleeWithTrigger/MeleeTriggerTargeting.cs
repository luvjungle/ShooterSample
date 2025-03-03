using System;
using UnityEngine;

namespace Code.Weapons.Targeting.MeleeWithTrigger
{
	public class MeleeTriggerTargeting : IDisposable
	{
		private readonly ITriggerView _triggerView;
		private readonly IMeleeTriggerModel _model;

		public MeleeTriggerTargeting(ITriggerView triggerView, IMeleeTriggerModel model)
		{
			_triggerView = triggerView;
			_model = model;

			_triggerView.TriggerEnter += OnTriggerViewEnter;
			_triggerView.TriggerExit += OnTriggerViewExit;
		}

		private void OnTriggerViewEnter(Collider other)
		{
			if (!TryGetDamageable(other, out IDamageable damageable))
				return;

			if (_model.Target == null || _model.Target.Dead)
				_model.Target = damageable;
		}

		private void OnTriggerViewExit(Collider other)
		{
			if (!TryGetDamageable(other, out IDamageable damageable))
				return;

			if (_model.Target == damageable)
				_model.Target = null;
		}

		private bool TryGetDamageable(Collider other, out IDamageable damageable)
		{
			damageable = null;

			if ((_model.EnemyLayer & (1 << other.gameObject.layer)) == 0)
				return false;

			return other.TryGetComponent(out damageable);
		}

		public void Update()
		{
			if (_model.Target is { Dead: true })
				_model.Target = null;
		}

		public void Dispose()
		{
			_triggerView.TriggerEnter -= OnTriggerViewEnter;
			_triggerView.TriggerExit -= OnTriggerViewExit;
		}
	}
}