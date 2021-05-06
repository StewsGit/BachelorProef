using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed = 5;
    // Use this for initialization
    void Start()
    {
        Cursor.lockState = CursorLockMode.
       Locked;
    }
    // Update is called once per frame
    void Update()
    {
        float Horizontal = Input.
       GetAxis("Horizontal") * speed;
        float Vertical = Input.
       GetAxis("Vertical") * speed;
        Horizontal *= Time.deltaTime;
        Vertical *= Time.deltaTime;
        transform.Translate(Horizontal, 0,
       Vertical);
        if (Input.GetKeyDown("escape"))
            Cursor.lockState = CursorLockMode.
           None;
    }
}
