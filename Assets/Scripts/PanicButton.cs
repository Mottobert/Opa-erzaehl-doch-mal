using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PanicButton : MonoBehaviour
{
    [SerializeField]
    private InputActionReference panicButtonReference;

    [SerializeField]
    private Transform safeRoomTransform;

    [SerializeField]
    private Transform playerTransform;

    [SerializeField]
    private Transform savedTransform;

    public bool active = false;

    // Update is called once per frame
    void Update()
    {
        bool input = panicButtonReference.action.ReadValue<bool>();

        if(input || Input.GetKeyDown(KeyCode.B))
        {
            Debug.Log("Panic Button Pressed");
            ActivateSafeRoom();
        }

        if (Input.GetKeyDown(KeyCode.H))
        {
            Debug.Log("Teleport back to previous Position");
            DeactivateSafeRoom();
        }
    }

    // bringt Nutzer in den Safe Room
    private void ActivateSafeRoom()
    {
        active = true;
        SafeTransform(playerTransform);
        TeleportPlayerToTransform(safeRoomTransform);
    }

    // bringt Nutzer zurück zur Ausgangsposition
    public void DeactivateSafeRoom()
    {
        TeleportPlayerToTransform(savedTransform);
        active = false;
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
    }

    // Speichern der aktuellen Position und Rotation
    // Teleportieren zum Safe Room und anhalten des Spielgeschehens
    // Wieder zurück zur Ursprünglichen Position teleportieren, wenn weitergemacht werden soll
    // Oder beenden wenn abgebrochen werden soll
}
