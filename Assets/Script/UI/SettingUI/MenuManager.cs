using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System.IO;
public class MenuManager : MonoBehaviour
{
    /*public TMP_Text panelName;*/
    [Header("Menu Buttons")]
    [SerializeField] private Button newGameButton;
    [SerializeField] private Button loadGameButton;

    [Header("Reference")]
    [SerializeField] private DataPresistenceManager dataManager;

    private void Start() {
        loadGameButton.interactable = false;

        string fullPath = Path.Combine(Application.persistentDataPath, DataPresistenceManager.instance.fileName);
        Debug.Log("Checking Save File at " +  fullPath);

        if (File.Exists(fullPath))
        {
            loadGameButton.interactable = true;
            Debug.Log("Save file Found");
        }
        else
        {
            Debug.Log("Save file Not Found");
        }
    }
    public void OnNewGameClicked()
    {
        DataPresistenceManager.instance.NewGame();
        loadGameButton.interactable = true;
        SceneManager.LoadSceneAsync("Sekolah");
    }
    public void OnLoadGameClicked()
    {
        SceneManager.LoadSceneAsync("Sekolah");
    }

    public void exitGame()
    {
        Application.Quit();
        Debug.Log("User Telah Keluar");
    }



    /*public void setPanelName(string name)
    {
        panelName.text = name;
    }*/
}
