using UnityEngine;

public class ShowTextOnEnter : MonoBehaviour
{
    public GameObject textObject;

    void Start()
    {
        textObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            textObject.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            textObject.SetActive(false);
        }
    }
}
