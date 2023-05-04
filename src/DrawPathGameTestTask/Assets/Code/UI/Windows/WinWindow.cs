using Code.Infrastructure.States;
using Code.Services.Level;
using UnityEngine;
using UnityEngine.UI;

namespace Code.UI.Windows
{
  public class WinWindow : MonoBehaviour
  {
    [SerializeField]
    private Button _nextLevelButton;

    private IGameStateMachine _gameStateMachine;
    private ILevelService _levelService;

    public void Construct(IGameStateMachine gameStateMachine, ILevelService levelService)
    {
      _levelService = levelService;
      _gameStateMachine = gameStateMachine;
    }
    
    private void Start() => 
      _nextLevelButton.onClick.AddListener(OnNextLevelButtonClick);

    private void OnNextLevelButtonClick() => 
      _gameStateMachine.Enter<LoadLevelState, string>(_levelService.NextLevelName());
  }
}