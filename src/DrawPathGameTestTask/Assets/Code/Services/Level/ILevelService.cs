namespace Code.Services.Level
{
  public interface ILevelService : IService
  {
    string NextLevelName();
    string CurrentLevelName();
    bool HasNextLevel();
  }
}