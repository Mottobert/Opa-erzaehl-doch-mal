using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirBalloon : MonoBehaviour
{
    public float airAmount = 0;
    private Animator animator;
    private bool decreaseAir = true;

    [SerializeField]
    private AudioSource audioSource;
    [SerializeField]
    private Color[] colors;

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
            audioSource.Play();

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

            if(airAmount > 15)
            {
                airAmount = 15;
                UpdateSize();
                animator.SetBool("active", true);
                decreaseAir = false;
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
        airAmount = 0;
        UpdateSize();
        animator.SetBool("active", false);
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
