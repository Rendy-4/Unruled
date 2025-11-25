using UnityEngine;

public class ShowPath : MonoBehaviour
{
    void Start()
    {
        Debug.Log("PATH = " + Application.persistentDataPath);
    }
}
