using UnityEngine;

public interface IDataPresistence
{
    void LoadData(GameData data);
    void SaveData(ref GameData data);
}
