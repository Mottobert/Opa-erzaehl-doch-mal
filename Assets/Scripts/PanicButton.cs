using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PanicButton : MonoBehaviour
{
    [SerializeField]
    private InputActionReference panicButtonReference;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        bool input = panicButtonReference.action.ReadValue<bool>();

        if(input)
        {
            Debug.Log("Panic Button Pressed");
        }
    }
}
