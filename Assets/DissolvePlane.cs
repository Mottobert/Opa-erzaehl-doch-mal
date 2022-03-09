using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
[ExecuteInEditMode]
public class DissolvePlane : MonoBehaviour
{
    [SerializeField]
    private Transform dissolvePoint;
    [SerializeField]
    private GameObject dissolveMaterial;

    void Update()
    {
        dissolveMaterial.GetComponent<MeshRenderer>().material.SetVector("Location", dissolvePoint.transform.position);
    }
}