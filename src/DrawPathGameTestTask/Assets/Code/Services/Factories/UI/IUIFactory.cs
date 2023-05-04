using UnityEngine;

namespace Code.Services.Factories.UI
{
  public interface IUIFactory : IService
  {
    GameObject CreateLoseWindow();
    GameObject CreateWinWindow();
  }
}