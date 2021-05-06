using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutOfBounds : MonoBehaviour
{
    public GameObject outOfBoundsWarning;
    public GameObject environment;
    public TypeOutScript TOS;

    List<MeshRenderer> meshesInEnvironment = new List<MeshRenderer>();
    // Start is called before the first frame update
    void Start()
    {
        foreach (MeshRenderer mr in environment.GetComponentsInChildren<MeshRenderer>())
        {
            meshesInEnvironment.Add(mr);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerExit(Collider other)
    {

        if (other.tag == "MainCamera")
        {

            outOfBoundsWarning.SetActive(true);
            //TOS.reset = true;
            TOS.i = 0;
            TOS.On = true;

            foreach (MeshRenderer mr in meshesInEnvironment)
            {
                mr.enabled = false;
            }
        }

    }

    void OnTriggerEnter(Collider other)
    {

        if (other.tag == "MainCamera")
        {
            TOS.reset = true;
            TOS.On = false;
            outOfBoundsWarning.SetActive(false);
            foreach (MeshRenderer mr in meshesInEnvironment)
            {
                mr.enabled = true;
            }
        }
    }
}
