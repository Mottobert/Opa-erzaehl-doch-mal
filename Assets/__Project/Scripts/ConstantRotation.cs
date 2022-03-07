using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstantRotation : MonoBehaviour
{
    [SerializeField] private float Speed;
    void Update()
    {
        this.transform.Rotate(0, -Speed*Time.deltaTime, 0);
    }
}
