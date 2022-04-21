using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class AttachPlayerToRope : MonoBehaviour
{
    [SerializeField]
    private GameObject xrOrigin;

    [SerializeField]
    private PlayableDirector helicopterFlyAwayTimeline;

    public void AttachPlayer()
    {
        xrOrigin.transform.parent = this.transform;
        helicopterFlyAwayTimeline.Play();
    }

    public void DetachPlayer()
    {
        xrOrigin.transform.parent = null;
    }
}
