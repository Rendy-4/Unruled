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
        string fullPath = Path.Combine(Application.persistentDataPath, DataPresistenceManager.instance.fileName);

        if (File.Exists(fullPath))
        {
            loadGameButton.interactable = true;
        }
        else
        {
            loadGameButton.interactable = false;
        }
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

    public void exitGame()
    {
        Application.Quit();
    }
}
