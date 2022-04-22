using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportPlayer : MonoBehaviour
{
    [SerializeField]
    private GameObject xrOrigin;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void TeleportPlayerToRoom(Transform newPosition) 
    {
        xrOrigin.transform.position = newPosition.position;
        xrOrigin.transform.rotation = newPosition.rotation;
    }
}
