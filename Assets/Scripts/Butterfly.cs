using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class Butterfly : MonoBehaviour
{
    [SerializeField]
    private Transform activeTarget;

    [SerializeField]
    private GameObject currentTargetVisualizer;

    [SerializeField]
    private Transform playerTarget;

    [SerializeField]
    private Transform lookAtTransform;

    [SerializeField]
    private Transform spawnPosition;
    [SerializeField]
    private float spawnRadius;

    [SerializeField]
    private Transform zeroTarget;

    [SerializeField]
    private Animator animator;

    [SerializeField]
    private float speed;
    [SerializeField]
    private float minDistance;

    private bool randomFlight = true;
    private bool handTarget = true;

    private bool activateHandFlight = false;

    private Vector3 oldPos;
    private Vector3 newPos;
    public Vector3 velocity;

    private bool butterflySatOnHand = false;

    [SerializeField]
    private PlayableDirector butterflySatOnHandTimeline;

    [SerializeField]
    public bool treeButterfly;

    private bool treeButterflyActive = false;

    public List<Transform> nextTargets = new List<Transform>();

    private int nextTargetTimer = 0;

    [SerializeField]
    private WalkingController walkingController;
    [SerializeField]
    private GameObject endButton;

    private void Start()
    {
        oldPos = activeTarget.transform.position;

        if (!treeButterfly)
        {
            ChangeTarget(RandomTarget(this.transform));
        }
        else
        {
            ChangeTarget(nextTargets[0]);
            nextTargets.RemoveAt(0);
            treeButterflyActive = true;
        }

        //ActivateHandFlight();
    }

    void FixedUpdate()
    {
        newPos = activeTarget.transform.position;
        velocity = (newPos - oldPos) / Time.deltaTime;
        oldPos = newPos;

        float dist = Vector3.Distance(transform.position, activeTarget.position);

        if (dist > minDistance)
        {
            FlyToTarget(dist);
        }
        else if(dist <= minDistance)
        {
            if (treeButterflyActive && nextTargets.Count != 0 && nextTargetTimer <= 0)
            {
                ChangeTarget(nextTargets[0]);
                nextTargets.RemoveAt(0);

                nextTargetTimer = 100;
            }

            if(treeButterflyActive && nextTargets.Count == 0 && nextTargetTimer <= 0)
            {
                animator.SetBool("active", false);
                transform.position = activeTarget.position;
                transform.rotation = activeTarget.rotation;

                StartCoroutine(DeactivateTreeButterflyDelay());
            }

            if (randomFlight && !treeButterflyActive)
            {
                ChangeTarget(RandomTarget(this.transform));
            }
            else if(!treeButterflyActive && handTarget)
            {
                if (!butterflySatOnHand && !treeButterfly)
                {
                    butterflySatOnHandTimeline.Play();
                    walkingController.OpaWalkAgain();
                    walkingController.StopWalking();
                    Invoke("ResetOpaWalking", 10f);
                    butterflySatOnHand = true;
                }

                if (!butterflySatOnHand && treeButterfly)
                {
                    butterflySatOnHand = true;
                    Invoke("ShowEndButton", 2f);
                }
                
                animator.SetBool("active", false);
                transform.position = activeTarget.position;
                transform.rotation = activeTarget.rotation;
            }
        }

        if (!randomFlight && velocity.magnitude > 1f && handTarget && butterflySatOnHand)
        {
            handTarget = false;
            StopAllCoroutines();
            StartCoroutine(DelayBeforeHandTarget());
            ActivateRandomFlight();
        }

        if(nextTargetTimer > 0)
        {
            nextTargetTimer--;
        }
    }

    private void ShowEndButton()
    {
        endButton.SetActive(true);
    }

    IEnumerator DelayBeforeHandTarget()
    {
        yield return new WaitForSeconds(3f);
        handTarget = true;
    }

    IEnumerator DeactivateTreeButterflyDelay()
    {
        yield return new WaitForSeconds(4f);
        treeButterflyActive = false;

        ChangeTarget(playerTarget);
        activateHandFlight = true;
        randomFlight = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (handTarget && other.gameObject.tag == "butterflyTarget" && activeTarget.tag != "butterflyTarget" && activateHandFlight && butterflySatOnHand)
        {
            StopCoroutine(FlyToHand());
            StartCoroutine("ActivateTargetFlightDelay", other.transform);
            //ActivatePlayerTargetFlight();
        }
    }

    public void ActivateHandFlight()
    {
        activateHandFlight = true;
        StartCoroutine(FlyToHand());
    }

    IEnumerator FlyToHand()
    {
        yield return new WaitForSeconds(3);
        ActivatePlayerTargetFlight(playerTarget);
    }

    private void ChangeTarget(Transform newTarget)
    {
        activeTarget = newTarget;
    }

    private void FlyToTarget(float distance)
    {
        animator.SetBool("active", true);

        // Rotation //
        // Offset fuer das LookAt Target (zwischen 0 und 1)
        float xOffset = map(Mathf.PerlinNoise(0.1f, Time.realtimeSinceStartup / 1) * distance, 0, 6, 0, 1);
        float yOffset = map(Mathf.PerlinNoise(0.3f, Time.realtimeSinceStartup / 1) * distance, 0, 6, 0, 1);
        float zOffset = map(Mathf.PerlinNoise(0.8f, Time.realtimeSinceStartup / 1) * distance, 0, 6, 0, 1);

        // Transform der immer genau auf das Target rotiert ist (plus einem kleinen Offset)
        lookAtTransform.LookAt(activeTarget.position + new Vector3(xOffset, yOffset, zOffset));

        // Drehungswinkel ist abhaengig von der Distanz zum Target (zwischen 0.3 und 1)
        float degreeDelta = map(0.4f / distance, 0, 1, 0.3f, 3);

        // Schmetterling wird langsam in Richtung Target bewegt
        transform.rotation = Quaternion.RotateTowards(transform.rotation, lookAtTransform.rotation, degreeDelta);


        // Speed //
        // zu der Geschwindigkeit wird etwas Noise addiert
        float newSpeed = map(Mathf.PerlinNoise(0.1f, Time.realtimeSinceStartup/10), 0, 1, 0, speed);

        // Wenn der Schmetterling nah an einem Target Point ist, wird er langsamer
        if (distance < 1f)
        {
            newSpeed = newSpeed * (distance + 0.3f);
        }

        // Schmetterling fliegt die ganze Zeit nur nach vorne mit definierter Geschwindigkeit
        transform.position = transform.position + (transform.forward * newSpeed);
    }

    private Transform RandomTarget(Transform currentPosition)
    {
        float targetX = Random.Range(-3f, 3f);
        float targetY = Random.Range(-3f, 3f);
        float targetZ = Random.Range(-3f, 3f);

        zeroTarget.position = currentPosition.position + new Vector3(targetX, targetY, targetZ);

        float distanceToSpawnPoint = Vector3.Distance(zeroTarget.position, spawnPosition.position);

        if (distanceToSpawnPoint < spawnRadius && zeroTarget.position.y >= spawnPosition.position.y)
        {
            currentTargetVisualizer.transform.position = zeroTarget.position;
            return zeroTarget;
        }
        else
        {
            return RandomTarget(currentPosition);
        }
    }

    IEnumerator ActivateTargetFlightDelay(Transform newTarget)
    {
        yield return new WaitForSeconds(0.4f);
        ActivatePlayerTargetFlight(newTarget);
    }

    private void ActivateRandomFlight()
    {
        randomFlight = true;
        ChangeTarget(RandomTarget(this.transform));
    }

    private void ActivatePlayerTargetFlight(Transform newTarget)
    {
        randomFlight = false;
        ChangeTarget(newTarget);
        oldPos = newTarget.transform.position;
    }

    private void ResetOpaWalking()
    {
        walkingController.ResetCurveSpeed();
    }

    public float map(float s, float a1, float a2, float b1, float b2)
    {
        return b1 + (s - a1) * (b2 - b1) / (a2 - a1);
    }
}

