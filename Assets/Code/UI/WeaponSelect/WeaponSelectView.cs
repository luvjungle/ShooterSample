using System.Collections.Generic;
using Code.UI.OpenClose;
using Code.UI.OpenClose.MoveApart;
using UnityEngine;

namespace Code.UI.WeaponSelect
{
	public class WeaponSelectView : MonoBehaviour, IUiMoveApartView
	{
		[SerializeField] private RectTransform _background;
		[SerializeField] private CanvasGroup _group;
		[SerializeField] private List<WeaponSelectEntryView> _entries;
		
		public RectTransform Background => _background;
		public CanvasGroup Group => _group;
		public GameObject GameObject => gameObject;
		public List<WeaponSelectEntryView> Entries => _entries;
	}
}