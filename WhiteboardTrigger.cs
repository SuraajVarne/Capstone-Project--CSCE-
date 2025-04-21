using UnityEngine;

public class WhiteboardTrigger : MonoBehaviour
{
    public GameObject ballToActivate;

    void OnMouseDown()
    {
        if (ballToActivate != null)
        {
            ballToActivate.SetActive(true);
        }
    }
}
