using System.Collections.Generic;
using UnityEngine;

namespace Code.Weapons.Targeting.Weapon
{
	public class WeaponTargeting
	{
		private readonly IReadOnlyList<IDamageable> _targetList;
		private readonly IWeaponTargetingModel _model;

		public WeaponTargeting(IReadOnlyList<IDamageable> targetList, IWeaponTargetingModel model)
		{
			_targetList = targetList;
			_model = model;
		}

		public void Update()
		{
			if (_model.Weapon == null)
			{
				_model.Target = null;
				return;
			}

			if (_model.Target != null)
				CheckTarget();

			if (_model.Target == null)
				FindNewTarget();
		}

		private void FindNewTarget()
		{
			IDamageable result = null;
			for (int i = 0; i < _targetList.Count; i++)
			{
				var target = _targetList[i];
				if (target.Dead)
					continue;

				var sqDistance = Vector3.SqrMagnitude(_model.Shooter.position - target.Transform.position);
				if (sqDistance > _model.Weapon.Range * _model.Weapon.Range)
					continue;

				if (!RayHitTarget(target))
					continue;

				if (result == null ||
				    sqDistance < Vector3.SqrMagnitude(_model.Shooter.position - result.Transform.position))
				{
					result = target;
				}
			}

			_model.Target = result;
		}

		private bool RayHitTarget(IDamageable target)
		{
			var shootDir = target.ShootPoint.position - _model.TargetingRaycastPoint.position;
			
			bool rayHit = Physics.Raycast(_model.TargetingRaycastPoint.position, shootDir.normalized,
				out RaycastHit hitInfo, shootDir.magnitude, _model.Weapon.HitLayerMask.value, QueryTriggerInteraction.Ignore);

			return rayHit && hitInfo.collider.transform == target.Transform;
		}

		private void CheckTarget()
		{
			if (_model.Target.Dead || TargetOutOfRange() || !RayHitTarget(_model.Target))
				_model.Target = null;
		}

		private bool TargetOutOfRange()
		{
			return Vector3.SqrMagnitude(_model.Shooter.position - _model.Target.Transform.position) >
			       _model.Weapon.ReleaseRange * _model.Weapon.ReleaseRange;
		}
	}
}