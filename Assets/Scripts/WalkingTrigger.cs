using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingTrigger : MonoBehaviour
{
    [SerializeField]
    private WalkingController walkingController;

    private bool active = false;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "MainCamera" && active)
        {
            //Debug.Log("Player crossed");

            walkingController.ResetCurveSpeed();
        }
    }

    public void SetActive()
    {
        active = true;
    }
}
