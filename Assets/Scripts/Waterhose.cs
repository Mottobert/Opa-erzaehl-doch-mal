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

    [SerializeField]
    private Waterpump waterpump;

    [SerializeField]
    private GameObject[] waterAmountdisplay;
    [SerializeField]
    private Material waterAmountDisplayDefaultMaterial;
    [SerializeField]
    private Material waterAmountDisplayFullMaterial;

    private void Start()
    {
        UpdateWaterAmountDisplay();
    }

    // Update is called once per frame
    void Update()
    {
        if (active && waterAmount > 0 && !waterpump.outsideActive)
        {
            float input = triggerReference.action.ReadValue<float>();

            UpdateWaterParticleSystem(input);

            if (waterTick)
            {
                StartCoroutine(EmptyWater());
            }
        }

        if(waterAmount < 1)
        {
            UpdateWaterParticleSystem(0);
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

    private void UpdateWaterParticleSystem(float input)
    {
        if(input > 0.1f)
        {
            rightController.SendHapticImpulse(Clamp(input + Random.Range(0, 0.1f), 1f), 0.1f);
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

    public void IncreaseWaterAmount()
    {
        waterAmount++;
        UpdateWaterAmountDisplay();
    }

    public void UpdateWaterAmountDisplay()
    {
        ResetWaterAmountDisplay();

        for(int i = 0; i < waterAmountdisplay.Length; i++)
        {
            if(i < waterAmount)
            {
                waterAmountdisplay[i].GetComponent<MeshRenderer>().material = waterAmountDisplayFullMaterial;
            }
        }
    }

    private void ResetWaterAmountDisplay()
    {
        foreach(GameObject wd in waterAmountdisplay)
        {
            wd.GetComponent<MeshRenderer>().material = waterAmountDisplayDefaultMaterial;
        }
    }
}
