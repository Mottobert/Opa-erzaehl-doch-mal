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

    private bool isTriggerable = true;

    // Update is called once per frame
    void Update()
    {
        if (active && isTriggerable)
        {
            float input = triggerReference.action.ReadValue<float>();
            UpdateConfettiCanon(input);
        }
    }

    private void UpdateConfettiCanon(float input)
    {
        if (input > 0.7f && updateTimer > 10)
        {
            updateTimer = 0;
            rightController.SendHapticImpulse(0.8f, 0.1f);
            confettiParticleSystem.Play();
            confettiParticleSystem.Emit(50);
            confettiSound.Stop();
            confettiSound.Play();

            isTriggerable = false;
            Invoke("SetTriggerable", 2f);
        }

        updateTimer++;
    }

    private void SetTriggerable()
    {
        isTriggerable = true;
    }

    public void ActivateConfettiCanon()
    {
        active = true;
    }

    public void DeactivateConfettiCanon()
    {
        active = false;
        confettiSound.Stop();
    }
}
