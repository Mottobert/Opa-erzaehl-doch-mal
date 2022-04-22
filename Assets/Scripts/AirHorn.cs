using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class AirHorn : MonoBehaviour
{
    public bool active = false;

    [SerializeField]
    private InputActionReference triggerReference;
    [SerializeField]
    private AudioSource airHornSound;
    [SerializeField]
    private ActionBasedController rightController;

    private float updateTimer = 0;

    // Update is called once per frame
    void Update()
    {
        if (active)
        {
            float input = triggerReference.action.ReadValue<float>();
            UpdateAirHornSound(input);
        }
    }

    private void UpdateAirHornSound(float input)
    {
        if (input > 0.1f && updateTimer > 1)
        {
            updateTimer = 0;
            rightController.SendHapticImpulse(Clamp(input + Random.Range(0, 0.1f), 1f), 0.1f);
        }
        
        airHornSound.volume = input;

        updateTimer++;
    }

    private float Clamp(float value, float maxValue)
    {
        if (value > maxValue)
        {
            return maxValue;
        }
        else
        {
            return value;
        }
    }

    public void ActivateAirHorn()
    {
        active = true;
        airHornSound.Play();
    }

    public void DeactivateAirHorn()
    {
        active = false;
        airHornSound.Stop();
    }
}
