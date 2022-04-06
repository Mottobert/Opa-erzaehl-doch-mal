using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttachPlayerToRope : MonoBehaviour
{
    [SerializeField]
    private GameObject xrOrigin;

    public void AttachPlayer()
    {
        xrOrigin.transform.parent = this.transform;
    }
}
