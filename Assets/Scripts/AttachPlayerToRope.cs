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

    public void AttachPlayer()
    {
        xrOrigin.transform.parent = this.transform;
        if (hoehenangst)
        {
            helicopterFlyAwayTimelineHoehenangst.Play();
        }
        else
        {
            helicopterFlyAwayTimelineNoHoehenangst.Play();
        }
    }

    public void DetachPlayer()
    {
        xrOrigin.transform.parent = null;
    }

    public void SetHoehenangst(bool angst)
    {
        hoehenangst = angst;
    }
}
