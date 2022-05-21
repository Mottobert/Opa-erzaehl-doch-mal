using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClimatePlanner : MonoBehaviour
{
    [SerializeField]
    private GameObject[] buttons;

    [SerializeField]
    private Color defaultColor;
    [SerializeField]
    private Color selectedColor;

    public void SelectButton(GameObject activeButton)
    {
        DeselectAllButtons();

        //activeButton.GetComponent<Image>().color = selectedColor;

        activeButton.SetActive(true);
    }

    private void DeselectAllButtons()
    {
        foreach(GameObject b in buttons)
        {
            //b.GetComponent<Image>().color = defaultColor;
            b.SetActive(false);
        }
    }
}
