using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Waterhose : MonoBehaviour
{
    [SerializeField]
    private InputActionReference waterHoseReference;
    [SerializeField]
    private ParticleSystem waterParticleSystem;

    // Update is called once per frame
    void Update()
    {
        float input = waterHoseReference.action.ReadValue<float>();
        UpdateWaterParticleSystem(input);
    }

    private void UpdateWaterParticleSystem(float input)
    {
        var emission = waterParticleSystem.emission;
        emission.rateOverTime = input * 1000;
    }
}