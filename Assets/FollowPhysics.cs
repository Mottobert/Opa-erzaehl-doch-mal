using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPhysics : MonoBehaviour
{
    [SerializeField]
    private Transform target;
    private Rigidbody rb;

    private Vector3 startPosition;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        startPosition = gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (target.transform.position.y < startPosition.y + 0.1f && target.transform.position.y > startPosition.y - 0.3f)
        {   
            rb.MovePosition(target.transform.position);
        }
        
        if(target.transform.position.y > startPosition.y + 0.1f)
        {
            gameObject.transform.position = new Vector3(gameObject.transform.position.x, startPosition.y + 0.1f, gameObject.transform.position.z);
        }

        if (target.transform.position.y < startPosition.y - 0.3f)
        {
            gameObject.transform.position = new Vector3(gameObject.transform.position.x, startPosition.y - 0.3f, gameObject.transform.position.z);
        }
    }
}
