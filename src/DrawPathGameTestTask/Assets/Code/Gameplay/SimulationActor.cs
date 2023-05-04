using System;
using Code.Gameplay.DrawingPath;
using UnityEngine;

namespace Code.Gameplay
{
  public class SimulationActor : MonoBehaviour
  {
    [SerializeField]
    private SimulationObserver _simulationObserver;

    [SerializeField]
    private PathActor _pathActor;

    private float _currentSimulationTime;
    private float _timeToGoNextPosition;
    private float _simulationSpeed;
    private bool _speedCalculated;
    private bool _simulationStopped;
    private bool _alreadyWon;
    private PathObject PathObject => _pathActor.PathObject;
    
    public bool SimulationStarted { get; private set; }
    public bool SimulationStopped { get; private set; }
    public bool SimulationFinished { get; private set; }
    
    public event Action Finished;
    public event Action Stopped;

    private void OnCollisionEnter2D(Collision2D col) =>
      _simulationObserver.StopSimulation();
    
    private void Update()
    {
      if (!SimulationStarted || SimulationFinished || SimulationStopped)
        return;

      if (!_speedCalculated)
        CalculateSimulationSpeed();

      UpdateCurrentTime();

      if (!IsTimeToGoNextPosition())
        return;

      Vector3 nextPosition = PathObject.NextPosition();
      PathObject.MoveNextPosition();
      TranslateToNextPosition(nextPosition);
      if (nextPosition == PathObject.LastPosition())
        FinishSimulation();
      else
        CalculateTimeToGoNextPosition();
    }

    public void StartSimulation() =>
      SimulationStarted = true;

    public void StopSimulation()
    {
      SimulationStopped = true;
      Stopped?.Invoke();
    }

    private void FinishSimulation()
    {
      SimulationFinished = true;
      Finished?.Invoke();
    }

    private void CalculateSimulationSpeed()
    {
      _simulationSpeed = _pathActor.PathObject.Length() / _simulationObserver.SimulationTime;
      _speedCalculated = true;
    }

    private void UpdateCurrentTime() =>
      _currentSimulationTime += Time.deltaTime;

    private bool IsTimeToGoNextPosition() =>
      _currentSimulationTime > _timeToGoNextPosition;

    private void TranslateToNextPosition(Vector3 nextPosition) =>
      _pathActor.transform.Translate(nextPosition - _pathActor.transform.position);

    private void CalculateTimeToGoNextPosition() =>
      _timeToGoNextPosition = _pathActor.PathObject.DistanceToNextPosition() / _simulationSpeed
                              + _currentSimulationTime;
  }
}