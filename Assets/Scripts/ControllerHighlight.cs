using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Playables;
using UnityEngine.XR.Interaction.Toolkit;

public class ControllerHighlight : MonoBehaviour
{
    [SerializeField]
    private bool tutorialIntroduction;

    private bool tutorialStarted = false;
    [SerializeField]
    private PlayableDirector tutorialTimeline;

    [SerializeField]
    private GameObject rightGripButton;
    [SerializeField]
    private GameObject rightTriggerButton;
    [SerializeField]
    private GameObject rightJoystick;
    [SerializeField]
    private GameObject rightAButton;
    [SerializeField]
    private GameObject rightBButton;

    [SerializeField]
    private GameObject leftGripButton;
    [SerializeField]
    private GameObject leftTriggerButton;
    [SerializeField]
    private GameObject leftJoystick;
    [SerializeField]
    private GameObject leftXButton;
    [SerializeField]
    private GameObject leftYButton;

    [SerializeField]
    private InputActionReference rightJoystickReference;
    [SerializeField]
    private InputActionReference leftGripReference;
    [SerializeField]
    private InputActionReference panicButtonReference;
    [SerializeField]
    private InputActionReference rightGripReference;
    [SerializeField]
    private InputActionReference rightTriggerReference;

    private bool rightJoystickPressed = false;
    private bool leftGripPressed = false;
    private bool panicButtonPressed = false;
    private bool rightGripPressed = false;
    private bool rightTriggerPressed = false;

    [SerializeField]
    private ActionBasedController rightController;
    [SerializeField]
    private ActionBasedController leftController;

    [SerializeField]
    private GameObject joystickCanvas;
    [SerializeField]
    private GameObject joystickControllerCanvas;
    [SerializeField]
    private GameObject leftGripCanvas;
    [SerializeField]
    private GameObject leftGripControllerCanvas;
    [SerializeField]
    private GameObject panicButtonCanvas;
    [SerializeField]
    private GameObject panicButtonRightControllerCanvas;
    [SerializeField]
    private GameObject panicButtonLeftControllerCanvas;
    [SerializeField]
    private GameObject rightGripCanvas;
    [SerializeField]
    private GameObject rightGripControllerCanvas;
    [SerializeField]
    private GameObject rightTriggerCanvas;
    [SerializeField]
    private GameObject rightTriggerControllerCanvas;
    [SerializeField]
    private GameObject bookCanvas;

    [SerializeField]
    private GameObject rightTriggerButtonPressCanvas;

    [SerializeField]
    private GameObject wildfireBook;

    [SerializeField]
    private GameObject[] outlineObjects;

    [SerializeField]
    private PanicButton panicButton;

    [SerializeField]
    private GameObject[] interactables;

    public bool safeRoomWasActive = false;

    // Start is called before the first frame update
    void Start()
    {
        HideAllCanvases();
        DeactivateInteractables();

        if (tutorialIntroduction)
        {
            tutorialTimeline.Play();
        }
        else
        {
            StartTutorial();
        }
    }

    public void StartTutorial()
    {
        StartCoroutine(ButtonBlinking(rightJoystick, 0f, rightController, joystickCanvas, joystickControllerCanvas));
        tutorialStarted = true;
    }

