using System;
using UnityEngine;

namespace Code.Gameplay.DrawingPath
{
  public class PathActor : MonoBehaviour
  {
    [SerializeField]
    private GenderType _genderType;

    private PathObject _pathObject;

    public GenderType GenderType => _genderType;
    public event Action PathFinished;

    public bool HasFinishedPath => _pathObject != null;
    public PathObject PathObject => _pathObject;


    public void FinishPath(PathObject pathObject)
    {
      _pathObject = pathObject;
      PathFinished?.Invoke();
    }
  }
}