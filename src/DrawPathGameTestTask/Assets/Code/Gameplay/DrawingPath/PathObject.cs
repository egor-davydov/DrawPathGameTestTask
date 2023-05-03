using UnityEngine;

namespace Code.Gameplay.DrawingPath
{
  public class PathObject : MonoBehaviour
  {
    private LineRenderer _lineRenderer;
    private int _currentPosition;
    private void Awake() =>
      _lineRenderer = GetComponent<LineRenderer>();

    public void AddPosition(Vector3 position)
    {
      _lineRenderer.positionCount++;
      _lineRenderer.SetPosition(LastPositionIndex(), position);
    }

    public Vector3 NextPosition() => 
      _lineRenderer.GetPosition(_currentPosition++);

    public Vector3 LastPosition() => 
      _lineRenderer.GetPosition(LastPositionIndex());

    private int LastPositionIndex() =>
      _lineRenderer.positionCount - 1;
  }
}