    private void Update()
    {
        // Wenn rechter Joystick gedrueckt -> linker Grip Button highlighten
        if((rightJoystickReference.action.ReadValue<Vector2>().x > 0 || rightJoystickReference.action.ReadValue<Vector2>().x < 0 || rightJoystickReference.action.ReadValue<Vector2>().y > 0 || rightJoystickReference.action.ReadValue<Vector2>().y < 0) && !rightJoystickPressed && tutorialStarted)
        {
            StopAllCoroutines();
            ResetButtonHighlight(rightJoystick);

            rightJoystickPressed = true;

            StartCoroutine(ButtonBlinking(leftGripButton, 2f, leftController, leftGripCanvas, leftGripControllerCanvas));
        }

        // Wenn linker Grip Button gedrueckt -> Panic Buttons highlighten
        if (leftGripReference.action.ReadValue<float>() > 0 && rightJoystickPressed && !leftGripPressed && tutorialStarted)
        {
            StopAllCoroutines();
            ResetButtonHighlight(leftGripButton);

            leftGripPressed = true;

            StartCoroutine(ButtonBlinking(rightAButton, 2f, rightController, panicButtonCanvas, panicButtonRightControllerCanvas));
            StartCoroutine(ButtonBlinking(rightBButton, 2f, rightController, panicButtonCanvas, panicButtonRightControllerCanvas));
            StartCoroutine(ButtonBlinking(leftXButton, 2f, leftController, panicButtonCanvas, panicButtonLeftControllerCanvas));
            StartCoroutine(ButtonBlinking(leftYButton, 2f, leftController, panicButtonCanvas, panicButtonLeftControllerCanvas));

            panicButton.gameObject.SetActive(true);
        }

        // Wenn Panic Buttons gedrueckt -> rechter Grip Button highlighten
        if (leftGripPressed && !panicButtonPressed && tutorialStarted && !panicButton.active && safeRoomWasActive)
        {
            StopAllCoroutines();
            ResetButtonHighlight(rightAButton);
            ResetButtonHighlight(rightBButton);
            ResetButtonHighlight(leftXButton);
            ResetButtonHighlight(leftYButton);

            panicButtonPressed = true;

            ActivateOutlineObjects();

            StartCoroutine(ButtonBlinking(rightGripButton, 2f, rightController, rightGripCanvas, rightGripControllerCanvas));

            ActivateInteractables();
        }

        // Wenn rechter Grip Button gedrueckt -> rechter Trigger highlighten
        if (rightGripReference.action.ReadValue<float>() > 0 && panicButtonPressed && !rightGripPressed && tutorialStarted && !panicButton.active)
        {
            StopAllCoroutines();
            ResetButtonHighlight(rightGripButton);

            rightGripPressed = true;

            DeactivateOutlineObjects();

            StartCoroutine(ButtonBlinking(rightTriggerButton, 2f, rightController, rightTriggerCanvas, rightTriggerControllerCanvas));
        }

        // Wenn rechter Grip Trigger gedrueckt -> kein highlight mehr
        if (rightTriggerReference.action.ReadValue<float>() > 0.1f && rightGripPressed && !rightTriggerPressed && tutorialStarted && !panicButton.active)
        {
            StopAllCoroutines();
            ResetButtonHighlight(rightTriggerButton);

            rightTriggerPressed = true;

            bookCanvas.SetActive(true);
            wildfireBook.GetComponent<Outline>().ActivateBlinking();
        }
    }

    private void ActivateInteractables()
    {
        foreach(GameObject i in interactables)
        {
            i.GetComponent<XRGrabInteractable>().enabled = true;
        }
    }

    private void DeactivateInteractables()
    {
        foreach (GameObject i in interactables)
        {
            i.GetComponent<XRGrabInteractable>().enabled = false;
        }
    }

    public void SetSafeRoomActive()
    {
        safeRoomWasActive = true;
    }

    public void HighlightRightTrigger()
    {
        StartCoroutine(ButtonBlinking(rightTriggerButton, 0f, rightController, null, rightTriggerButtonPressCanvas));
    }

    IEnumerator ButtonBlinking(GameObject button, float delay, ActionBasedController controller, GameObject canvas, GameObject controllerCanvas)
    {
        yield return new WaitForSeconds(delay);

        ResetButtonBlinkingForAll();

        controller.SendHapticImpulse(0.8f, 0.2f);

        if (canvas)
        {
            canvas.SetActive(true);
            canvas.GetComponentInChildren<AudioSource>().Play();
        }
        
        controllerCanvas.SetActive(true);

        float value = 0;
        bool positive = true;

        while (true)
        {
            if(value > 0.75f)
            {
                positive = false;
            }

            if(value < 0.02f)
            {
                positive = true;
            }

            if (positive)
            {
                value = value + 0.01f;
            }
            else if (!positive)
            {
                value = value - 0.01f;
            }

            yield return new WaitForSeconds(0.01f);
            button.GetComponent<SkinnedMeshRenderer>().material.SetFloat("Vector1_58daa7a04f41410286e9e6b425c90861", value);
        }
    }

