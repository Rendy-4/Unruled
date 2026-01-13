using UnityEngine;

public class EfekRedupBG : MonoBehaviour
{
    public GameObject settingsPanel;
    public GameObject Creditpanel;
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
public void CloseSettingswithoutDimBG()
{
    settingsPanel.SetActive(false);
}
public void OpenCredits()
{
    dimBackground.SetActive(true);
    Creditpanel.SetActive(true);
}

public void CloseCredits()
{
    Creditpanel.SetActive(false);
    dimBackground.SetActive(false);
}
public void SedangRedupOnly(){
    dimBackground.SetActive(true);
}
public void TidakRedupOnly(){
    dimBackground.SetActive(false);
}

public void CloseSettingsPanel(){
    settingsPanel.SetActive(false);
}


}
