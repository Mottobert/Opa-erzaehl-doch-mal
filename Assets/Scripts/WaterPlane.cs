using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterPlane : MonoBehaviour
{
    [SerializeField]
    private GameObject fish;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "bread")
        {
            fish.GetComponent<FishMovement>().ChangeTarget(collision.gameObject.transform);
        }
    }
}
