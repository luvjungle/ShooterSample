using Code.Weapons.Targeting.Weapon;
using UnityEngine;
using UnityEngine.Animations.Rigging;

namespace Code.Player.Animation
{
	public class HandIkController
	{
		private readonly TwoBoneIKConstraint _constraint;
		private readonly Transform _target;
		private readonly IWeaponTargetingModel _targetingModel;
		private readonly IHandIkModel _model;

		public HandIkController(TwoBoneIKConstraint constraint, Transform target, IWeaponTargetingModel targetingModel,
			IHandIkModel model)
		{
			_constraint = constraint;
			_target = target;
			_targetingModel = targetingModel;
			_model = model;
		}
		
		public void SetDefault() => _constraint.weight = 0;

		public void Update(float deltaTime)
		{
			if (_targetingModel.Target == null)
			{
				_constraint.weight =
					Mathf.MoveTowards(_constraint.weight, 0, _model.ConstraintActivateSpeed * deltaTime);
			}
			else
			{
				var lookDir = _targetingModel.Target.ShootPoint.position - _constraint.data.tip.position;
				
				_target.position = _targetingModel.Target.ShootPoint.position;
				_target.rotation = Quaternion.LookRotation(lookDir.normalized);
				
				_constraint.weight =
					Mathf.MoveTowards(_constraint.weight, 1, _model.ConstraintActivateSpeed * deltaTime);
			}

			_model.IkActive = Mathf.Approximately(_constraint.weight, 1);
		}
	}
}