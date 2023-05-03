using UnityEngine;

namespace Code.Gameplay.DrawingPath
{
  public class PathStart : MonoBehaviour
  {
    [SerializeField]
    private GenderType _genderType;

    public GenderType GenderType => _genderType;
    public PathObject PathObject { get; set; }
    public bool SimulationFinished { get; set; }
  }
}