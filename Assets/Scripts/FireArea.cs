using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireArea : MonoBehaviour
{
    [SerializeField]
    private float extinguishAmount;
    [SerializeField]
    private float extinguishReduction;
    [SerializeField]
    private float extinguishAddition;

    private float currentExtinguishAmount;

    [SerializeField]
    private int updateFrequency;

    private int updateTimer = 0;

    [SerializeField]
    private GameObject fireParticleSystem;

    [SerializeField]
    private FireManager fireManager;

    [SerializeField]
    private AudioSource fireSound;

    public bool active;

    // Start is called before the first frame update
    void Start()
    {
        currentExtinguishAmount = extinguishAmount;

        //IgniteFire();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (updateTimer >= updateFrequency && currentExtinguishAmount < extinguishAmount && active)
        {
            currentExtinguishAmount = currentExtinguishAmount + extinguishAddition;
        }

        updateTimer++;
    }

    private void OnParticleCollision(GameObject other)
    {
        //Debug.Log(currentExtinguishAmount);

        if (other.gameObject.tag == "particleWater" && currentExtinguishAmount >= 0 && active)
        {
            currentExtinguishAmount = currentExtinguishAmount - extinguishReduction;
        }
        
        if (other.gameObject.tag == "particleWater" && currentExtinguishAmount <= 0 && active)
        {
            ExtinguishFire();
        }
    }

    public void IgniteFire()
    {
        fireParticleSystem.GetComponent<ParticleSystem>().Play();
        active = true;

        currentExtinguishAmount = extinguishAmount;

        fireSound.Play();
    }

    private void ExtinguishFire()
    {
        fireParticleSystem.GetComponent<ParticleSystem>().Stop();
        active = false;

        fireSound.Stop();

        fireManager.gameObject.SetActive(true);

        StartCoroutine(ActivateRandomIgniterDelay(5f));
    }

    IEnumerator ActivateRandomIgniterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        fireManager.ActivateRandomIgniter();
        fireManager.ignitable = true;
    }
}