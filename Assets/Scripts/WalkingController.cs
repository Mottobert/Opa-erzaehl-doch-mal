using BansheeGz.BGSpline.Curve;
using BansheeGz.BGSpline.Components;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.XR.Interaction.Toolkit;

public class WalkingController : MonoBehaviour
{
    [SerializeField]
    private BGCurve bgCurve;

    [SerializeField]
    private GameObject opa;
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private GameObject[] teleportationAreas;
    [SerializeField]
    private float teleportationAreaActivationRadius;

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

    [SerializeField]
    private float maxDistanceOpaPlayer;

    float updateTimer;
    bool opaAhead = false;

    private void Start()
    {
        CheckDistanceToTeleportationAreas();
    }

    private void FixedUpdate()
    {
        if (updateTimer == 100)
        {
            updateTimer = 0;

            float distanceOpaPlayer = Vector3.Distance(opa.transform.position, player.transform.position);

            if(distanceOpaPlayer > maxDistanceOpaPlayer && player.transform.position.x > opa.transform.position.x && !opaAhead)
            {
                bgCurve.GetComponent<BGCcCursorChangeLinear>().Speed = 0;
                opa.GetComponentInChildren<Animator>().SetBool("active", false);

                opaAhead = true;
                //Debug.Log("Opa too much ahead");
            }
            else if(distanceOpaPlayer > 3f && player.transform.position.x < opa.transform.position.x)
            {
                ResetCurveSpeed();

                opaAhead = false;
                //Debug.Log("Opa walk again");
            }
        }

        updateTimer++;
    }

    public void ReachedPoint(int point)
    {
        CheckDistanceToTeleportationAreas();

        switch (point)
        {
            case 1:
                // Blumenwiese Text abspielen
                timelineBlumenwiese.Play();
                bgCurve.GetComponent<BGCcCursorChangeLinear>().Speed = 0;
                opa.GetComponentInChildren<Animator>().SetBool("active", false);
                break;
            case 6:
                // See Text abspielen
                timelineSee.Play();
                bgCurve.GetComponent<BGCcCursorChangeLinear>().Speed = 0;
                opa.GetComponentInChildren<Animator>().SetBool("active", false);
                break;
            case 9:
                // Schmetterling Text abspielen
                timelineSchmetterling.Play();
                bgCurve.GetComponent<BGCcCursorChangeLinear>().Speed = 0;
                opa.GetComponentInChildren<Animator>().SetBool("active", false);
                break;
            case 12:
                // Feuer abspielen
                timelineFeuer.Play();
                bgCurve.GetComponent<BGCcCursorChangeLinear>().Speed = 0;
                opa.GetComponentInChildren<Animator>().SetBool("active", false);
                break;
        }
    }

    public void ResetCurveSpeed()
    {
        bgCurve.GetComponent<BGCcCursorChangeLinear>().Speed = 0.8f;
        opa.GetComponentInChildren<Animator>().SetBool("active", true);
    }

    private void CheckDistanceToTeleportationAreas()
    {
        foreach(GameObject teleArea in teleportationAreas)
        {
            float distance = Vector3.Distance(opa.transform.position, teleArea.transform.position);

            if (distance < teleportationAreaActivationRadius)
            {
                teleArea.SetActive(true);
            }
            else
            {
                teleArea.SetActive(false);
            }
        }
    }
}
