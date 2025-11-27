using UnityEngine;
using UnityEngine.Rendering;

public class PauseMenu : MonoBehaviour
{
    public void pause()
    {
        
        Time.timeScale = 0f;
    }
    public void resume()
    {
        Time.timeScale = 1f;
    }
}
