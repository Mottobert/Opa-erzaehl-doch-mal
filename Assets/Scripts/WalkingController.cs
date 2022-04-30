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
    private Animator opaAnimator;
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

    [SerializeField]
    private SkinnedMeshRenderer opaSkinnedMeshRendererLOD1;
    [SerializeField]
    private SkinnedMeshRenderer opaSkinnedMeshRendererLOD2;
    [SerializeField]
    private SkinnedMeshRenderer opaSkinnedMeshRendererLOD3;
    [SerializeField]
    private GameObject dissolveParticleSystem;

    [SerializeField]
    private PanicButton safeRoomController;

    float updateTimer;
    bool opaAhead = false;

    private void Start()
    {
        CheckDistanceToTeleportationAreas();
        StopWalking();
    }

    private void FixedUpdate()
    {
        // alle 2 Sekunden wird gecheckt, ob der Opa sich zu weit entfernt hat
        if (updateTimer == 100)
        {
            updateTimer = 0;

            CheckDistanceToTeleportationAreas();

            float distanceOpaPlayer = Vector3.Distance(opa.transform.position, player.transform.position);

            bool safeRoomActive = safeRoomController.active;

            if(distanceOpaPlayer > maxDistanceOpaPlayer && player.transform.position.x > opa.transform.position.x && !opaAhead && !safeRoomActive)
            {
                StopWalking();

                opaAhead = true;
                //Debug.Log("Opa too much ahead");
            }
            else if(distanceOpaPlayer > 3f && player.transform.position.x < opa.transform.position.x && !safeRoomActive)
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
            case 0:
                StopWalking();
                break;
            case 4:
                // Blumenwiese Text abspielen
                timelineBlumenwiese.Play();
                StopWalking();
                break;
            case 15:
                // See Text abspielen
                timelineSee.Play();
                StopWalking();
                break;
            case 23:
                // Schmetterling Text abspielen
                timelineSchmetterling.Play();
                StopWalking();
                break;
            case 32:
                // Feuer abspielen
                timelineFeuer.Play();
                StopWalking();
                break;
        }
    }

    private void StopWalking()
    {
        bgCurve.GetComponent<BGCcCursorChangeLinear>().Speed = 0;
        opaAnimator.SetBool("active", false);
    }

    public void ResetCurveSpeed()
    {
        //Debug.Log("ResetCurve");
        bgCurve.GetComponent<BGCcCursorChangeLinear>().Speed = 0.8f;
        opaAnimator.SetBool("active", true);
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

    public void DissolveOpa()
    {
        Invoke("StartDissolveParticleSystem", 0.5f);
        Invoke("StopDissolveParticleSystem", 2f);
        
        StartCoroutine(ChangeOpaDissolve(1f));
    }

    public void ShowOpa()
    {
        opaSkinnedMeshRendererLOD1.material.SetFloat("Vector1_db32db56eadb40c0bdf8b88238ef20f1", 0);
        opaSkinnedMeshRendererLOD2.material.SetFloat("Vector1_db32db56eadb40c0bdf8b88238ef20f1", 0);
        opaSkinnedMeshRendererLOD3.material.SetFloat("Vector1_db32db56eadb40c0bdf8b88238ef20f1", 0);
    }

    private void StartDissolveParticleSystem()
    {
        dissolveParticleSystem.GetComponent<ParticleSystem>().Play();
        dissolveParticleSystem.GetComponent<Animator>().SetBool("active", true);
    }

    private void StopDissolveParticleSystem()
    {
        dissolveParticleSystem.GetComponent<ParticleSystem>().Stop();
    }

    IEnumerator ChangeOpaDissolve(float maxValue)
    {
        float dissolve = 0;

        while (dissolve < maxValue)
        {
            dissolve = dissolve + 0.005f;
            opaSkinnedMeshRendererLOD1.material.SetFloat("Vector1_db32db56eadb40c0bdf8b88238ef20f1", dissolve);
            opaSkinnedMeshRendererLOD2.material.SetFloat("Vector1_db32db56eadb40c0bdf8b88238ef20f1", dissolve);
            opaSkinnedMeshRendererLOD3.material.SetFloat("Vector1_db32db56eadb40c0bdf8b88238ef20f1", dissolve);
            yield return new WaitForSeconds(0.01f);
        }
    }

    public void TeleportOpa(Transform location)
    {
        bgCurve.GetComponent<BGCcCursorObjectTranslate>().ObjectToManipulate = null;

        opa.transform.position = location.position;
        opa.transform.rotation = location.rotation;
    }
}
