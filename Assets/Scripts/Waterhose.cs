using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class Waterhose : MonoBehaviour
{
    [SerializeField]
    private InputActionReference triggerReference;
    [SerializeField]
    private ParticleSystem waterParticleSystem;
    [SerializeField]
    private ActionBasedController rightController;

    public float waterAmount = 15;
    private bool waterTick = true;

    public bool active = false;

    private bool waterEmpty = false;

    [SerializeField]
    private Waterpump waterpump;

    [SerializeField]
    private GameObject[] waterAmountdisplay;
    [SerializeField]
    private GameObject[] waterAmountdisplayPump;
    [SerializeField]
    private Material waterAmountDisplayDefaultMaterial;
    [SerializeField]
    private Material waterAmountDisplayFullMaterial;

    [SerializeField]
    private GameObject waterEmptyCanvas;

    private void Start()
    {
        UpdateWaterAmountDisplay();

        HideEmptyWaterCanvas();
    }

    // Update is called once per frame
    void Update()
    {
        if (active && waterAmount > 0 && !waterpump.outsideActive)
        {
            float input = triggerReference.action.ReadValue<float>();

            UpdateWaterParticleSystem(input);

            if (waterTick && input > 0.1f)
            {
                StartCoroutine(EmptyWater());
            }
        }

        if(waterAmount < 1 && !waterEmpty)
        {
            //UpdateWaterParticleSystem(0f);
            StartCoroutine(LowerWaterPressure());
        }
    }

    IEnumerator EmptyWater()
    {
        waterTick = false;
        waterAmount = waterAmount - 1;
        UpdateWaterAmountDisplay();
        yield return new WaitForSeconds(1f);
        waterTick = true;
    }

    IEnumerator LowerWaterPressure()
    {
        waterEmpty = true;

        var waterPSMain = waterParticleSystem.main;
        float waterpressure = waterParticleSystem.startSpeed;
        while (waterpressure > 1f)
        {
            waterpressure = Clamp(waterpressure - 0.3f, 20, 0);
            waterPSMain.startSpeedMultiplier = waterpressure;

            var emission = waterParticleSystem.emission;
            emission.rateOverTime = Clamp(Remap(waterpressure - 1, 0, 20, 0, 1), 1f, 0f) * 1000;
            yield return new WaitForSeconds(0.02f);
        }

        ShowEmptyWaterCanvas();
    }

    public void ResetWaterPressure()
    {
        waterParticleSystem.startSpeed = 20;
    }

    public float Remap(float value, float from1, float to1, float from2, float to2)
    {
        return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
    }

    public void UpdateWaterParticleSystem(float input)
    {
        if (input > 0.1f)
        {
            rightController.SendHapticImpulse(Clamp(input + Random.Range(0, 0.1f), 1f, 0f), 0.1f);
        }
        
        var emission = waterParticleSystem.emission;
        emission.rateOverTime = input * 1000;

        var waterPSMain = waterParticleSystem.main;
        waterPSMain.startSpeedMultiplier = Remap(input, 0, 1, 0, 20);
    }

    private float Clamp(float value, float maxValue, float minValue)
    {
        if(value > maxValue)
        {
            return maxValue;
        }
        else if(value < minValue)
        {
            return minValue;
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

    public void IncreaseWaterAmount()
    {
        waterAmount++;
        UpdateWaterAmountDisplay();
        ResetWaterPressure();
        waterEmpty = false;

        HideEmptyWaterCanvas();
    }

    public void UpdateWaterAmountDisplay()
    {
        ResetWaterAmountDisplay();

        for(int i = 0; i < waterAmountdisplay.Length; i++)
        {
            if(i < waterAmount)
            {
                waterAmountdisplay[i].GetComponent<MeshRenderer>().material = waterAmountDisplayFullMaterial;
                waterAmountdisplayPump[i].GetComponent<MeshRenderer>().material = waterAmountDisplayFullMaterial;
            }
        }
    }

    private void ResetWaterAmountDisplay()
    {
        foreach(GameObject wd in waterAmountdisplay)
        {
            wd.GetComponent<MeshRenderer>().material = waterAmountDisplayDefaultMaterial;
        }

        foreach (GameObject wd in waterAmountdisplayPump)
        {
            wd.GetComponent<MeshRenderer>().material = waterAmountDisplayDefaultMaterial;
        }
    }

    private void ShowEmptyWaterCanvas()
    {
        waterEmptyCanvas.SetActive(true);
    }

    public void HideEmptyWaterCanvas()
    {
        waterEmptyCanvas.SetActive(false);
    }
}
