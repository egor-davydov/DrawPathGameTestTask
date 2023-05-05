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

    [SerializeField]
    private Button _restartButton;

    private IGameStateMachine _gameStateMachine;
    private ILevelService _levelService;

    public void Construct(IGameStateMachine gameStateMachine, ILevelService levelService)
    {
      _levelService = levelService;
      _gameStateMachine = gameStateMachine;
    }
    
    private void Start()
    {
      _nextLevelButton.onClick.AddListener(OnNextLevelButtonClick);
      _restartButton.onClick.AddListener(OnRestartButtonClick);
      if(!_levelService.HasNextLevel())
        _nextLevelButton.gameObject.SetActive(false);
    }

    private void OnNextLevelButtonClick() => 
      _gameStateMachine.Enter<LoadLevelState, string>(_levelService.NextLevelName());

    private void OnRestartButtonClick() => 
      _gameStateMachine.Enter<LoadLevelState, string>(_levelService.CurrentLevelName());
  }
}