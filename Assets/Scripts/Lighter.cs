using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class Lighter : MonoBehaviour
{
    public bool active = false;

    [SerializeField]
    private InputActionReference triggerReference;
    [SerializeField]
    private ParticleSystem lighterPS;
    [SerializeField]
    private ParticleSystem sparksPS;
    [SerializeField]
    private ActionBasedController rightController;

    private Vector3 maxScale;
    public bool ignited = false;

    private void Start()
    {
        maxScale = lighterPS.gameObject.transform.localScale;
        lighterPS.gameObject.transform.localScale = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        if (active)
        {
            float triggerInput = triggerReference.action.ReadValue<float>();
            UpdateLighter(triggerInput);
        }
    }

    private void UpdateLighter(float input)
    {
        //var main = lighterPS.main;
        //main.startSizeMultiplier = input;

        if (ignited)
        {
            lighterPS.gameObject.transform.localScale = Vector3.Lerp(Vector3.zero, maxScale, input);
        }

        if(input > 0.7f && !ignited)
        {
            IgniteLighter();
        }

        if(input < 0.1f && ignited)
        {
            ignited = false;
            lighterPS.Stop();
            lighterPS.gameObject.transform.localScale = Vector3.zero;
        }
    }

    private void IgniteLighter()
    {
        rightController.SendHapticImpulse(0.8f, 0.1f);
        sparksPS.Emit((Random.Range(0,5)));
        lighterPS.Play();
        ignited = true;
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

    public void ActivateLighter()
    {
        active = true;
        lighterPS.Play();
        //lighterPS.gameObject.transform.localScale = new Vector3(0, 0, 0);
    }

    public void DeactivateLighter()
    {
        active = false;
        lighterPS.Stop();
        //lighterPS.gameObject.transform.localScale = new Vector3(0, 0, 0);
    }
}
