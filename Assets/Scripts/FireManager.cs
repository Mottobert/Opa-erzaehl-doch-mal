using Ignis;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] igniters;


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
            GameObject radnomIgniter = igniters[Random.Range(0, igniters.Length - 1)];
            radnomIgniter.SetActive(true);
            StartCoroutine(DeactivateIgniterDelay(radnomIgniter, 1f));
        }
    }

    IEnumerator DeactivateIgniterDelay(GameObject igniter, float delay)
    {
        yield return new WaitForSeconds(delay);
        igniter.SetActive(false);
    }
}
