using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireAnimationController : MonoBehaviour
{
    [SerializeField]
    private GameObject[] particleSystems;

    public void ActivateParticleSystems()
    {
        foreach(GameObject ps in particleSystems)
        {
            ps.GetComponentInChildren<ParticleSystem>().Play();
        }
    } 
}
