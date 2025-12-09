using UnityEngine;

public class EfekRedupBG : MonoBehaviour
{
    public GameObject settingsPanel;
public GameObject dimBackground;

public void OpenSettings()
{
    dimBackground.SetActive(true);
    settingsPanel.SetActive(true);
}

public void CloseSettings()
{
    settingsPanel.SetActive(false);
    dimBackground.SetActive(false);
}

}
