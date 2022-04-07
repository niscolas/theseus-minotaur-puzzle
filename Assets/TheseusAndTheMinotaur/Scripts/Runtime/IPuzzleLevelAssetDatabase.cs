namespace TheseusAndTheMinotaur
{
    public interface IPuzzleLevelAssetDatabase
    {
        string GetCurrentLevelComment();
        bool CheckHasNextLevel();
        bool CheckHasPreviousLevel();
        int GetLevelCount();
    }
}