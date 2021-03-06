using Ignis;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] igniters;

    [SerializeField]
    private GameObject[] fireAreas;

    [SerializeField]
    private GameObject[] newFireAreas;

    public int timer = 0;
    public int fireActivationTimer = 300;

    private float maximumActiveFireAreas = 1;

    public bool ignitable = true;

    public void ActivateRandomIgniter()
    {
        if (ignitable)
        {
            int ignitersCount = 0;

            //foreach(GameObject i in igniters)
            //{
            //    if (i.activeInHierarchy)
            //    {
            //        ignitersCount++;
            //    }
            //}

            //foreach (GameObject i in fireAreas)
            //{
            //    if (i.GetComponent<FlammableObject>().onFire)
            //    {
            //        ignitersCount++;
            //    }
            //}

            foreach (GameObject i in newFireAreas)
            {
                if (i.GetComponent<FireArea>().active)
                {
                    ignitersCount++;
                }
            }

            if (ignitersCount <= Mathf.FloorToInt(maximumActiveFireAreas))
            {
                if(ignitersCount == Mathf.FloorToInt(maximumActiveFireAreas))
                {
                    ignitable = false;
                }   

                maximumActiveFireAreas = maximumActiveFireAreas + 0.1f;
                
                //GameObject randomIgniter = igniters[Random.Range(0, igniters.Length)];

                newFireAreas[Random.Range(0, newFireAreas.Length)].GetComponent<FireArea>().IgniteFire();

                //randomIgniter.SetActive(true);
                
                //StartCoroutine(DeactivateIgniterDelay(randomIgniter, 5f));
            }
        }
    }

    IEnumerator ActivateIgniterDelay(GameObject igniter, float delay)
    {
        yield return new WaitForSeconds(delay);
        igniter.SetActive(true);
        StartCoroutine(DeactivateIgniterDelay(igniter, 5f));
    }

    IEnumerator DeactivateIgniterDelay(GameObject igniter, float delay)
    {
        yield return new WaitForSeconds(delay);
        igniter.SetActive(false);
        ignitable = true;
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

        if(fireActivationTimer <= 0)
        {
            fireActivationTimer = 250;
            ActivateRandomIgniter();
        }

        fireActivationTimer--;
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
        yield return new WaitForSeconds(0.01f);
        fireArea.SetActive(true);
    }
}
