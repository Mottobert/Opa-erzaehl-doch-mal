using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class AttachPlayerToRope : MonoBehaviour
{
    [SerializeField]
    private GameObject xrOrigin;

    [SerializeField]
    private PlayableDirector helicopterFlyAwayTimelineHoehenangst;

    [SerializeField]
    private PlayableDirector helicopterFlyAwayTimelineNoHoehenangst;

    [SerializeField]
    private bool hoehenangst;

    public bool active = false;

    public void AttachPlayer()
    {
        AttachPlayerToHelicopter();
        active = true;

        if (hoehenangst)
        {
            helicopterFlyAwayTimelineHoehenangst.Play();
        }
        else
        {
            helicopterFlyAwayTimelineNoHoehenangst.Play();
        }
    }

    public void AttachPlayerToHelicopter()
    {
        active = true;
        xrOrigin.transform.parent = this.transform;
    }

    public void DetachPlayer()
    {
        xrOrigin.transform.parent = null;
        active = false;
    }

    public void SetHoehenangst(bool angst)
    {
        hoehenangst = angst;
    }
}
