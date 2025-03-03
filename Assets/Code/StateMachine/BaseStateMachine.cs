using System;
using System.Collections.Generic;
using UnityEngine;

namespace Code.StateMachine
{
	public class BaseStateMachine
	{
		private readonly Dictionary<Type, IState> _states;
		private readonly bool _logging;

		private IState _activeState;

		public BaseStateMachine(bool logging = false)
		{
			_states = new Dictionary<Type, IState>();
			_logging = logging;
		}

		public bool IsStateActive<TState>() where TState : class, IState =>
			_activeState.GetType() == typeof(TState);

		public virtual void Update(float deltaTime) =>
			_activeState?.Update(deltaTime);

		public void RegisterState<TState>(TState state) where TState : class, IState =>
			_states.Add(typeof(TState), state);

		public void ChangeState<TState>() where TState : class, IState
		{
			var newState = GetState<TState>();
			if (_activeState == newState)
				return;

			if (_logging)
				Debug.Log($"Exit {_activeState}");

			_activeState?.Exit();
			_activeState = newState;
			_activeState.Enter();

			if (_logging)
				Debug.Log($"Enter {_activeState}");
		}

		public TState GetState<TState>() where TState : class, IState =>
			_states[typeof(TState)] as TState;

		public bool HasState<TState>() where TState : class, IState => _states.ContainsKey(typeof(TState));

		public IState GetActiveState() => _activeState;
	}
}