using Ignis;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class AnimationController : MonoBehaviour
{
    [SerializeField]
    private GameObject[] fireAnimationControllers;
    [SerializeField]
    private GameObject[] sphereIgniters;

    [SerializeField]
    private Material terrainMaterial;

    [SerializeField]
    private Volume volume;
    private ColorAdjustments thisExposure;

    [SerializeField]
    private Color fogStartColor;
    [SerializeField]
    private Color fogEndColor;

    [SerializeField]
    private GameObject burningFloorTerrain;

    [SerializeField]
    private Terrain treeTerrain;
    [SerializeField]
    private int starTreeDistance;

    private void Start()
    {
        ResetBurning();

        treeTerrain.treeDistance = starTreeDistance;
    }

    public void ResetBurning()
    {
        RenderSettings.skybox.SetFloat("_AtmosphereThickness", 1);
        terrainMaterial.SetFloat("Vector1_5a729d7b72da468d839cfbf65d212a2f", 0);
        RenderSettings.fogDensity = 0.005f;
        RenderSettings.fogColor = fogStartColor;
        volume.profile.TryGet<ColorAdjustments>(out thisExposure);
        thisExposure.postExposure.value = 1.5f;
    }

    public void ActivateBurning()
    {
        //ActivateFlammableObjects();
        ActivateFireAnimationControllers();

        StartCoroutine(ChangeFogDensity(0.02f));
        StartCoroutine(ChangeSkyboxAtmosphereThickness(1f, 4f));
        StartCoroutine(ChangeTerrainMaterialTemperature(0f, 50f));
        StartCoroutine(ChangePostProcessingExposure(1.5f, 0f));
        StartCoroutine(ChangeFogColor(fogStartColor, fogEndColor));
        StartCoroutine(ChangeTreeDistance(50));
        

        Invoke("BurningFloorChangeAlpha", 5f);
    }

    private void BurningFloorChangeAlpha()
    {
        burningFloorTerrain.GetComponent<ChangeMaterialAlpha>().StartChangeAlpha();
    }

    private void ActivateFlammableObjects()
    {
        StartCoroutine(ActivateFlammableObjectsDelay(2, 5));
    }

    IEnumerator ActivateFlammableObjectsDelay(float minDelay, float maxDelay)
    {
        foreach (GameObject si in sphereIgniters)
        {
            si.GetComponent<SphereIgnite>().enabled = true;
            yield return new WaitForSeconds(Random.Range(minDelay, maxDelay));
        }
    }

    private void ActivateFireAnimationControllers()
    {
        StartCoroutine(ActivateFireAnimationControllersDelay(2, 4));
    }

    IEnumerator ActivateFireAnimationControllersDelay(float minDelay, float maxDelay)
    {
        foreach (GameObject fac in fireAnimationControllers)
        {
            fac.GetComponent<FireAnimationController>().ActivateParticleSystems();
            yield return new WaitForSeconds(Random.Range(minDelay, maxDelay));
        }
    }

    IEnumerator ChangeFogDensity(float maxValue)
    {
        float density = 0;

        while(density < maxValue)
        {
            density = density + 0.0001f;
            RenderSettings.fogDensity = density;
            yield return new WaitForSeconds(0.01f);
        }
    }

    IEnumerator ChangeFogColor(Color startColor, Color endColor)
    {
        Color fogColor = startColor;

        int i = 0;

        while (fogColor != endColor || i < 100)
        {
            fogColor = Color.Lerp(fogColor, endColor, 0.1f);
            RenderSettings.fogColor = fogColor;
            i++;
            yield return new WaitForSeconds(0.01f);
        }
    }

    IEnumerator ChangeSkyboxAtmosphereThickness(float start, float end)
    {
        float density = start;

        while (density < end)
        {
            density = density + 0.1f;
            RenderSettings.skybox.SetFloat("_AtmosphereThickness", density);
            yield return new WaitForSeconds(0.06f);
        }
    }

    IEnumerator ChangeTerrainMaterialTemperature(float start, float end)
    {
        float density = start;

        while (density < end)
        {
            density = density + 1f;
            terrainMaterial.SetFloat("Vector1_5a729d7b72da468d839cfbf65d212a2f", density);
            yield return new WaitForSeconds(0.06f);
        }
    }

    IEnumerator ChangePostProcessingExposure(float start, float end)
    {
        float exposure = start;

        while (exposure > end)
        {
            exposure = exposure - 0.1f;

            volume.profile.TryGet<ColorAdjustments>(out thisExposure);
            thisExposure.postExposure.value = exposure;

            yield return new WaitForSeconds(0.1f);
        }
    }

    IEnumerator ChangeTreeDistance(float end)
    {
        float treeDistance = treeTerrain.treeDistance;

        while (treeDistance > end)
        {
            treeDistance = treeDistance - 0.5f;

            treeTerrain.treeDistance = treeDistance;

            yield return new WaitForSeconds(0.1f);
        }
    }
}
