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
/*************  ✨ Windsurf Command ⭐  *************/
/// <summary>
/// Enable dim background only for redup mode
/// </summary>
/*******  b38fe17e-863d-45e7-8eb4-f52f9759be18  *******/
public void SedangRedupOnly(){
    dimBackground.SetActive(true);
}
public void TidakRedupOnly(){
    dimBackground.SetActive(false);
}

}
