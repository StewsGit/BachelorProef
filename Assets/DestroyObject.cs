using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObject : MonoBehaviour
{
    bool spawned = false;
    public int breakpoint;

    public GameObject destroyedVersion;
    private void Start()
    {

    }
    private void OnMouseDown()
    {
        Instantiate(destroyedVersion, transform.position, transform.rotation);
        Destroy(gameObject);
    }

    private void MakeSingleObjects()
    {
        GameObject[] everyChildInDestroyed = destroyedVersion.GetComponentsInChildren<GameObject>();

        for (int i = 0; i < destroyedVersion.GetComponentsInChildren<GameObject>().Length; i++)
        {
            everyChildInDestroyed[i].transform.SetParent(null);
        }
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.relativeVelocity.magnitude > breakpoint)
        {
            Debug.Log(spawned);
            if (spawned) return;
            spawned = true;

            Vector3 initialVelocity = this.GetComponent<Rigidbody>().velocity;
            Vector3 initialAngularVelocity = this.GetComponent<Rigidbody>().angularVelocity;
                ///transform.InverseTransformDirection(this.GetComponent<Rigidbody>().velocity);
            Debug.Log("initialSpeed = " + initialVelocity);
            
            GameObject BrokenObject = Instantiate(destroyedVersion, transform.position, transform.rotation);
            Destroy(gameObject);

            Rigidbody [] rb = BrokenObject.GetComponentsInChildren<Rigidbody>();

            for (int i = 0; i < rb.Length; i++)
            {
                rb[i].velocity = initialVelocity;
                rb[i].angularVelocity = initialAngularVelocity;
                Debug.Log(rb[i].gameObject.name + " RigidBodySpeed = " + rb[i].velocity);
                //rb[i].AddForce(initialSpeed);
            }
            //foreach (Rigidbody rigidBody in destroyedVersion.GetComponentsInChildren<Rigidbody>())
            //{
            //    rigidBody.AddForce(150 * Vector3.up);
            //    Debug.Break();
            //    //   rigidBody.velocity = initialSpeed*10;

            //}


        }
    }
}

