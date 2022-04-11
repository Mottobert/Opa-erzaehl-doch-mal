using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyboxChanger : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        RenderSettings.skybox.SetFloat("_Exposure", 1f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeSkybox()
    {
        StartCoroutine(ChangeSkyboxExposure(1f, 0.1f));
    }

    IEnumerator ChangeSkyboxExposure(float start, float end)
    {
        float exposure = start;

        while (exposure > end)
        {
            exposure = exposure - 0.1f;
            RenderSettings.skybox.SetFloat("_Exposure", exposure);
            yield return new WaitForSeconds(0.06f);
        }
    }
}
