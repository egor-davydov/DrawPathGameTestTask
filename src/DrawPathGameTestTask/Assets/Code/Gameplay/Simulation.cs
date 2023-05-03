using System.Collections.Generic;
using System.Linq;
using Code.Gameplay.DrawingPath;
using UnityEngine;

namespace Code.Gameplay
{
  public class Simulation : MonoBehaviour
  {
    [SerializeField]
    private List<PathStart> PathStarts;

    [SerializeField]
    private float SimulationTime = 3f;

    private float _currentSimulationTime;
    private readonly Dictionary<PathStart, float> _timeToGoNextPosition = new();
    private readonly Dictionary<PathStart, float> _simulationSpeed = new();
    private bool _speedCalculated;
    private bool _simulationStopped;
    private bool _alreadyWon;
    
    public bool Started { get; private set; }
    public bool Stopped { get; private set; }

    private void Start()
    {
      foreach (PathStart pathStart in PathStarts)
        _timeToGoNextPosition.Add(pathStart, 0);
    }

    private void Update()
    {
      if (!Started || Stopped)
        return;
      
      if (SimulationFinished())
      {
        if (!_alreadyWon)
          Win();
        return;
      }

      if (!_speedCalculated)
        CalculateSimulationSpeed();

      UpdateCurrentTime();
      foreach (PathStart pathStart in PathStarts)
      {
        if (pathStart.SimulationFinished)
          continue;
        PathObject pathObject = pathStart.PathObject;

        if (!TimeToGoNextPosition(pathStart))
          return;

        Vector3 nextPosition = pathObject.NextPosition();
        pathObject.MoveNextPosition();
        TranslateToNextPosition(pathStart, nextPosition);
        if (nextPosition == pathObject.LastPosition())
          pathStart.SimulationFinished = true;
        else
          CalculateTimeToGoNextPosition(pathStart);
      }
    }

    public bool ShouldStartSimulation() =>
      PathStarts.All(start => start.PathObject != null);

    public void StartSimulation() =>
      Started = true;

    public void StopSimulation() =>
      Stopped = true;

    private void Win()
    {
      Debug.Log("Win");
      _alreadyWon = true;
    }

    private bool SimulationFinished() =>
      PathStarts.All(start => start.SimulationFinished);

    private void CalculateSimulationSpeed()
    {
      foreach (PathStart pathStart in PathStarts)
        _simulationSpeed.Add(pathStart, pathStart.PathObject.Length() / SimulationTime);
      _speedCalculated = true;
    }

    private void UpdateCurrentTime() =>
      _currentSimulationTime += Time.deltaTime;

    private bool TimeToGoNextPosition(PathStart pathStart) =>
      _currentSimulationTime > _timeToGoNextPosition[pathStart];

    private static void TranslateToNextPosition(PathStart pathStart, Vector3 nextPosition) =>
      pathStart.transform.Translate(nextPosition - pathStart.transform.position);

    private void CalculateTimeToGoNextPosition(PathStart pathStart) =>
      _timeToGoNextPosition[pathStart] = pathStart.PathObject.DistanceToNextPosition() / _simulationSpeed[pathStart]
                                         + _currentSimulationTime;
  }
}