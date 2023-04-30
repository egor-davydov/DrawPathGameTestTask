using Code.Services;

namespace Code.Infrastructure.States
{
  public class BootstrapState : IState
  {
    private readonly GameStateMachine _gameStateMachine;

    public BootstrapState(GameStateMachine gameStateMachine, AllServices services)
    {
      _gameStateMachine = gameStateMachine;
    }

    public void Enter()
    {
      _gameStateMachine.Enter<LoadLevelState, string>("Level 1");
    }

    public void Exit()
    {
      
    }
  }
}