using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Butterfly : MonoBehaviour
{
    [SerializeField]
    private Transform target;

    [SerializeField]
    private Transform target2;

    [SerializeField]
    private Animator animator;

    [SerializeField]
    private float speed;

    // Update is called once per frame
    void Update()
    {
        //float noise = Mathf.PerlinNoise(0.1f, Time.frameCount / 10) * 0.1f;

        if(Vector3.Distance(transform.position, target.position) > 0.1f)
        {
            animator.SetBool("active", true);
            transform.position = Vector3.MoveTowards(transform.position, target.position, speed);
            transform.LookAt(target);
        }
        else
        {
            animator.SetBool("active", false);
            transform.position = target.position;
            transform.rotation = target.rotation;
        }
    }

    public void ChangeTarget()
    {
        target = target2;
    }
}

