using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditPanel : MonoBehaviour
{
    private int siteIndex = 0;

    [SerializeField]
    private GameObject[] sites;

    private void Start()
    {
        DisableAllSites();

        ShowSite(siteIndex);
    }

    public void ShowNextSite()
    {
        if(siteIndex < sites.Length - 1)
        {
            siteIndex++;

            DisableAllSites();
            ShowSite(siteIndex);
        }
    }

    public void ShowPreviousSite()
    {
        if(siteIndex > 0)
        {
            siteIndex--;

            DisableAllSites();
            ShowSite(siteIndex);
        }
    }

    private void ShowSite(int index)
    {
        sites[index].SetActive(true);
    }

    private void DisableAllSites()
    {
        foreach(GameObject s in sites)
        {
            s.SetActive(false);
        }
    }
}
