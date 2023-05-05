using Code.Services;

namespace Code.Infrastructure.States
{
  public interface IGameStateMachine : IService
  {
    void Enter<TState>() where TState : IState;
    void Enter<TState, TPayload>(TPayload payload) where TState : IPayloadState<TPayload>;
  }
}