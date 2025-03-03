using UnityEngine;

namespace Code.Weapons.Bullets.Movers
{
	public class RbForwardBulletMover : IBulletMover
	{
		private readonly Rigidbody _rb;
		
		private IGenericBulletModel _model;
		private Vector3 _translation;
		
		public RbForwardBulletMover(Rigidbody rb)
		{
			_rb = rb;
		}

		public void SetDefault(IGenericBulletModel model)
		{
			_model = model;
			_translation = Vector3.zero;
		}

		public void Move(float deltaTime)
		{
			_translation = _rb.transform.forward * (_model.Speed * deltaTime);
			_rb.MovePosition(_rb.transform.position + _translation);
		}

		public Vector3 GetFrameTranslation() => _translation;
	}
}