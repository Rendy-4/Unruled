using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class DataPresistenceManager : MonoBehaviour
{
    [Header("FIle Storage Config")]
    [SerializeField] private string fileName;

    private GameData gameData;
    private List<IDataPresistence> dataPresistencesObjects;
    private DataHandler dataHandler;

    public static DataPresistenceManager instance {get; private set;}

    private void Awake() 
    {
        if (instance != null)
        {
            Debug.LogWarning("Found more than one Data Presistence Manager in the scene. Destroying the newest one.");
            Destroy(this.gameObject);
            return;
        }
        instance = this;

        this.dataHandler = new DataHandler(Application.persistentDataPath, fileName);
    }

    private void Start() 
    {
        this.dataPresistencesObjects = FindAllDataPresistenceObjects();
        LoadGame();
    }
    public void NewGame()
    {
        this.gameData = new GameData();
    }
    public void LoadGame()
    {
        this.gameData = dataHandler.Load();
        if (this.gameData == null)
        {
            Debug.Log("No data was found. Initializing data to defaults.");
            NewGame();
        }

        foreach (IDataPresistence dataPresistenceObj in dataPresistencesObjects)
        {
            dataPresistenceObj.LoadData(gameData);
        }
    }

    public void SaveGame()
    {
        if (this.gameData == null)
        {
            Debug.Log("Game data is null. Cannot save game.");
            return;
        }
        foreach (IDataPresistence dataPresistenceObj in dataPresistencesObjects)
        {
            dataPresistenceObj.SaveData(ref gameData);
        }
        dataHandler.Save(gameData);
    }

    private void OnApplicationQuit() {
        SaveGame();
    }

    private List<IDataPresistence> FindAllDataPresistenceObjects()
    {
        MonoBehaviour[] objects = FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None);
        IEnumerable<IDataPresistence> dataPresistenceObjects = objects.OfType<IDataPresistence>();

        return new List<IDataPresistence>(dataPresistenceObjects);
    }
    public bool HasGameData()
    {
        return this.gameData != null;
    }
}
