using UnityEngine;

namespace Code.Services.AssetManagement
{
  public interface IAssetProvider : IService
  {
    GameObject Load(string assetPath);
  }
}