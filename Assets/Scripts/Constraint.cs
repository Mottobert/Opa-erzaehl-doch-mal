using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Constraint : MonoBehaviour
{
    [SerializeField]
    private Transform constrainedObject;
    [SerializeField]
    private Transform sourceObject;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        constrainedObject.position = sourceObject.position;
        constrainedObject.rotation = sourceObject.rotation;
    }
}
