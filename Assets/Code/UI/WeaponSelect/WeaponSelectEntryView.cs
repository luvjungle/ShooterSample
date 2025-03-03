using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Code.UI.WeaponSelect
{
	public class WeaponSelectEntryView : MonoBehaviour
	{
		[SerializeField] private TMP_Text _name;
		[SerializeField] private Image _icon;
		[SerializeField] private TMP_Text _description;
		[SerializeField] private Button _selectButton;
		
		public void SetName(string weaponName) => _name.SetText(weaponName);
		
		public void SetIcon(Sprite icon) => _icon.sprite = icon;
		
		public void SetDescription(string description) => _description.SetText(description);

		public void AddSelectCallback(UnityAction callback)
		{
			_selectButton.onClick.RemoveAllListeners();
			_selectButton.onClick.AddListener(callback);
		}
	}
}