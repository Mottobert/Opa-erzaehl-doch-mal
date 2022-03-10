using Ignis;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    [SerializeField]
    private GameObject[] fireAnimationControllers;
    [SerializeField]
    private GameObject[] sphereIgniters;

    private void Start()
    {
        StartCoroutine(ChangeFogDensity(0.01f));
    }

    public void ActivateBurning()
    {
        ActivateFlammableObjects();
        ActivateFireAnimationControllers();

        StartCoroutine(ChangeFogDensity(0.01f));
    }

    private void ActivateFlammableObjects()
    {
        StartCoroutine(ActivateFlammableObjectsDelay(2, 5));
    }

    IEnumerator ActivateFlammableObjectsDelay(float minDelay, float maxDelay)
    {
        foreach (GameObject si in sphereIgniters)
        {
            si.SetActive(true);
            yield return new WaitForSeconds(Random.Range(minDelay, maxDelay));
        }
    }

    private void ActivateFireAnimationControllers()
    {
        StartCoroutine(ActivateFireAnimationControllersDelay(2, 5));
    }

    IEnumerator ActivateFireAnimationControllersDelay(float minDelay, float maxDelay)
    {
        foreach (GameObject fac in fireAnimationControllers)
        {
            fac.GetComponent<FireAnimationController>().ActivateParticleSystems();
            yield return new WaitForSeconds(Random.Range(minDelay, maxDelay));
        }
    }

    IEnumerator ChangeFogDensity(float maxValue)
    {
        float density = 0;

        while(density < maxValue)
        {
            RenderSettings.fogDensity = density + 0.00001f;
            yield return new WaitForSeconds(0.01f);
        }
    }
}
