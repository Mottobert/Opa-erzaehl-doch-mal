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

    // Update is called once per frame
    void Update()
    {
        float input = waterHoseReference.action.ReadValue<float>();
        UpdateWaterParticleSystem(input);
    }

    private void UpdateWaterParticleSystem(float input)
    {
        rightController.SendHapticImpulse(Clamp(input + Random.Range(0,0.1f), 1f), 0.5f);
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
}

