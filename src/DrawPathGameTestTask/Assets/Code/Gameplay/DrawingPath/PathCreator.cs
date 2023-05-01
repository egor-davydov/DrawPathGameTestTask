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

    public PathObject PathObjectPrefab;
  
    private PathObject _pathObject;
    private Camera _camera;
    private Vector2 _previousPosition;
    private bool _nowDrawing;
    private PathStart _pathStart;

    private void Awake() => 
      _camera = Camera.main;

    private void Update()
    {
      Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
    
      if (Input.GetButtonDown(FireButtonName) && IsPathStart(ray))
        _nowDrawing = true;
    
      if (!_nowDrawing)
        return;
    
      if (Input.GetButton(FireButtonName))
        Draw(ray.origin);

      if (Input.GetButtonUp(FireButtonName))
      {
        if (IsPathEnd(ray))
          _pathObject = null;
        else
          Destroy(_pathObject.gameObject);

        _nowDrawing = false;
      }
    }

    private bool IsPathStart(Ray ray)
    {
      RaycastHit2D[] hits = new RaycastHit2D[10];
      var contactFilter2D = new ContactFilter2D();
      contactFilter2D.SetLayerMask(1 << LayerMask.NameToLayer(PathStartLayerName));
      int raycastHitsCount = Physics2D.Raycast(ray.origin, ray.direction, contactFilter2D, hits);
      for (var index = 0; index < raycastHitsCount; index++)
      {
        RaycastHit2D raycastHit2D = hits[index];
        if (!raycastHit2D.transform.TryGetComponent(out PathStart pathStart))
          continue;
      
        if(pathStart.PathCreated)
          continue;
      
        _pathStart = pathStart;
        return true;
      }

      return false;
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

        if (PathEndNotSatisfy(pathEnd))
          continue;
      
        _pathStart.PathCreated = true;
        return true;
      }

      return false;
    }

    private bool PathEndNotSatisfy(PathEnd pathEnd) => 
      pathEnd.GenderTypes.All(pathEndGenderType => pathEndGenderType != _pathStart.GenderType);

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
  }
}