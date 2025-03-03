using System.Collections.Generic;
using Code.Weapons;
using UnityEngine;

namespace Code.UI.WeaponSelect
{
	[CreateAssetMenu(fileName = "WeaponSelectConfig", menuName = "Configs/UI/Weapon Select Config")]
	public class WeaponSelectConfigSO : ScriptableObject
	{
		[SerializeField] private List<WeaponConfigSO> _weapons;
		[SerializeField] private Vector2 _openedSizeDelta;
		[SerializeField] private Vector2 _closedSizeDelta;
		[SerializeField] private float _animationDuration;
		
		public List<WeaponConfigSO> Weapons => _weapons;
		public Vector2 OpenedSizeDelta => _openedSizeDelta;
		public Vector2 ClosedSizeDelta => _closedSizeDelta;
		public float AnimationDuration => _animationDuration;
	}
}