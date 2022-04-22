using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerHighlight : MonoBehaviour
{
    [SerializeField]
    private GameObject rightGripButton;
    [SerializeField]
    private GameObject rightTriggerButton;

    // Start is called before the first frame update
    void Start()
    {
        //HighlightRightGripButton();
        StartCoroutine(ButtonBlinking(rightGripButton, 1.4f));
        StartCoroutine(ButtonBlinking(rightTriggerButton, 0f));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void HighlightRightGripButton()
    {
        rightGripButton.GetComponent<SkinnedMeshRenderer>().material.SetFloat("Vector1_58daa7a04f41410286e9e6b425c90861", 1);
    }

    IEnumerator ButtonBlinking(GameObject blinking, float delay)
    {
        yield return new WaitForSeconds(delay);

        float value = 0;
        bool positive = true;

        while (true)
        {
            if(value > 0.98f)
            {
                positive = false;
            }

            if(value < 0.02f)
            {
                positive = true;
            }

            if (positive)
            {
                value = value + 0.01f;
            }
            else if (!positive)
            {
                value = value - 0.01f;
            }

            yield return new WaitForSeconds(0.01f);
            blinking.GetComponent<SkinnedMeshRenderer>().material.SetFloat("Vector1_58daa7a04f41410286e9e6b425c90861", value);
        }
    }
}
