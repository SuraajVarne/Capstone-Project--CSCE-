using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 // Add the XR Interaction Toolkit namespace

public class CubeInteraction : MonoBehaviour
{
    public UnityEngine.XR.Interaction.Toolkit.Interactors.XRRayInteractor leftRayInteractor;  // Reference for left hand ray
    public UnityEngine.XR.Interaction.Toolkit.Interactors.XRRayInteractor rightRayInteractor; // Reference for right hand ray

    void Start()
    {
        // Ensure ray interactors are assigned correctly in the Inspector
        if (leftRayInteractor == null || rightRayInteractor == null)
        {
            Debug.LogError("XRRayInteractor is not assigned on one of the controllers.");
        }
    }

    void Update()
    {
        RaycastHit hit;  // Declare the RaycastHit variable

        // Check if the left ray interactor hit the cube
        if (leftRayInteractor && leftRayInteractor.TryGetHitInfo(out hit))
        {
            if (hit.transform == this.transform) // If the ray hit the Cube
            {
                Debug.Log("Cube clicked with left hand!");
                GetComponent<Renderer>().material.color = Color.red;  // Optionally, change color
            }
        }

        // Check if the right ray interactor hit the cube
        if (rightRayInteractor && rightRayInteractor.TryGetHitInfo(out hit))
        {
            if (hit.transform == this.transform) // If the ray hit the Cube
            {
                Debug.Log("Cube clicked with right hand!");
                GetComponent<Renderer>().material.color = Color.blue;  // Optionally, change color
            }
        }
    }
}
