using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;
using System.Collections;

public class MenuManager : MonoBehaviour
{
    [Header("Menu Buttons")]
    [SerializeField] private Button newGameButton;
    [SerializeField] private Button loadGameButton;

    [Header("Transition Settings")]
    [SerializeField] private string startText = "memulai permainan...";
    [SerializeField] private float fadeDuration = 1f;
    [SerializeField] private float displayTime = 2f;

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

        StartCoroutine(TransitionAndLoad("Sekolah"));
    }

    public void OnLoadGameClicked()
    {
        DataPresistenceManager.instance.LoadGame();

        StartCoroutine(TransitionAndLoad("Sekolah"));
    }

    IEnumerator TransitionAndLoad(string sceneName)
    {
        Debug.Log("Transitioning to scene: " + sceneName);
        if (SceneOverlayUIController.Instance != null)
        {
            SceneOverlayUIController.Instance.PlaySceneText(
                startText,
                fadeDuration,
                displayTime
            );
        }
        else 
        {
            Debug.LogWarning("SceneOverlayUIController instance is null. Skipping scene text display.");
        }

        yield return new WaitForSeconds(fadeDuration);

        SceneManager.LoadSceneAsync(sceneName);
    }

    public void ToMainMenu()
    {
        Time.timeScale = 1f;
        DataPresistenceManager.instance.SaveGame();
        SceneManager.LoadSceneAsync("Main Menu");
    }
    public void exitGame()
    {
        DataPresistenceManager.instance.SaveGame();
        Application.Quit();
    }
}
