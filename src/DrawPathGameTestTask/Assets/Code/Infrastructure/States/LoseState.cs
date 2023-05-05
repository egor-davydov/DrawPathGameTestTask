using Code.Services.Factories.UI;

namespace Code.Infrastructure.States
{
  public class LoseState : IState
  {
    private readonly IUIFactory _uiFactory;

    public LoseState(IUIFactory uiFactory)
    {
      _uiFactory = uiFactory;
    }

    public void Enter()
    {
      _uiFactory.CreateLoseWindow();
    }

    public void Exit()
    {
    }
  }
}