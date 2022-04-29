using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Candle : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem candleFire;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "lighter" && other.GetComponent<Lighter>().ignited)
        {
            EnableCandle();
            Invoke("DisableCandle", 5f);
        }
    }

    private void EnableCandle()
    {
        candleFire.Play();
    }

    private void DisableCandle()
    {
        candleFire.Stop();
    }
}
