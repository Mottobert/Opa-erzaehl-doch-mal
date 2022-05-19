using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class WaterPlane : MonoBehaviour
{
    [SerializeField]
    private GameObject fish;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "bread")
        {
            //fish.GetComponent<FishMovement>().ChangeTarget(collision.gameObject.transform);

            fish.GetComponent<FishMovement>().AddToNextTargets(collision.gameObject);
            collision.gameObject.GetComponent<XRGrabInteractable>().enabled = false;
        }
    }
}
