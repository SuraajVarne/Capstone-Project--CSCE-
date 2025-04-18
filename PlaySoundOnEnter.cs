using UnityEngine;

public class PlaySoundOnEnter : MonoBehaviour
{
    public AudioSource soundEffect;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            soundEffect.Play();
        }
    }
}
