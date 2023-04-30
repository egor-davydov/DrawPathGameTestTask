using Code.Infrastructure.States;
using Code.Services;
using UnityEngine;

namespace Code.Infrastructure
{
  public class GameBootstrapper : MonoBehaviour
  {
    private void Awake()
    {
      GameStateMachine stateMachine = new GameStateMachine(new SceneLoader(), new AllServices());
      
      stateMachine.Enter<BootstrapState>();
    }
  }
}
