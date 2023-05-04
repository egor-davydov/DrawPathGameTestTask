using Code.Infrastructure.States;
using Code.Services.AssetManagement;
using Code.Services.Level;
using Code.UI.Windows;
using UnityEngine;

namespace Code.Services.Factories.UI
{
  public class UIFactory : IUIFactory
  {
    private readonly IAssetProvider _assets;
    private readonly IGameStateMachine _gameStateMachine;
    private readonly ILevelService _levelService;

    public UIFactory(IAssetProvider assets, IGameStateMachine gameStateMachine, ILevelService levelService)
    {
      _assets = assets;
      _gameStateMachine = gameStateMachine;
      _levelService = levelService;
    }
    
    public GameObject CreateWinWindow()
    {
      GameObject winWindowPrefab = _assets.Load(AssetPath.WinWindow);
      GameObject winWindowObject = Object.Instantiate(winWindowPrefab);
      
      WinWindow winWindow = winWindowObject.GetComponent<WinWindow>();
      winWindow.Construct(_gameStateMachine, _levelService);

      return winWindowObject;
    }
    
    public GameObject CreateLoseWindow()
    {
      GameObject loseWindowPrefab = _assets.Load(AssetPath.LoseWindow);
      GameObject loseWindowObject = Object.Instantiate(loseWindowPrefab);

      LoseWindow loseWindow = loseWindowObject.GetComponent<LoseWindow>();
      loseWindow.Construct(_gameStateMachine, _levelService);

      return loseWindowObject;
    }
  }
}