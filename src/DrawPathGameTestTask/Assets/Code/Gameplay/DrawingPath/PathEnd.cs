using System.Collections.Generic;
using UnityEngine;

namespace Code.Gameplay.DrawingPath
{
  public class PathEnd : MonoBehaviour
  {
    [SerializeField]
    private List<GenderType> _genderTypes;

    public List<GenderType> GenderTypes => _genderTypes;
  }
}