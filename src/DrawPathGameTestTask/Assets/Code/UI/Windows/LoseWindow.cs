using Code.Infrastructure.States;
using Code.Services.Level;
using UnityEngine;
using UnityEngine.UI;

namespace Code.UI.Windows
{
  public class LoseWindow : MonoBehaviour
  {
    [SerializeField]
    private Button _restartLevelButton;

    private IGameStateMachine _gameStateMachine;
    private ILevelService _levelService;

    public void Construct(IGameStateMachine gameStateMachine, ILevelService levelService)
    {
      _levelService = levelService;
      _gameStateMachine = gameStateMachine;
    }
    
    private void Start() => 
      _restartLevelButton.onClick.AddListener(OnRestartLevelButtonClick);

    private void OnRestartLevelButtonClick() => 
      _gameStateMachine.Enter<LoadLevelState, string>(_levelService.CurrentLevelName());
  }
}