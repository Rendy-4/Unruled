using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;

public class MenuManager : MonoBehaviour
{
    [Header("Menu Buttons")]
    [SerializeField] private Button newGameButton;
    [SerializeField] private Button loadGameButton;

    private void Start()
    {

        if(newGameButton == null && loadGameButton == null)
        return;

        string fullPath = Path.Combine(Application.persistentDataPath, DataPresistenceManager.instance.fileName);

        if (loadGameButton != null)
        loadGameButton.interactable = File.Exists(fullPath);
    }

    public void OnNewGameClicked()
    {
        
            DataPresistenceManager.instance.NewGame();

        SceneManager.LoadSceneAsync("Sekolah");
    }

    public void OnLoadGameClicked()
    {
        SceneManager.LoadSceneAsync("Sekolah");
    }

    public void ToMainMenu()
    {
        DataPresistenceManager.instance.SaveGame();
        SceneManager.LoadSceneAsync("Main Menu");
    }
    public void exitGame()
    {
        DataPresistenceManager.instance.SaveGame();
        Application.Quit();
    }

    
}
