// VR Movement Controller
// Team Members: Suraj Varne Sheela, [Team Member 2], [Team Member 3], [Team Member 4]
// Description: Enables smooth locomotion and teleportation in VR

using System.Collections;
using UnityEngine;
using UnityEngine.XR;

public class VRMovement : MonoBehaviour
{
    public float moveSpeed = 2.0f;
    public float rotationSpeed = 45.0f;
    public Transform head;
    public XRNode inputSource;
    private CharacterController character;
    private Vector2 inputAxis;
    
    void Start()
    {
        character = GetComponent<CharacterController>();
    }

    void Update()
    {
        InputDevice device = InputDevices.GetDeviceAtXRNode(inputSource);
        device.TryGetFeatureValue(CommonUsages.primary2DAxis, out inputAxis);

        Vector3 forward = head.forward;
        forward.y = 0;
        forward.Normalize();

        Vector3 move = forward * inputAxis.y + head.right * inputAxis.x;
        character.Move(move * moveSpeed * Time.deltaTime);

        if (device.TryGetFeatureValue(CommonUsages.secondary2DAxis, out Vector2 rotationAxis))
        {
            transform.Rotate(Vector3.up, rotationAxis.x * rotationSpeed * Time.deltaTime);
        }
    }
}
