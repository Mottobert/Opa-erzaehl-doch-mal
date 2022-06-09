using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Airpump : MonoBehaviour
{
    [SerializeField]
    private AirBalloon balloon;

    private Vector3 startPosition;

    private bool active = true;

    public bool outsideActive = false;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.transform.position.y < startPosition.y - 0.2f && active)
        {
            if (balloon.airAmount < 16)
            {
                balloon.IncreaseAirAmount();
            }

            active = false;
        }

        if (gameObject.transform.position.y > startPosition.y)
        {
            active = true;
        }
    }

    public void DeactivatePump()
    {
        outsideActive = false;
    }

    public void ActivatePump()
    {
        outsideActive = true;
    }
}
