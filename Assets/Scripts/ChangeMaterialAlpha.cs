using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMaterialAlpha : MonoBehaviour
{
    private float alpha = 0;

    // Start is called before the first frame update
    void Start()
    {
        ChangeAlpha(0);
        //Invoke("ChangeAlphaOverTime", 3f);
        StartCoroutine(ChangeAlphaOverTime());
    }

    public void ChangeAlpha(float newAlpha)
    {
        GetComponent<Terrain>().materialTemplate.SetFloat("Vector1_1a9f23ea7f93457bbd2ad37ba62472f5", newAlpha);
    }

    IEnumerator ChangeAlphaOverTime()
    {
        while(alpha < 1)
        {
            yield return new WaitForSeconds(0.1f);
            alpha = alpha + 0.01f;
            Debug.Log(alpha);

            ChangeAlpha(alpha);
        }
    }
}
