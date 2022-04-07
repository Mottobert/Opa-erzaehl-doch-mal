using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Butterfly : MonoBehaviour
{
    [SerializeField]
    private Transform activeTarget;

    [SerializeField]
    private Transform target;
    [SerializeField]
    private Transform flyAwayTarget;

    private bool flyAway = false;

    [SerializeField]
    private Transform zeroTarget;

    [SerializeField]
    private Animator animator;

    [SerializeField]
    private float speed;
    [SerializeField]
    private float minDistance;

    private bool randomFlight = false;

    private Vector3 oldPos;
    private Vector3 newPos;
    public Vector3 velocity;

    private void Start()
    {
        //StartCoroutine(StartTargetFlight());
        //ChangeTarget(RandomTarget(this.transform));

        oldPos = activeTarget.transform.position;
    }

    void FixedUpdate()
    {
        //if (!randomFlight)
        //{
        //    ActivateTargetFlight();
        //}

        newPos = activeTarget.transform.position;
        velocity = (newPos - oldPos) / Time.deltaTime;
        oldPos = newPos;

        float dist = Vector3.Distance(transform.position, activeTarget.position);

        if (dist > minDistance)
        {
            FlyToTarget(dist);
        }
        else if(!flyAway)
        {
            if (randomFlight)
            {
                ChangeTarget(RandomTarget(this.transform));
            }
            else
            {
                animator.SetBool("active", false);
                transform.position = activeTarget.position;
                transform.rotation = activeTarget.rotation;

                //Debug.Log(velocity.magnitude);

                if (velocity.magnitude > 0.5f)
                {
                    if (!randomFlight)
                    {
                        ActivateRandomFlight();
                        //StartCoroutine(ActivateTargetFlightDelay());
                        Invoke("ActivateTargetFlight", 2f);
                        //Debug.Log("random");
                    }
                }
            }
        }
    }

    public void ChangeTarget(Transform newTarget)
    {
        activeTarget = newTarget;
    }

    private void FlyToTarget(float distance)
    {
        animator.SetBool("active", true);

        float newSpeed = Mathf.Clamp(distance / speed, 0, 0.01f);
        
        transform.position = Vector3.MoveTowards(transform.position, activeTarget.position, newSpeed);
        transform.LookAt(activeTarget);
    }

    private Transform RandomTarget(Transform currentPosition)
    {
        float targetX = Random.Range(-1f, 1f);
        float targetY = Random.Range(-1f, 1f);
        float targetZ = Random.Range(-1f, 1f);

        zeroTarget.position = currentPosition.position + new Vector3(targetX, targetY, targetZ);

        //Debug.Log(newTarget.position);

        return zeroTarget;
    }

    IEnumerator ActivateTargetFlightDelay()
    {
        yield return new WaitForSeconds(3f);
        ActivateTargetFlight();
    }

    private void ActivateRandomFlight()
    {
        randomFlight = true;
        ChangeTarget(RandomTarget(this.transform));
    }

    private void ActivateTargetFlight()
    {
        randomFlight = false;
        ChangeTarget(target);
        //Debug.Log("Target");
    }

    public void ActivateFlyAway()
    {
        flyAway = true;
        ChangeTarget(flyAwayTarget);
    }

    public float map(float s, float a1, float a2, float b1, float b2)
    {
        return b1 + (s - a1) * (b2 - b1) / (a2 - a1);
    }

    // Random Target in einem bestimmten Bereich
    // Fliegt zum Target
    // Wenn in bestimmten Abstand zu Target
    // Neues Target
}

