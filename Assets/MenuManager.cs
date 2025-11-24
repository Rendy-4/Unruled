using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
public class MenuManager : MonoBehaviour
{
    public TMP_Text panelName;
    [Header("Menu Buttons")]
    [SerializeField] private Button newGameButton;
    [SerializeField] private Button loadGameButton;

    private void Start() {
        if(!DataPresistenceManager.instance.HasGameData())
        {
            loadGameButton.interactable = false;
        }
    }
    public void OnNewGameClicked()
    {
        DataPresistenceManager.instance.NewGame();
        SceneManager.LoadSceneAsync("In Game2");
    }
    public void OnLoadGameClicked()
    {
        SceneManager.LoadSceneAsync("In Game2");
    }

    public void exitGame()
    {
        Application.Quit();
        Debug.Log("User Telah Keluar");
    }

    public void setPanelName(string name)
    {
        panelName.text = name;
    }
}
