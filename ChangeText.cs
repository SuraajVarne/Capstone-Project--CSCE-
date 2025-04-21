using UnityEngine;
using TMPro;

public class ChangeText : MonoBehaviour
{
    public TMP_Text textElement;

    public void UpdateText()
    {
        if (textElement != null)
        {
            textElement.text = "You clicked the button!";
            Debug.Log("Text updated.");
        }
    }
}