    public void HideAllCanvases()
    {
        joystickCanvas.SetActive(false);
        leftGripCanvas.SetActive(false);
        panicButtonCanvas.SetActive(false);
        rightGripCanvas.SetActive(false);
        rightTriggerCanvas.SetActive(false);
        joystickControllerCanvas.SetActive(false);
        leftGripControllerCanvas.SetActive(false);
        panicButtonRightControllerCanvas.SetActive(false);
        panicButtonLeftControllerCanvas.SetActive(false);
        rightGripControllerCanvas.SetActive(false);
        rightTriggerControllerCanvas.SetActive(false);
        bookCanvas.SetActive(false);
        rightTriggerButtonPressCanvas.SetActive(false);
    }

    private void ResetButtonHighlight(GameObject button)
    {
        //highlight.GetComponent<SkinnedMeshRenderer>().material.SetFloat("Vector1_58daa7a04f41410286e9e6b425c90861", 0);
        StartCoroutine(ResetButtonBlinking(button, 0f));
        HideAllCanvases();
    }

    IEnumerator ResetButtonBlinking(GameObject button, float delay)
    {
        yield return new WaitForSeconds(delay);

        float value = button.GetComponent<SkinnedMeshRenderer>().material.GetFloat("Vector1_58daa7a04f41410286e9e6b425c90861"); 
        bool positive = true;

        while (true)
        {
            if (value > 0.98f)
            {
                positive = false;
            }

            if (positive)
            {
                value = value + 0.01f;
            }
            else if (!positive)
            {
                value = value - 0.01f;
            }

            if(value < 0)
            {
                break;
            }

            yield return new WaitForSeconds(0.01f);
            button.GetComponent<SkinnedMeshRenderer>().material.SetFloat("Vector1_58daa7a04f41410286e9e6b425c90861", value);
        }
    }

    private void ResetButtonBlinkingForAll()
    { 
        rightGripButton.GetComponent<SkinnedMeshRenderer>().material.SetFloat("Vector1_58daa7a04f41410286e9e6b425c90861", 0);
        rightTriggerButton.GetComponent<SkinnedMeshRenderer>().material.SetFloat("Vector1_58daa7a04f41410286e9e6b425c90861", 0);
        rightJoystick.GetComponent<SkinnedMeshRenderer>().material.SetFloat("Vector1_58daa7a04f41410286e9e6b425c90861", 0);
        rightAButton.GetComponent<SkinnedMeshRenderer>().material.SetFloat("Vector1_58daa7a04f41410286e9e6b425c90861", 0);
        rightBButton.GetComponent<SkinnedMeshRenderer>().material.SetFloat("Vector1_58daa7a04f41410286e9e6b425c90861", 0);

        leftGripButton.GetComponent<SkinnedMeshRenderer>().material.SetFloat("Vector1_58daa7a04f41410286e9e6b425c90861", 0);
        leftTriggerButton.GetComponent<SkinnedMeshRenderer>().material.SetFloat("Vector1_58daa7a04f41410286e9e6b425c90861", 0);
        leftJoystick.GetComponent<SkinnedMeshRenderer>().material.SetFloat("Vector1_58daa7a04f41410286e9e6b425c90861", 0);
        leftXButton.GetComponent<SkinnedMeshRenderer>().material.SetFloat("Vector1_58daa7a04f41410286e9e6b425c90861", 0);
        leftYButton.GetComponent<SkinnedMeshRenderer>().material.SetFloat("Vector1_58daa7a04f41410286e9e6b425c90861", 0);
    }

    private void ActivateOutlineObjects()
    {
        foreach(GameObject g in outlineObjects)
        {
            g.GetComponent<Outline>().ActivateBlinking();
        }
    }

    private void DeactivateOutlineObjects()
    {
        foreach (GameObject g in outlineObjects)
        {
            g.GetComponent<Outline>().DeactivateBlinking();
        }
    }
}
