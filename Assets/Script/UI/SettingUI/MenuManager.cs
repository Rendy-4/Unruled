using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
public class MenuManager : MonoBehaviour
{
    /*public TMP_Text panelName;*/
    [Header("Menu Buttons")]
    [SerializeField] private Button newGameButton;
    [SerializeField] private Button loadGameButton;

    private void Start() {
        if(!DataPresistenceManager.instance.HasGameData() || !DataPresistenceManager.instance.GetGameData().HasStartedGame)
        {
            loadGameButton.interactable = false;
        }
        else
        {
            loadGameButton.interactable = true;
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
