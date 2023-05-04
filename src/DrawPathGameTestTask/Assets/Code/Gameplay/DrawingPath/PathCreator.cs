using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Code.Gameplay.DrawingPath
{
  public class PathCreator : MonoBehaviour
  {
    private const double PixelPositionEpsilon = 0.01;
    private const string FireButtonName = "Fire1";
    private const string PathStartLayerName = "PathStart";
    private const string PathEndLayerName = "PathEnd";

    [SerializeField]
    private List<PathActor> _pathActors;

    [SerializeField]
    private PathObject PathObjectPrefab;

    [SerializeField]
    private SimulationObserver _simulationObserver;

    private PathObject _pathObject;
    private Camera _camera;
    private Vector2 _previousPosition;
    private bool _nowDrawing;
    private PathActor _pathActor;

    private void Awake() =>
      _camera = Camera.main;

    private void Start()
    {
      foreach (PathActor pathActor in _pathActors)
        pathActor.PathFinished += OnPathFinished;
    }

    private void Update() => 
      PathCreating();

    private void OnDestroy()
    {
      foreach (PathActor pathActor in _pathActors)
        pathActor.PathFinished -= OnPathFinished;
    }

    private void OnPathFinished()
    {
      if(AllPathsFinished())
        _simulationObserver.StartSimulation();
    }

    private bool AllPathsFinished() =>
      _pathActors.All(pathStart => pathStart.HasFinishedPath);

    private void PathCreating()
    {
      Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

      if (Input.GetButtonDown(FireButtonName) && IsPathStart(ray))
        StartDrawing();

      if (!_nowDrawing)
        return;

      if (Input.GetButton(FireButtonName))
        Draw(ray.origin);

      if (Input.GetButtonUp(FireButtonName)) 
        FinishDrawing(ray);
    }

    private void StartDrawing() => 
      _nowDrawing = true;

    private bool IsPathStart(Ray ray)
    {
      RaycastHit2D[] hits = new RaycastHit2D[10];
      var contactFilter2D = new ContactFilter2D();
      contactFilter2D.SetLayerMask(1 << LayerMask.NameToLayer(PathStartLayerName));
      int raycastHitsCount = Physics2D.Raycast(ray.origin, ray.direction, contactFilter2D, hits);
      for (var index = 0; index < raycastHitsCount; index++)
      {
        RaycastHit2D raycastHit2D = hits[index];
        if (!raycastHit2D.transform.TryGetComponent(out PathActor pathActor))
          continue;

        if (pathActor.HasFinishedPath)
          continue;

        _pathActor = pathActor;
        return true;
      }

      return false;
    }


    private void Draw(Vector2 position)
    {
      if (_pathObject == null)
        _pathObject = Instantiate(PathObjectPrefab);
      else
      {
        if (Vector2.Distance(position, _previousPosition) > PixelPositionEpsilon)
          _pathObject.AddPosition(position);
        _previousPosition = position;
      }
    }

    private void FinishDrawing(Ray ray)
    {
      if (IsPathEnd(ray))
      {
        _pathActor.FinishPath(_pathObject);
        _pathObject = null;
      }
      else
        Destroy(_pathObject.gameObject);

      _nowDrawing = false;
    }

    private bool IsPathEnd(Ray ray)
    {
      RaycastHit2D[] hits = new RaycastHit2D[10];
      var contactFilter2D = new ContactFilter2D();
      contactFilter2D.SetLayerMask(1 << LayerMask.NameToLayer(PathEndLayerName));

      int raycastHitsCount = Physics2D.Raycast(ray.origin, ray.direction, contactFilter2D, hits);

      for (var index = 0; index < raycastHitsCount; index++)
      {
        RaycastHit2D raycastHit2D = hits[index];
        if (!raycastHit2D.collider.TryGetComponent(out PathEnd pathEnd))
          continue;

        if (PathEndNotSatisfyGender(pathEnd))
          continue;

        return true;
      }

      return false;
    }

    private bool PathEndNotSatisfyGender(PathEnd pathEnd) =>
      pathEnd.GenderTypes.All(pathEndGenderType => pathEndGenderType != _pathActor.GenderType);
  }
}