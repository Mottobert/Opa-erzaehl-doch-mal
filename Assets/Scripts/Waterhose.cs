using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class Waterhose : MonoBehaviour
{
    [SerializeField]
    private InputActionReference waterHoseReference;
    [SerializeField]
    private ParticleSystem waterParticleSystem;
    [SerializeField]
    private ActionBasedController rightController;

    public bool active = false;

    // Update is called once per frame
    void Update()
    {
        if (active)
        {
            float input = waterHoseReference.action.ReadValue<float>();
            UpdateWaterParticleSystem(input);
        }
    }

    private void UpdateWaterParticleSystem(float input)
    {
        if(input != 0)
        {
            rightController.SendHapticImpulse(Clamp(input + Random.Range(0, 0.1f), 1f), 1/Time.deltaTime);
        }
        
        var emission = waterParticleSystem.emission;
        emission.rateOverTime = input * 1000;
    }

    private float Clamp(float value, float maxValue)
    {
        if(value > maxValue)
        {
            return maxValue;
        }
        else
        {
            return value;
        }
    }

    public void ActivateWaterhose()
    {
        active = true;
        waterParticleSystem.Play();
    }

    public void DeactivateWaterhose()
    {
        active = false;
        waterParticleSystem.Stop();
    }
}

