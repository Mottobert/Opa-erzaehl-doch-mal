using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    [SerializeField]
    private GameObject canvasBlend;

    private void Start()
    {
        canvasBlend.GetComponent<Animator>().SetBool("active", false);
    }

    public void ChangeScene(int index)
    {
        canvasBlend.GetComponent<Animator>().SetBool("active", true);

        StartCoroutine(ChangeSceneDelay(index, 2f));
    }

    IEnumerator ChangeSceneDelay(int index, float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(index);
    }
}
