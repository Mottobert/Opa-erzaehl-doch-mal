using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tornado : MonoBehaviour
{
    [SerializeField]
    private Transform tornadoCenter;
    [SerializeField]
    private float pullForce;
    [SerializeField]
    private float refreshRate;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "tornadoObject")
        {
            StartCoroutine(PullObject(other));
        }
    }

    IEnumerator PullObject(Collider x) 
    {
        Vector3 ForwardDir = tornadoCenter.position - x.transform.position;
        x.GetComponent<Rigidbody>().AddForce(ForwardDir.normalized * pullForce * Time.deltaTime);
        yield return refreshRate;
        StartCoroutine(PullObject(x));
    }

    private void OnTriggerExit(Collider other)
    {
        StopCoroutine("PullObject");
    }
}
