using System.Collections.Generic;
using System.Linq;
using Code.Gameplay.DrawingPath;
using UnityEngine;

namespace Code.Gameplay
{
  public class Simulation : MonoBehaviour
  {
    public List<DrawStart> PathStarts;
    private void Update()
    {
      if (!ShouldStartSimulation())
        return;
      
      foreach (DrawStart pathStart in PathStarts)
      {
        if (pathStart.SimulationFinished)
          continue;
          
        Vector3 nextPosition = pathStart.PathObject.NextPosition();
        Vector2 translation = nextPosition - pathStart.transform.position;
        pathStart.transform.Translate(translation);
        if (nextPosition == pathStart.PathObject.LastPosition())
          pathStart.SimulationFinished = true;
      }

    }

    public bool ShouldStartSimulation() => 
        PathStarts.All(start => start.PathObject != null);
  }
}