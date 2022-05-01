using Ignis;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] igniters;

    public int timer = 0;

    public void ActivateRandomIgniter()
    {
        int ignitersCount = 0;

        foreach(GameObject i in igniters)
        {
            if (i.activeInHierarchy)
            {
                ignitersCount++;
            }
        }

        if(ignitersCount <= 1)
        {
            GameObject randomIgniter = igniters[Random.Range(0, igniters.Length)];
            randomIgniter.SetActive(true);
            Debug.Log(randomIgniter);
            StartCoroutine(DeactivateIgniterDelay(randomIgniter, 5f));
        }

        StartCoroutine(ActivateIgniterDelay(Random.Range(5f, 15f)));
    }

    IEnumerator ActivateIgniterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        ActivateRandomIgniter();
    }

    IEnumerator DeactivateIgniterDelay(GameObject igniter, float delay)
    {
        yield return new WaitForSeconds(delay);
        igniter.SetActive(false);
    }

    public void StartFireSound(GameObject fireAudioSource)
    {
        fireAudioSource.GetComponent<AudioSource>().Play();
    }

    public void StopFireSound(GameObject fireAudioSource)
    {
        fireAudioSource.GetComponent<AudioSource>().Stop();
    }

    private void FixedUpdate()
    {
        if(timer > 0)
        {
            timer--;
        }
        
    }

    public void ResetFireArea(GameObject fireArea)
    {
        if(timer <= 0)
        {
            StartCoroutine(ReactivateFireArea(fireArea));
            timer = 300;
        }
    }

    IEnumerator ReactivateFireArea(GameObject fireArea)
    { 
        yield return new WaitForSeconds(1f);
        fireArea.SetActive(false);
        yield return new WaitForSeconds(0.1f);
        fireArea.SetActive(true);
    }
}
