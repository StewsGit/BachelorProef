using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    public bool x;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void A_Button(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
        x = true;

        }

        if (context.canceled)
        {
            x = false;
        }

#if UNITY_EDITOR
        Debug.Log("A Button Pressed");
#endif
    }
}
