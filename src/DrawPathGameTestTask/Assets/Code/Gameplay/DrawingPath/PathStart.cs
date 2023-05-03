using UnityEngine;

namespace Code.Gameplay.DrawingPath
{
  public class PathStart : MonoBehaviour
  {
    [SerializeField]
    private GenderType _genderType;

    [SerializeField]
    private Simulation _simulation;

    public GenderType GenderType => _genderType;
    public PathObject PathObject { get; set; }
    public bool SimulationFinished { get; set; }

    private void OnCollisionEnter2D(Collision2D col)
    {
      _simulation.StopSimulation();
      Debug.Log("Lose");
    }
  }
}