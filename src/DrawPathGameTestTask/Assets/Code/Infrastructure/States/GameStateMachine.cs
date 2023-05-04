using System;
using System.Collections.Generic;
using Code.Services;

namespace Code.Infrastructure.States
{
  public class GameStateMachine
  {
    private readonly Dictionary<Type, IExitableState> _states;

    private IExitableState _currentState;

    public GameStateMachine(SceneLoader sceneLoader, AllServices services)
    {
      _states = new Dictionary<Type, IExitableState>
      {
        [typeof(BootstrapState)] = new BootstrapState(this, services),
        [typeof(LoadLevelState)] = new LoadLevelState(this, sceneLoader),
        [typeof(DrawingState)] = new DrawingState(),
        [typeof(WinState)] = new WinState(this),
        [typeof(LoseState)] = new LoseState(this),
      };
    }

    public void Enter<TState>() where TState : IState
    {
      IState state = ChangeState<TState>();
      state.Enter();
    }

    public void Enter<TState, TPayload>(TPayload payload) where TState : IPayloadState<TPayload>
    {
      IPayloadState<TPayload> state = ChangeState<TState>();
      state.Enter(payload);
    }

    private TState ChangeState<TState>() where TState : IExitableState
    {
      _currentState?.Exit();
      TState state = (TState)_states[typeof(TState)];
      _currentState = state;

      return state;
    }
  }
}