using Code.UI.OpenClose.MoveApart;
using UnityEngine;

namespace Code.UI.Restart
{
	[CreateAssetMenu(fileName = "RestartUiConfig", menuName = "Configs/UI/Restart Config")]
	public class RestartUiConfigSO : ScriptableObject, IUiMoveApartModel
	{
		[SerializeField] private Vector2 _openedSizeDelta;
		[SerializeField] private Vector2 _closedSizeDelta;
		[SerializeField] private float _animationDuration;
		[SerializeField] private float _afterDeathDelay;
		
		public Vector2 OpenedSizeDelta => _openedSizeDelta;
		public Vector2 ClosedSizeDelta => _closedSizeDelta;
		public float AnimationDuration => _animationDuration;
		public float AfterDeathDelay => _afterDeathDelay;
	}
}