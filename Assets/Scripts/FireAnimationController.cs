using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireAnimationController : MonoBehaviour
{
    [SerializeField]
    private GameObject[] particleSystems;

    [SerializeField]
    private float minDelay;
    [SerializeField]
    private float maxDelay;


    public void ActivateParticleSystems()
    {
        //ps.GetComponentInChildren<ParticleSystem>().Play();
        StartCoroutine(ActivateFireDelay(minDelay, maxDelay));
    }

    IEnumerator ActivateFireDelay(float minDelay, float maxDelay)
    {
        foreach (GameObject ps in particleSystems)
        {
            ps.GetComponentInChildren<ParticleSystem>().Play();
            yield return new WaitForSeconds(Random.Range(minDelay, maxDelay));
        }
    }
}
