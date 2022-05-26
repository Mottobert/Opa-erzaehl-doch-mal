using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bread : MonoBehaviour
{
    [SerializeField]
    public GameObject breadMesh;

    public bool hasSunken = false;

    public void SinkBread()
    {
        hasSunken = true;
        StartCoroutine(SinkBreadDown());
    }

    IEnumerator SinkBreadDown()
    {
        yield return new WaitForSeconds(1);

        float i = 0;
        while (i <= 0.2f)
        {
            yield return new WaitForSeconds(0.00001f);

            breadMesh.transform.position = new Vector3(transform.position.x, breadMesh.transform.position.y - 0.002f, transform.position.z);

            i = i + 0.001f;
        }
    }
}
