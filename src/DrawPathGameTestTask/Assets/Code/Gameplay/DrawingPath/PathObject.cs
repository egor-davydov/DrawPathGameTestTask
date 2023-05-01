using UnityEngine;

namespace Code.Gameplay.DrawingPath
{
  public class PathObject : MonoBehaviour
  {
    private LineRenderer _lineRenderer;

    private void Awake() =>
      _lineRenderer = GetComponent<LineRenderer>();

    public void AddPosition(Vector3 position)
    {
      _lineRenderer.positionCount++;
      _lineRenderer.SetPosition(LastPositionIndex(), position);
    }

    private int LastPositionIndex() =>
      _lineRenderer.positionCount - 1;
  }
}