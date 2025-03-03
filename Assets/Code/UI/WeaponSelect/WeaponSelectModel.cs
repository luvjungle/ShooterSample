using System.Collections.Generic;
using Code.UI.OpenClose;
using Code.UI.OpenClose.MoveApart;
using Code.Weapons;
using UnityEngine;

namespace Code.UI.WeaponSelect
{
	public class WeaponSelectModel : IUiMoveApartModel
	{
		public List<WeaponConfigSO> Weapons => _config.Weapons;
		public Vector2 OpenedSizeDelta => _config.OpenedSizeDelta;
		public Vector2 ClosedSizeDelta => _config.ClosedSizeDelta;
		public float AnimationDuration => _config.AnimationDuration;

		private readonly WeaponSelectConfigSO _config;

		public WeaponSelectModel(WeaponSelectConfigSO config)
		{
			_config = config;
		}
	}
}