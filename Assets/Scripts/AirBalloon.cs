using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirBalloon : MonoBehaviour
{
    public float airAmount = 0;
    private Animator animator;
    public bool decreaseAir = true;

    [SerializeField]
    private AudioSource audioSourcePop;
    [SerializeField]
    private AudioSource audioSourceFillIn;
    [SerializeField]
    private Color[] colors;

    [SerializeField]
    private GameObject descriptionCanvas;

    private int colorIndex;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();

        SelectRandomColor();
    }

    // Update is called once per frame
    //void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.B))
    //    {
    //        IncreaseAirAmount();
    //    }
    //}

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "wall")
        {
            audioSourcePop.Play();

            Invoke("ResetAirBalloon", 3f);
        }
    }

    private void FixedUpdate()
    {
        if (airAmount > 0 && decreaseAir)
        {
            DecreaseAirAmount();
        }
    }

    private void DecreaseAirAmount()
    {
        airAmount = airAmount - 0.005f;

        UpdateSize();
    }

    public void IncreaseAirAmount()
    {
        if(airAmount < 15)
        {
            airAmount++;
            audioSourceFillIn.Play();

            if (airAmount > 15)
            {
                airAmount = 15;
                UpdateSize();
                animator.SetBool("active", true);
                decreaseAir = false;

                descriptionCanvas.SetActive(false);
            }

            UpdateSize();
        }
    }

    private void UpdateSize()
    {
        this.gameObject.transform.localScale = new Vector3(airAmount, airAmount, airAmount);
    }

    private void ResetAirBalloon()
    {
        animator.SetBool("active", false);
        airAmount = 0;
        UpdateSize();

        Invoke("ResetAirBalloonFillIn", 0.2f);
    }

    private void ResetAirBalloonFillIn()
    {
        airAmount = 0;
        UpdateSize();
        decreaseAir = true;
        SelectRandomColor();
    }

    private void SelectRandomColor()
    {
        int newColorIndex = UnityEngine.Random.Range(0, colors.Length);

        while (colorIndex == newColorIndex)
        {
            newColorIndex = UnityEngine.Random.Range(0, colors.Length);
        }
        
        colorIndex = newColorIndex;

        gameObject.GetComponent<MeshRenderer>().material.color = colors[colorIndex];
    }
}
