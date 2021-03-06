using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ControllerHandler : MonoBehaviour
{
    [SerializeField]
    private GameObject leftController = default;
    [SerializeField]
    private GameObject rightController = default;

    private static Color red = new Color(1f, 0f, 0f);
    private static Color black = new Color(0f, 0f, 0f);

    void Awake()
    {
        UpdateControllers();
    }

    // Other logic that you might require...
    // This is a good centralised place to check if triggers are pressed, etc

    void UpdateControllers()
    {
        if (leftController == null || rightController == null)
        {
            // If you haven't pre-defined the controllers in the editor
            // Find them via tags at run-time
            GameObject[] controllers = GameObject.FindGameObjectsWithTag("GameController");
            // Bit of an assumption, but this can be changed if needed
            leftController = controllers[0];
            rightController = controllers[1];

            leftController.GetComponentInChildren<CustomControllerSystem>().Awake();
            rightController.GetComponentInChildren<CustomControllerSystem>().Awake();
        }
    }

    // Convenience methods
    public void HighlightButtonOne(bool active)
    {
        HighlightButtonOne(active, true);
        HighlightButtonOne(active, false);
    }

    public void HighlightButtonOne(bool active, bool isLeft)
    {
        if (active)
        {
            SetButtonOneColour(red, isLeft);
            SetButtonOneTextColour(black, isLeft);
            SetButtonOneEmit(true, isLeft);
            SetButtonOneTextEmit(true, isLeft);
        }
        else
        {
            SetDefaultButtonOneColour(isLeft);
            SetDefaultButtonOneTextColour(isLeft);
            SetButtonOneEmit(false, isLeft);
            SetButtonOneTextEmit(false, isLeft);
        }
    }

    public void HighlightButtonTwo(bool active)
    {
        HighlightButtonTwo(active, true);
        HighlightButtonTwo(active, false);
    }

    public void HighlightButtonTwo(bool active, bool isLeft)
    {
        if (active)
        {
            SetButtonTwoColour(red, isLeft);
            SetButtonTwoTextColour(black, isLeft);
            SetButtonTwoEmit(true, isLeft);
            SetButtonTwoTextEmit(true, isLeft);
        }
        else
        {
            SetDefaultButtonTwoColour(isLeft);
            SetDefaultButtonTwoTextColour(isLeft);
            SetButtonTwoEmit(false, isLeft);
            SetButtonTwoTextEmit(false, isLeft);
        }
    }

    public void HighlightToggle(bool active)
    {
        HighlightToggle(active, true);
        HighlightToggle(active, false);
    }

    public void HighlightToggle(bool active, bool isLeft)
    {
        if (active)
        {
            SetToggleColour(red, isLeft);
            SetToggleEmit(true, isLeft);
        }
        else
        {
            SetDefaultToggleColour(isLeft);
            SetToggleEmit(false, isLeft);
        }
    }

    public void HighlightPrimaryTrigger(bool active)
    {
        HighlightPrimaryTrigger(active, true);
        HighlightPrimaryTrigger(active, false);
    }

    public void HighlightPrimaryTrigger(bool active, bool isLeft)
    {
        if (active)
        {
            SetPrimaryTriggerColour(red, isLeft);
            SetPrimaryTriggerEmit(true, isLeft);
        }
        else
        {
            SetDefaultPrimaryTriggerColour(isLeft);
            SetPrimaryTriggerEmit(false, isLeft);
        }
    }

    public void HighlightSecondaryTrigger(bool active)
    {
        HighlightSecondaryTrigger(active, true);
        HighlightSecondaryTrigger(active, false);
    }

    public void HighlightSecondaryTrigger(bool active, bool isLeft)
    {
        if (active)
        {
            SetSecondaryTriggerColour(red, isLeft);
            SetSecondaryTriggerEmit(true, isLeft);
        }
        else
        {
            SetDefaultSecondaryTriggerColour(isLeft);
            SetSecondaryTriggerEmit(false, isLeft);
        }
    }


    // Base methods for all buttons
    public void SetControllerButtonColour(ControllerEnum button, Color col, bool isLeft)
    {
        if (isLeft)
        {
            if (leftController != null)
                leftController.GetComponentInChildren<CustomControllerSystem>().SetControllerButtonColour(button, col);
        }
        else
        {
            if (rightController != null)
                rightController.GetComponentInChildren<CustomControllerSystem>().SetControllerButtonColour(button, col);
        }
    }

    public void SetControllerButtonEmit(ControllerEnum button, bool on, bool isLeft)
    {
        if (isLeft)
        {
            if (leftController != null)
                leftController.GetComponentInChildren<CustomControllerSystem>().SetControllerButtonEmit(button, on);
        }
        else
        {
            if (rightController != null)
                rightController.GetComponentInChildren<CustomControllerSystem>().SetControllerButtonEmit(button, on);
        }
    }

    public void SetControllerDefaultButtonColour(ControllerEnum button, bool isLeft)
    {
        if (isLeft)
        {
            if (leftController != null)
                leftController.GetComponentInChildren<CustomControllerSystem>().SetControllerDefaultButtonColour(button);
        }
        else
        {
            if (rightController != null)
                rightController.GetComponentInChildren<CustomControllerSystem>().SetControllerDefaultButtonColour(button);
        }
    }

    // Button One
    public void SetButtonOneColour(Color col, bool isLeft)
    {
        SetControllerButtonColour(ControllerEnum.BUTTON_ONE, col, isLeft);
    }

    public void SetButtonOneEmit(bool on, bool isLeft)
    {
        SetControllerButtonEmit(ControllerEnum.BUTTON_ONE, on, isLeft);
    }

    public void SetDefaultButtonOneColour(bool isLeft)
    {
        SetControllerDefaultButtonColour(ControllerEnum.BUTTON_ONE, isLeft);
    }

    // Button One Text
    public void SetButtonOneTextColour(Color col, bool isLeft)
    {
        SetControllerButtonColour(ControllerEnum.BUTTON_ONE_TEXT, col, isLeft);
    }

    public void SetButtonOneTextEmit(bool on, bool isLeft)
    {
        SetControllerButtonEmit(ControllerEnum.BUTTON_ONE_TEXT, on, isLeft);
    }

    public void SetDefaultButtonOneTextColour(bool isLeft)
    {
        SetControllerDefaultButtonColour(ControllerEnum.BUTTON_ONE_TEXT, isLeft);
    }

    // Button Two
    public void SetButtonTwoColour(Color col, bool isLeft)
    {
        SetControllerButtonColour(ControllerEnum.BUTTON_TWO, col, isLeft);
    }

    public void SetButtonTwoEmit(bool on, bool isLeft)
    {
        SetControllerButtonEmit(ControllerEnum.BUTTON_TWO, on, isLeft);
    }

    public void SetDefaultButtonTwoColour(bool isLeft)
    {
        SetControllerDefaultButtonColour(ControllerEnum.BUTTON_TWO, isLeft);
    }

    // Button Two Text
    public void SetButtonTwoTextColour(Color col, bool isLeft)
    {
        SetControllerButtonColour(ControllerEnum.BUTTON_TWO_TEXT, col, isLeft);
    }

    public void SetButtonTwoTextEmit(bool on, bool isLeft)
    {
        SetControllerButtonEmit(ControllerEnum.BUTTON_TWO_TEXT, on, isLeft);
    }

    public void SetDefaultButtonTwoTextColour(bool isLeft)
    {
        SetControllerDefaultButtonColour(ControllerEnum.BUTTON_TWO_TEXT, isLeft);
    }

    // Toggle
    public void SetToggleColour(Color col, bool isLeft)
    {
        SetControllerButtonColour(ControllerEnum.TOGGLE, col, isLeft);
    }

    public void SetToggleEmit(bool on, bool isLeft)
    {
        SetControllerButtonEmit(ControllerEnum.TOGGLE, on, isLeft);
    }

    public void SetDefaultToggleColour(bool isLeft)
    {
        SetControllerDefaultButtonColour(ControllerEnum.TOGGLE, isLeft);
    }

    // Primary Trigger
    public void SetPrimaryTriggerColour(Color col, bool isLeft)
    {
        SetControllerButtonColour(ControllerEnum.PRIMARY_TRIGGER, col, isLeft);
    }

    public void SetPrimaryTriggerEmit(bool on, bool isLeft)
    {
        SetControllerButtonEmit(ControllerEnum.PRIMARY_TRIGGER, on, isLeft);
    }

    public void SetDefaultPrimaryTriggerColour(bool isLeft)
    {
        SetControllerDefaultButtonColour(ControllerEnum.PRIMARY_TRIGGER, isLeft);
    }

    // Secondary Trigger
    public void SetSecondaryTriggerColour(Color col, bool isLeft)
    {
        SetControllerButtonColour(ControllerEnum.SECONDARY_TRIGGER, col, isLeft);
    }

    public void SetSecondaryTriggerEmit(bool on, bool isLeft)
    {
        SetControllerButtonEmit(ControllerEnum.SECONDARY_TRIGGER, on, isLeft);
    }

    public void SetDefaultSecondaryTriggerColour(bool isLeft)
    {
        SetControllerDefaultButtonColour(ControllerEnum.SECONDARY_TRIGGER, isLeft);
    }
}
