using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class ConfettiCanon : MonoBehaviour
{
    public bool active = false;

    [SerializeField]
    private InputActionReference triggerReference;
    [SerializeField]
    private AudioSource confettiSound;
    [SerializeField]
    private ParticleSystem confettiParticleSystem;
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
        if (input > 0.7f && updateTimer > 1 && !confettiParticleSystem.isPlaying)
        {
            updateTimer = 0;
            rightController.SendHapticImpulse(0.8f, 0.1f);
            confettiParticleSystem.Play();
            confettiSound.Play();
        }

        updateTimer++;
    }

    public void ActivateAirHorn()
    {
        active = true;
        
    }

    public void DeactivateAirHorn()
    {
        active = false;
        confettiSound.Stop();
    }
}
