using BansheeGz.BGSpline.Curve;
using BansheeGz.BGSpline.Components;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class WalkingController : MonoBehaviour
{
    [SerializeField]
    private BGCurve bgCurve;

    [SerializeField]
    private PlayableDirector timelineRoom;
    [SerializeField]
    private PlayableDirector timelineBlumenwiese;
    [SerializeField]
    private PlayableDirector timelineSee;
    [SerializeField]
    private PlayableDirector timelineSchmetterling;
    [SerializeField]
    private PlayableDirector timelineFeuer;


    public void ReachedPoint(int point)
    {
        bgCurve.GetComponent<BGCcCursorChangeLinear>().Speed = 0;

        switch (point)
        {
            case 1:
                // Blumenwiese Text abspielen
                timelineBlumenwiese.Play();
                break;
            case 6:
                // See Text abspielen
                timelineSee.Play();
                break;
            case 9:
                // Schmetterling Text abspielen
                timelineSchmetterling.Play();
                break;
            case 12:
                // Feuer abspielen
                timelineFeuer.Play();
                break;
        }
    }

    public void ResetCurveSpeed()
    {
        bgCurve.GetComponent<BGCcCursorChangeLinear>().Speed = 0.8f;
    }
}
