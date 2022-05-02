using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class FlashlightInteractable : MonoBehaviour
{
    public bool active = false;

    [SerializeField]
    private InputActionReference triggerReference;

    [SerializeField]
    private GameObject directionalLight;
    [SerializeField]
    private GameObject postProcessingEffects;
    private ColorAdjustments thisExposure;
    [SerializeField]
    private GameObject spotLight;

    [SerializeField]
    private GameObject lampSphere;

    [SerializeField]
    private GameObject[] lights;

    // Update is called once per frame
    void Update()
    {
        if (active)
        {
            float input = triggerReference.action.ReadValue<float>();
            UpdateSpotLight(input);
        }
    }

    private void UpdateSpotLight(float input)
    {
        spotLight.GetComponent<Light>().intensity = Remap(input, 0, 1, 0, 20);
        lampSphere.GetComponent<MeshRenderer>().material.SetColor("_EmissionColor", new Color(191, 98, 0) * Mathf.LinearToGammaSpace(Remap(input, 0, 1, 0, 4)));
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

    public float Remap(float value, float from1, float to1, float from2, float to2)
    {
        return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
    }

    public void ActivateSpotLight()
    {
        active = true;
        spotLight.SetActive(true);
        directionalLight.GetComponent<Light>().intensity = 0;

        postProcessingEffects.GetComponent<Volume>().profile.TryGet<ColorAdjustments>(out thisExposure);
        thisExposure.postExposure.value = 0;

        RenderSettings.skybox.SetFloat("_Exposure", 0);

        SetLightsIntensity(0);

        RenderSettings.ambientIntensity = 0.1f;
    }

    public void DeactivateSpotLight()
    {
        active = false;
        spotLight.SetActive(false);
        directionalLight.GetComponent<Light>().intensity = 1;

        postProcessingEffects.GetComponent<Volume>().profile.TryGet<ColorAdjustments>(out thisExposure);
        thisExposure.postExposure.value = 1.5f;

        RenderSettings.skybox.SetFloat("_Exposure", 1.3f);

        SetLightsIntensity(1.1f);

        RenderSettings.ambientIntensity = 1f;
    }

    private void SetLightsIntensity(float value)
    {
        foreach(GameObject g in lights)
        {
            g.GetComponent<Light>().intensity = value;
        }
    }
}
