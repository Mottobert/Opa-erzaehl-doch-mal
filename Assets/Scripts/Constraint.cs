using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Constraint : MonoBehaviour
{
    [SerializeField]
    private Transform constrainedObject;
    [SerializeField]
    private Transform targetObject;
    [SerializeField]
    private Vector3 targetPositionOffset;
    [SerializeField]
    private Vector3 targetRotationOffset;

    // Update is called once per frame
    void FixedUpdate()
    {
        //constrainedObject.position = targetObject.position;
        //constrainedObject.rotation = targetObject.rotation;

        constrainedObject.position = targetObject.TransformPoint(targetPositionOffset);
        constrainedObject.rotation = targetObject.rotation * Quaternion.Euler(targetRotationOffset);
    }
}
