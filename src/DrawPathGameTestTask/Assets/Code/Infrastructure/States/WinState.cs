using Code.Services.Factories.UI;

namespace Code.Infrastructure.States
{
  public class WinState : IState
  {
    private readonly IUIFactory _uiFactory;

    public WinState(IUIFactory uiFactory)
    {
      _uiFactory = uiFactory;
    }

    public void Enter()
    {
      _uiFactory.CreateWinWindow();
    }

    public void Exit()
    {
    }
  }
}