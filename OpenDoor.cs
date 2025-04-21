using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    public Transform door;
    public float openAngle = 90f;
    public float openSpeed = 2f;
    private bool isOpen = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            isOpen = !isOpen;
        }

        if (isOpen)
        {
            Quaternion targetRotation = Quaternion.Euler(0, openAngle, 0);
            door.rotation = Quaternion.Slerp(door.rotation, targetRotation, Time.deltaTime * openSpeed);
        }
    }
}

