using Code.Player;
using Code.UI.OpenClose;
using Code.UI.OpenClose.MoveApart;
using Code.Weapons;
using UnityEngine;

namespace Code.UI.WeaponSelect
{
	public class WeaponSelectController
	{
		private readonly WeaponSelectView _view;
		private readonly PlayerController _player;
		private readonly WeaponSelectModel _model;
		private readonly IUiOpenClose _openClose;

		public bool Opened => _openClose.Opened;

		public WeaponSelectController(WeaponSelectView view, PlayerController player, WeaponSelectModel model)
		{
			_view = view;
			_player = player;
			_model = model;

			InitEntries();
			_openClose = new MoveApartOpenClose(view, model);
		}

		private void InitEntries()
		{
			for (int i = 0; i < _model.Weapons.Count; i++)
			{
				var weapon = _model.Weapons[i];

				if (i >= _view.Entries.Count)
				{
					var newEntry = GameObject.Instantiate(_view.Entries[0], _view.Entries[0].transform.parent);
					_view.Entries.Add(newEntry);
				}

				var entry = _view.Entries[i];
				entry.SetName(weapon.Name);
				entry.SetDescription(weapon.Description);
				entry.SetIcon(weapon.Icon);
				entry.AddSelectCallback(() => SelectWeapon(weapon));
			}
		}

		public void Open() => _openClose.Open();

		private void Close() => _openClose.Close();

		private void SelectWeapon(WeaponConfigSO weapon)
		{
			_player.SetWeapon(weapon);
			Close();
		}
	}
}