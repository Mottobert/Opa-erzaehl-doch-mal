using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Frog : MonoBehaviour
{
    [SerializeField]
    private InputActionReference triggerReference;

    [SerializeField]
    private AudioSource audioSource;

    private bool active = false;

    private float timer = 0;

    private void FixedUpdate()
    {
        if(active && triggerReference.action.ReadValue<float>() >= 0.8f && timer <= 0)
        {
            Quack();
            timer = 250;

        }

        if(timer >= 0)
        {
            timer--;
        }
    }

    public void Quack()
    {
        audioSource.Play();
    }

    public void Activate()
    {
        active = true;
    }

    public void Deactivate()
    {
        active = false;
    }
}
