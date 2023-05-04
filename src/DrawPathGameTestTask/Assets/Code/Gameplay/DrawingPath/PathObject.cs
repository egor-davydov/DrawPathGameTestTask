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

    public float Length()
    {
      float length = 0;
      for (int i = 0; i < _lineRenderer.positionCount - 1; i++) 
        length += Vector2.Distance(_lineRenderer.GetPosition(i), _lineRenderer.GetPosition(i + 1));
      return length;
    }

    public Vector3 CurrentPosition() =>
      _lineRenderer.GetPosition(_currentPosition);

    public Vector3 NextPosition() =>
      _lineRenderer.GetPosition(_currentPosition+1);

    public void MoveNextPosition() =>
      _currentPosition++;

    public Vector3 LastPosition() =>
      _lineRenderer.GetPosition(LastPositionIndex());

    private int LastPositionIndex() =>
      _lineRenderer.positionCount - 1;

    public float DistanceToNextPosition() => 
      Vector2.Distance(CurrentPosition(), NextPosition());
  }
}