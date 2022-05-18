using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishEating : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "bread")
        {
            other.gameObject.GetComponentInChildren<MeshRenderer>().gameObject.SetActive(false);
        }
    }
}
