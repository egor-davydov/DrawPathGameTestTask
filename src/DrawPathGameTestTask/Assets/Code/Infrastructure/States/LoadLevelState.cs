namespace Code.Infrastructure.States
{
  public class LoadLevelState : IPayloadState<string>
  {
    private readonly GameStateMachine _gameStateMachine;
    private readonly SceneLoader _sceneLoader;

    public LoadLevelState(GameStateMachine gameStateMachine, SceneLoader sceneLoader)
    {
      _gameStateMachine = gameStateMachine;
      _sceneLoader = sceneLoader;
    }

    public void Enter(string levelName)
    {
      _sceneLoader.Load(levelName, OnLoaded);
    }

    private void OnLoaded()
    {
    }

    public void Exit()
    {
    }
  }
}