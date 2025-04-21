using UnityEngine;

public class OpenURL : MonoBehaviour
{
    public string url = "https://blockly.games/";

    public void OpenWebsite()
    {
        Debug.Log("Opening URL: " + url);
        Application.OpenURL(url);
    }
}
