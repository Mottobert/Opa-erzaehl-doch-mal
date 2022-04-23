using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Candle : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem candleFire;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "lighter")
        {
            EnableCandle();
            Invoke("DisableCandle", 20f);
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
