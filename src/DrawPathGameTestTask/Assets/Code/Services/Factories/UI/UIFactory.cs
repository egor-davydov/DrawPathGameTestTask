using Code.Services.AssetManagement;
using UnityEngine;

namespace Code.Services.Factories.UI
{
  public class UIFactory : IUIFactory
  {
    private readonly IAssetProvider _assets;

    public UIFactory(IAssetProvider assets)
    {
      _assets = assets;
    }
    
    public GameObject CreateWinWindow()
    {
      GameObject winWindowPrefab = _assets.Load(AssetPath.WinWindow);
      GameObject winWindowObject = Object.Instantiate(winWindowPrefab);
      
      return winWindowObject;
    }
    
    public GameObject CreateLoseWindow()
    {
      GameObject loseWindowPrefab = _assets.Load(AssetPath.LoseWindow);
      GameObject loseWindowObject = Object.Instantiate(loseWindowPrefab);
      
      return loseWindowObject;
    }
  }
}