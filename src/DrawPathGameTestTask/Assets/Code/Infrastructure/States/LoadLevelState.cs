namespace Code.Infrastructure.States
{
  public class LoadLevelState : IPayloadState<string>
  {
    public LoadLevelState(GameStateMachine gameStateMachine, SceneLoader sceneLoader)
    {
      
    }

    public void Enter(string payload)
    {
    }

    public void Exit()
    {
    }
  }
}