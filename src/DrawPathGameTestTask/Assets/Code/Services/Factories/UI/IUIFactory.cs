using UnityEngine;

namespace Code.Services.Factories.UI
{
  public interface IUIFactory
  {
    GameObject CreateLoseWindow();
    GameObject CreateWinWindow();
  }
}