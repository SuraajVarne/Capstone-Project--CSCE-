using UnityEngine;

public class ToggleVisibility : MonoBehaviour
{
    public GameObject targetPanel;

    public void TogglePanel()
    {
        if (targetPanel != null)
        {
            bool isActive = targetPanel.activeSelf;
            targetPanel.SetActive(!isActive);
            Debug.Log("Toggled panel visibility: " + !isActive);
        }
    }
}
