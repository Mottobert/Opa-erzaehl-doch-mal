using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class DirtHole : MonoBehaviour
{
    private bool treeSaplingPlaced = false;

    [SerializeField]
    private float waterNeeded;
    [SerializeField]
    private float waterReduction;

    [SerializeField]
    private GameObject treeModel;
    [SerializeField]
    private GameObject treeSapling;

    [SerializeField]
    private PlayableDirector zusammenfassung2Timeline;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "treeSapling")
        {
            treeSaplingPlaced = true;
            Debug.Log("Tree Sapling Placed");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "treeSapling")
        {
            treeSaplingPlaced = false;
        }
    }

    private void OnParticleCollision(GameObject other)
    {
        if(other.tag == "particleWater" && treeSaplingPlaced)
        {
            waterNeeded = waterNeeded - waterReduction;
        }

        if(waterNeeded <= 0)
        {
            Debug.Log("Tree has enough water");

            treeSapling.SetActive(false);

            treeModel.SetActive(true);
            treeModel.GetComponent<Animator>().SetBool("active", true);

            Invoke("PlayZusammenfassung2Timeline", 3f);
        }
    }

    private void PlayZusammenfassung2Timeline()
    {
        zusammenfassung2Timeline.Play();
    }
}
