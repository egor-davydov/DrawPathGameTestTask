using Code.Services;
using Code.Services.AssetManagement;
using Code.Services.Factories.UI;

namespace Code.Infrastructure.States
{
  public class BootstrapState : IState
  {
    private readonly GameStateMachine _gameStateMachine;
    private readonly AllServices _services;

    public BootstrapState(GameStateMachine gameStateMachine, AllServices services)
    {
      _gameStateMachine = gameStateMachine;
      _services = services;
      RegisterServices();
    }

    public void Enter() => 
      _gameStateMachine.Enter<LoadLevelState, string>("Level 1");

    public void Exit()
    {
    }

    private void RegisterServices()
    {
      _services.RegisterSingle<IAssetProvider>(new AssetProvider());
      _services.RegisterSingle<IGameStateMachine>(_gameStateMachine);

      _services.RegisterSingle<IUIFactory>(new UIFactory(
        _services.Single<IAssetProvider>()
      ));
    }
  }
}