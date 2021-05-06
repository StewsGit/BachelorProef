using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotateGuide : MonoBehaviour
{
    [SerializeField] float rotSpeed = 20;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("r"))
        {
            RotateGuide();
        }
    }

    void RotateGuide()
    {
        float rotX = Input.GetAxis("Mouse X") * rotSpeed * Mathf.Deg2Rad;
        float rotY = Input.GetAxis("Mouse Y") * rotSpeed * Mathf.Deg2Rad;
        Debug.Log("rotX = " + rotX + "rotY = " + rotY);
        this.gameObject.transform.Rotate(Vector3.up, -rotX);
        this.gameObject.transform.Rotate(Vector3.right, rotY);
    }
}
