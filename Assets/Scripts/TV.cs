using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TV : MonoBehaviour
{
    public GameObject cableEnd;
    public GameObject libretro;
    public GameObject LCD;
    public Material displayMat;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {

        float dist = Vector3.Distance(cableEnd.transform.position, transform.position);

        if (dist > 2.5)
        {
            cableEnd.GetComponent<Rigidbody>().isKinematic = false;
            libretro.SetActive(false);
            LCD.GetComponent<MeshRenderer>().material = displayMat;
            print("Distance to other: " + dist);
        }

    }
    private void LateUpdate()
    {

    }
}
