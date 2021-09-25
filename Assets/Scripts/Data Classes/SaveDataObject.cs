
/// <summary>
/// save data set blueprint
/// </summary>
[System.Serializable]
public class SaveDataObject
{
    public int levelIndex;
    public int playerScore;
    public int grenadeCount;

    public SaveDataObject(int _levelIndex, int _playerScore, int _grenadeCount)
    {
        this.levelIndex = _levelIndex;
        this.playerScore = _playerScore;
        this.grenadeCount = _grenadeCount;
    }

    public SaveDataObject(){}
}
