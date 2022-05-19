using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.XR.Interaction.Toolkit;

public class FishMovement : MonoBehaviour
{
    [SerializeField]
    public Transform activeTarget;

    [SerializeField]
    private GameObject currentTargetVisualizer;

    [SerializeField]
    private Transform lookAtTransform;

    [SerializeField]
    private Transform spawnPosition;
    [SerializeField]
    private float spawnRadius;

    [SerializeField]
    private Transform zeroTarget;

    [SerializeField]
    private float speed;
    [SerializeField]
    private float minDistance;

    private bool randomFlight = true;

    [SerializeField]
    private GameObject[] breads;

    [SerializeField]
    private float fishYOffset;

    public List<GameObject> nextTargets = new List<GameObject>();

    private void Start()
    {
        DeactivateBreadGrabInteractable();

        ChangeTarget(RandomTarget(this.transform));
    }

    void FixedUpdate()
    {
        float dist = Vector3.Distance(transform.position, activeTarget.position);

        if (dist > minDistance)
        {
            FlyToTarget(dist);
        }
        else if (dist <= minDistance)
        {
            if(activeTarget.tag == "bread")
            {
                StartCoroutine(DestroyTarget(activeTarget.gameObject));
            }

            if(nextTargets.Count != 0)
            {
                ChangeTarget(nextTargets[0].transform);
                nextTargets.RemoveAt(0);
            }
            else
            {
                ChangeTarget(RandomTarget(this.transform));
            }
        }
    }

    IEnumerator DestroyTarget(GameObject target)
    {
        yield return new WaitForSeconds(0.2f);
        Destroy(target.gameObject);
    }

    public void ChangeTarget(Transform newTarget)
    {
        activeTarget = newTarget;
    }

    private void FlyToTarget(float distance)
    {
        //animator.SetBool("active", true);

        // Rotation //
        // Offset fuer das LookAt Target (zwischen -1 und 1)
        float xOffset = map(Mathf.PerlinNoise(0.1f, Time.realtimeSinceStartup / 1) * distance, 0, 6, 0, 1);
        float yOffset = map(Mathf.PerlinNoise(0.3f, Time.realtimeSinceStartup / 1) * distance, 0, 6, 0, 1);
        float zOffset = map(Mathf.PerlinNoise(0.8f, Time.realtimeSinceStartup / 1) * distance, 0, 6, 0, 1);

        // Transform der immer genau auf das Target rotiert ist (plus einem kleinen Offset)
        lookAtTransform.LookAt(activeTarget.position + new Vector3(xOffset, yOffset + fishYOffset, zOffset));

        // Drehungswinkel ist abhaengig von der Distanz zum Target (zwischen 0.3 und 1)
        float degreeDelta = map(0.4f / distance, 0, 1, 0.3f, 3);

        // Schmetterling wird langsam in Richtung Target bewegt
        transform.rotation = Quaternion.RotateTowards(transform.rotation, lookAtTransform.rotation, degreeDelta);


        // Speed //
        // zu der Geschwindigkeit wird etwas Noise addiert
        float newSpeed = map(Mathf.PerlinNoise(0.1f, Time.realtimeSinceStartup / 10), 0, 1, 0, speed);

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
        float targetY = Random.Range(-1f, 1f);
        float targetZ = Random.Range(-3f, 3f);

        zeroTarget.position = currentPosition.position + new Vector3(targetX, targetY, targetZ);

        float distanceToSpawnPoint = Vector3.Distance(zeroTarget.position, spawnPosition.position);

        if (distanceToSpawnPoint < spawnRadius && zeroTarget.position.y <= spawnPosition.position.y && zeroTarget.position.y >= spawnPosition.position.y - 0.1f)
        {
            currentTargetVisualizer.transform.position = zeroTarget.position;
            return zeroTarget;
        }
        else
        {
            return RandomTarget(currentPosition);
        }
    }

    public void ActivateBreadGrabInteractable()
    {
        foreach(GameObject b in breads)
        {
            b.GetComponent<XRGrabInteractable>().enabled = true;
        }
    }

    public void DeactivateBreadGrabInteractable()
    {
        foreach (GameObject b in breads)
        {
            b.GetComponent<XRGrabInteractable>().enabled = false;
        }
    }

    private bool CheckNextTargets(GameObject target)
    {
        foreach(GameObject t in nextTargets)
        {
            if(t == target)
            {
                return false;
            }
        }

        return true;
    }

    public void AddToNextTargets(GameObject target)
    {
        if (CheckNextTargets(target))
        {
            nextTargets.Add(target);
        }
    }

    public float map(float s, float a1, float a2, float b1, float b2)
    {
        return b1 + (s - a1) * (b2 - b1) / (a2 - a1);
    }
}

