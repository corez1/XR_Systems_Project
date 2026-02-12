using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CustomGrab : MonoBehaviour
{
    CustomGrab otherHand = null;
    public List<Transform> nearObjects = new List<Transform>();
    public Transform grabbedObject = null;
    public InputActionReference action;
    bool grabbing = false;

    private Vector3 lastPosition;
    private Quaternion lastRotation;

    private void Start()
    {
        action.action.Enable();

        // Find the other hand script (sibling in hierarchy)
        foreach(CustomGrab c in transform.parent.GetComponentsInChildren<CustomGrab>())
        {
            if (c != this)
                otherHand = c;
        }

        lastPosition = transform.position;
        lastRotation = transform.rotation;
    }

    void Update()
    {
        // Calculate deltas
        Vector3 deltaPosition = transform.position - lastPosition;
        Quaternion deltaRotation = transform.rotation * Quaternion.Inverse(lastRotation);

        grabbing = action.action.IsPressed();

        if (grabbing)
        {
            // Grab logic
            if (!grabbedObject)
                grabbedObject = nearObjects.Count > 0 ? nearObjects[0] : otherHand.grabbedObject;

            if (grabbedObject)
            {
                float weight = 1.0f;
                if (otherHand != null && otherHand.grabbedObject == grabbedObject)
                {
                    weight = 0.5f;
                }

                // Rotation
                Vector3 pivotVector = grabbedObject.position - transform.position;
                Vector3 rotatedPivotVector = deltaRotation * pivotVector;
                Vector3 rotationTranslation = rotatedPivotVector - pivotVector;

                // Scaled movement
                Vector3 finalMove = (deltaPosition + rotationTranslation) * weight;
                grabbedObject.position += finalMove;

                // Apply scaled rotation
                Quaternion weightedRotation = Quaternion.Slerp(Quaternion.identity, deltaRotation, weight);
                grabbedObject.rotation = weightedRotation * grabbedObject.rotation;
            }
        }
        else if (grabbedObject)
        {
            grabbedObject = null;
        }

        lastPosition = transform.position;
        lastRotation = transform.rotation;
    }

    // Standard trigger logic
    private void OnTriggerEnter(Collider other)
    {
        Transform t = other.transform;
        if(t && t.tag.ToLower()=="grabbable")
            nearObjects.Add(t);
    }

    private void OnTriggerExit(Collider other)
    {
        Transform t = other.transform;
        if( t && t.tag.ToLower()=="grabbable")
            nearObjects.Remove(t);
    }
}