using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Playables;
using UnityEngine.XR.Interaction.Toolkit;

public class PanicButton : MonoBehaviour
{
    [SerializeField]
    private InputActionReference panicButtonReference;

    [SerializeField]
    private Transform safeRoomTransform;

    [SerializeField]
    private Transform playerTransform;
    [SerializeField]
    private Transform cameraTransform;

    [SerializeField]
    private GameObject[] grabbables;

    [SerializeField]
    private Transform savedTransform;

    public bool active = false;

    [SerializeField]
    private GameObject[] timelines;
    private List<GameObject> deactivatedTimelines = new List<GameObject>();

    // Update is called once per frame
    void Update()
    {
        float input = panicButtonReference.action.ReadValue<float>();

        if((input > 0.1f && !active)|| Input.GetKeyDown(KeyCode.B))
        {
            //Debug.Log("Panic Button Pressed");
            DropItemsFromController(true);
            DeactivateAllActiveTimelines();
            ActivateSafeRoom();
        }

        if (Input.GetKeyDown(KeyCode.H))
        {
            //Debug.Log("Teleport back to previous Position");
            DeactivateSafeRoom();

            ActivatePausedTimelines();
        }
    }

    private void DeactivateAllActiveTimelines()
    {
        deactivatedTimelines = new List<GameObject>();

        foreach(GameObject t in timelines)
        {
            if (t.GetComponent<PlayableDirector>().playableGraph.IsValid() && t.GetComponent<PlayableDirector>().playableGraph.IsPlaying())
            {
                t.GetComponent<PlayableDirector>().Pause();
                deactivatedTimelines.Add(t);
            }
        }
    }

    public void DropItemsFromController(bool reenable)
    {
        foreach(GameObject g in grabbables)
        {
            if(g != null)
            {
                g.GetComponent<XRGrabInteractable>().enabled = false;
            }

            if (reenable)
            {
                StartCoroutine(EnableGrabInteractorDelay(g));
            }
        }
    }

    IEnumerator EnableGrabInteractorDelay(GameObject grabbable)
    {
        yield return new WaitForSeconds(1f);
        grabbable.GetComponent<XRGrabInteractable>().enabled = true;
    }

    public void ActivatePausedTimelines()
    {
        foreach (GameObject t in deactivatedTimelines)
        {
            t.GetComponent<PlayableDirector>().Play();
        }
    }

    // bringt Nutzer in den Safe Room
    private void ActivateSafeRoom()
    {
        active = true;
        SafeTransform(playerTransform);
        TeleportPlayerToTransform(safeRoomTransform);
        //DeactivateAllActiveTimelines();
        //Time.timeScale = 1;
    }

    // bringt Nutzer zurück zur Ausgangsposition
    public void DeactivateSafeRoom()
    {
        TeleportPlayerToTransform(savedTransform);
        active = false;
        //Time.timeScale = 1;
        //ActivateAllTimelines();
    }

    public void BackToStart()
    {
        // Zurück zum Startmenü


        // provisorisch
        #if UNITY_EDITOR
            // Application.Quit() does not work in the editor so
            // UnityEditor.EditorApplication.isPlaying need to be set to false to end the game
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }

    private void SafeTransform(Transform saveTransform)
    {
        savedTransform.position = saveTransform.position;
        savedTransform.rotation = saveTransform.rotation;
    }

    private void TeleportPlayerToTransform(Transform newTransform)
    {
        playerTransform.position = newTransform.position;
        playerTransform.rotation = newTransform.rotation;

        cameraTransform.rotation = Quaternion.Euler(cameraTransform.rotation.eulerAngles.x, newTransform.rotation.eulerAngles.y, cameraTransform.rotation.eulerAngles.z);
    }

    // Speichern der aktuellen Position und Rotation
    // Teleportieren zum Safe Room und anhalten des Spielgeschehens
    // Wieder zurück zur Ursprünglichen Position teleportieren, wenn weitergemacht werden soll
    // Oder beenden wenn abgebrochen werden soll
}
