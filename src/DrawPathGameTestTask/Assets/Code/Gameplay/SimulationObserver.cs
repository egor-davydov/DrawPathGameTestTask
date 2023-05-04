using System.Collections.Generic;
using System.Linq;
using Code.Infrastructure.States;
using Code.Services;
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
    
    private IGameStateMachine _gameStateMachine;

    public float SimulationTime => _simulationTime;

    private void Start()
    {
      _gameStateMachine = AllServices.Container.Single<IGameStateMachine>();
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
      _lose = true;
      _gameStateMachine.Enter<LoseState>();
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
      _gameStateMachine.Enter<WinState>();
    }

    private bool AllActorsFinished() =>
      _simulationActors.All(simulationActor => simulationActor.SimulationFinished);
  }
}