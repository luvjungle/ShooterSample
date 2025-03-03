using System;
using Code.Utils.PlayerLoop;
using Code.Utils.PlayerLoop.Interfaces;
using UnityEngine;

namespace Code.Utils
{
	public class LookAtCamera: ILateUpdate, IDisposable
	{
		private readonly LoopUpdater _updater;
		private readonly Transform _camera;
		private readonly Transform _target;
		
		public LookAtCamera(LoopUpdater updater, Transform camera, Transform target)
		{
			_updater = updater;
			_camera = camera;
			_target = target;
			
			_updater.Add(this);
		}
		
		public void Tick(float deltaTime)
		{
			_target.rotation = Quaternion.LookRotation(_camera.forward);
		}

		public void Dispose()
		{
			_updater.Remove(this);
		}
	}
}