using Code.Gameplay.DrawingPath;
using UnityEngine;

namespace Code.Gameplay
{
  public class SimulationActor : MonoBehaviour
  {
    [SerializeField]
    private float SimulationTime = 3f;

    [SerializeField]
    private PathStart _pathStart;

    private float _currentSimulationTime;
    private float _timeToGoNextPosition;
    private float _simulationSpeed;
    private bool _speedCalculated;
    private bool _simulationStopped;
    private bool _alreadyWon;

    public bool Started { get; private set; }
    public bool Stopped { get; private set; }
    
    private void Update()
    {
      if (!Started || Stopped)
        return;
      
      if (!_speedCalculated)
        CalculateSimulationSpeed();

      UpdateCurrentTime();
        if (_pathStart.SimulationFinished)
          return;
        
        PathObject pathObject = _pathStart.PathObject;

        if (!TimeToGoNextPosition(_pathStart))
          return;

        Vector3 nextPosition = pathObject.NextPosition();
        pathObject.MoveNextPosition();
        TranslateToNextPosition(_pathStart, nextPosition);
        if (nextPosition == pathObject.LastPosition())
          _pathStart.SimulationFinished = true;
        else
          CalculateTimeToGoNextPosition(_pathStart);
    }

    public void StartSimulation() =>
      Started = true;

    public void StopSimulation() =>
      Stopped = true;

    private void Win()
    {
      Debug.Log("Win");
      _alreadyWon = true;
    }

    private void CalculateSimulationSpeed()
    {
      _simulationSpeed = _pathStart.PathObject.Length() / SimulationTime;
      _speedCalculated = true;
    }

    private void UpdateCurrentTime() =>
      _currentSimulationTime += Time.deltaTime;

    private bool TimeToGoNextPosition(PathStart pathStart) =>
      _currentSimulationTime > _timeToGoNextPosition;

    private static void TranslateToNextPosition(PathStart pathStart, Vector3 nextPosition) =>
      pathStart.transform.Translate(nextPosition - pathStart.transform.position);

    private void CalculateTimeToGoNextPosition(PathStart pathStart) =>
      _timeToGoNextPosition = pathStart.PathObject.DistanceToNextPosition() / _simulationSpeed
                                         + _currentSimulationTime;
  }
}