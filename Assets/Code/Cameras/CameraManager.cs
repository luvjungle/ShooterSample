using Cinemachine;
using UnityEngine;

namespace Code.Cameras
{
	public class CameraManager : MonoBehaviour
	{
		[SerializeField] private Camera _camera;
		[SerializeField] private CinemachineVirtualCamera _playerFollow;
		
		public Camera MainCamera => _camera;

		public void SetPlayerFollow(Transform player)
		{
			_playerFollow.Follow = player;
			_playerFollow.LookAt = player;
		}
	}
}