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

    private bool active = true;

    [SerializeField]
    private PlayableDirector zusammenfassung2Timeline;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "treeSapling")
        {
            treeSaplingPlaced = true;
            //Debug.Log("Tree Sapling Placed");
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
        if(other.tag == "particleWater" && treeSaplingPlaced && active)
        {
            waterNeeded = waterNeeded - waterReduction;
        }

        if(waterNeeded <= 0 && active)
        {
            //Debug.Log("Tree has enough water");

            treeSapling.SetActive(false);
            Destroy(treeSapling);

            treeModel.SetActive(true);
            treeModel.GetComponent<Animator>().SetBool("active", true);

            Invoke("PlayZusammenfassung2Timeline", 3f);

            active = false;
        }
    }

    private void PlayZusammenfassung2Timeline()
    {
        zusammenfassung2Timeline.Play();
    }
}
