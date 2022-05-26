using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishEating : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "bread")
        {
            if (other)
            {
                other.gameObject.GetComponentInChildren<MeshRenderer>().gameObject.SetActive(false);
            }
        }
    }
}
