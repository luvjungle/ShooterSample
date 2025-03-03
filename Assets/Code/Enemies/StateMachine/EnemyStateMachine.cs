using Code.StateMachine;

namespace Code.Enemies.StateMachine
{
	public class EnemyStateMachine : BaseStateMachine
	{
		private readonly EnemyModel _model;

		public EnemyStateMachine(EnemyModel model, bool logging = false) : base(logging)
		{
			_model = model;
		}

		public override void Update(float deltaTime)
		{
			SelectState();
			base.Update(deltaTime);
		}

		private void SelectState()
		{
			if (!GetActiveState().CanExit)
				return;

			if (_model.Health <= 0)
				ChangeState<DeadState>();
			else if (_model.Target != null)
				ChangeState<MeleeAttackState>();
			else if (_model.MoveTarget.HasValue)
				ChangeState<MoveToTargetState>();
			else
				ChangeState<IdleState>();
		}
	}
}