using System.Collections.Generic;
using System.Linq;
using Code.Infrastructure.States;
using UnityEngine;

namespace Code.Gameplay
{
  public class SimulationObserver : MonoBehaviour
  {
    [SerializeField]
    private List<SimulationActor> _simulationActors;

    [SerializeField]
    private float _simulationTime = 3f;

    private float _currentSimulationTime;
    private bool _simulationStopped;
    private bool _lose;
    
    private GameStateMachine _gameStateMachine;

    public float SimulationTime => _simulationTime;

    private void Start()
    {
      foreach (SimulationActor simulationActor in _simulationActors)
      {
        simulationActor.Finished += OnActorFinished;
        simulationActor.Stopped += OnActorStopped;
      }
    }

    private void OnDestroy()
    {
      foreach (SimulationActor simulationActor in _simulationActors)
      {
        simulationActor.Finished -= OnActorFinished;
        simulationActor.Stopped -= OnActorStopped;
      }
    }

    private void OnActorFinished()
    {
      if (AllActorsFinished())
        Win();
    }

    private void OnActorStopped()
    {
      if (!_lose)
        Lose();
    }

    private void Lose()
    {
      Debug.Log("Lose");
      _lose = true;
    }

    public void StartSimulation()
    {
      foreach (SimulationActor simulationActor in _simulationActors)
        simulationActor.StartSimulation();
    }

    public void StopSimulation()
    {
      foreach (SimulationActor simulationActor in _simulationActors)
        simulationActor.StopSimulation();
    }

    private void Win()
    {
      //_gameStateMachine.Enter<WinState>();
      Debug.Log("Win");
    }

    private bool AllActorsFinished() =>
      _simulationActors.All(simulationActor => simulationActor.SimulationFinished);
  }
}