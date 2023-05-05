using UnityEngine.SceneManagement;

namespace Code.Services.Level
{
  public class LevelService : ILevelService
  {
    public string NextLevelName()
    {
      string nextSceneFullPath = SceneUtility.GetScenePathByBuildIndex(CurrentLevel().buildIndex + 1);
      int indexBeforeSceneName = nextSceneFullPath.LastIndexOf('/');
      string nameWithFileType = nextSceneFullPath.Substring(indexBeforeSceneName + 1);
      int indexBeforeFileType = nameWithFileType.LastIndexOf('.');
      
      return nameWithFileType.Substring(0, indexBeforeFileType);
    }

    public bool HasNextLevel() => 
      CurrentLevel().buildIndex < SceneManager.sceneCountInBuildSettings-1;

    public string CurrentLevelName() =>
      CurrentLevel().name;

    private Scene CurrentLevel() =>
      SceneManager.GetActiveScene();
  }
}