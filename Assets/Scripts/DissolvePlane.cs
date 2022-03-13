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

    [SerializeField]
    private Shader dissolveShader;

    void Update()
    {
        //dissolveMaterial.GetComponent<MeshRenderer>().sharedMaterial.SetVector("Vector3_9102b48e333349b2867fec4b30279474", dissolvePoint.transform.position);
        Shader.SetGlobalVector("Vector3_9102b48e333349b2867fec4b30279474", dissolvePoint.transform.position);
    }
}