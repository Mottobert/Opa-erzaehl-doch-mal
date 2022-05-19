using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class ForestWaterChecker : MonoBehaviour
{
    [SerializeField]
    private AnimationController animationController;

    [SerializeField]
    public float waterAmount;
    [SerializeField]
    private float waterAmountIncrease;
    [SerializeField]
    private float waterAmountMax;
    [SerializeField]
    private float waterAmountThreshold;

    [SerializeField]
    private PlayableDirector zusammenfassungTimeline;

    private bool active = true;

    private void OnParticleCollision(GameObject other)
    {
        if(other.tag == "particleWater" && active)
        {
            waterAmount = waterAmount + waterAmountIncrease;

            if(waterAmount >= waterAmountThreshold)
            {
                float input = map(waterAmount, waterAmountThreshold, waterAmountMax, 0, 1);

                animationController.FadeLeafAlpha(input);
                animationController.FadeTreeColor(input);
            }

            if(waterAmount >= waterAmountMax)
            {
                zusammenfassungTimeline.Play();

                active = false;
            }
        }
    }

    public float map(float s, float a1, float a2, float b1, float b2)
    {
        return b1 + (s - a1) * (b2 - b1) / (a2 - a1);
    }
}
