using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetOnRelease : MonoBehaviour
{
    public void Reset()
    {
        gameObject.transform.localPosition = Vector3.zero;
        gameObject.transform.localRotation = Quaternion.identity;
        gameObject.transform.localScale = Vector3.one;
    }
}
