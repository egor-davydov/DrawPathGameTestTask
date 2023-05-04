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

    public string CurrentLevelName() =>
      CurrentLevel().name;

    public void LoadAllLevels()
    {
      for (int i = 1; i < SceneManager.sceneCountInBuildSettings; i++)
        SceneManager.LoadScene(i);
    }
    
    private Scene CurrentLevel() =>
      SceneManager.GetActiveScene();
  }
}