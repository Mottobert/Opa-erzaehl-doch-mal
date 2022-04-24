using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waterpump : MonoBehaviour
{
    [SerializeField]
    private Waterhose waterhose;

    private Vector3 startPosition;

    private bool active = true;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(gameObject.transform.position.y < startPosition.y - 0.125f && active)
        {
            if(waterhose.waterAmount < 16)
            {
                waterhose.waterAmount++;
            }
            
            active = false;
        }

        if(gameObject.transform.position.y > startPosition.y)
        {
            active = true;
        }
    }
}
