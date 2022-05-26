using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

public class ClimatePlanner : MonoBehaviour
{
    [SerializeField]
    private GameObject[] buttons;

    [SerializeField]
    private Color defaultColor;
    [SerializeField]
    private Color selectedColor;

    private GameObject currentButton;

    public void SelectButton(GameObject activeButton)
    {
        DeselectAllButtons();
        currentButton = activeButton;

        currentButton.GetComponent<Image>().color = selectedColor;

        //activeButton.SetActive(true);
    }

    private void DeselectAllButtons()
    {
        foreach(GameObject b in buttons)
        {
            b.GetComponent<Image>().color = defaultColor;
            //b.SetActive(false);
        }
    }

    private void HideAllButtons()
    {
        foreach (GameObject b in buttons)
        {
            b.SetActive(false);
        }
    }

    public void SelectVorhaben()
    {
        HideAllButtons();

        currentButton.SetActive(true);
        currentButton.GetComponent<Image>().color = defaultColor;

        currentButton.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(0, 30, 0);

        currentButton.GetComponent<VorhabenButton>().timeline.Play();
    }
}
