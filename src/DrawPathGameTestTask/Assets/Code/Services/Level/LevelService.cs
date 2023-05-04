using UnityEngine.SceneManagement;

namespace Code.Services.Level
{
  public class LevelService : ILevelService
  {
    public string NextLevelName() => 
      SceneManager.GetSceneByBuildIndex(CurrentLevel().buildIndex + 1).name;

    public string CurrentLevelName() =>
      CurrentLevel().name;

    private Scene CurrentLevel() =>
      SceneManager.GetActiveScene();
  }
}