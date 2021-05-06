using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JunkJet : MonoBehaviour
{
    public float speed, height;
    public GameObject[] junk;
    public GameObject[] jets;
    GameObject spawnpoint;
    GameObject spawnedFood;

    GameObject instantiated;
    public GameObject target;

    public float spawntime = (60 / 180) * 2;


    public float ANIMATION_DURATION = 2.0f;
    public float FRAMES_PER_SECOND = 30.0f;
    public GameObject one;
    public GameObject two;
    // Start is called before the first frame update
    void Start()
    {
        //spawntime = 10;
        InvokeRepeating("Spawn", spawntime, spawntime);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void Spawn()
    {
        Quaternion randomRotation = Quaternion.Euler(Random.Range(0.0f, 360.0f), Random.Range(0.0f, 360.0f), Random.Range(0.0f, 360.0f));
        spawnpoint = jets[Random.Range(0, jets.Length)];
        spawnedFood = junk[Random.Range(0, junk.Length)];

        instantiated = Instantiate(spawnedFood, spawnpoint.transform.position, randomRotation);
        instantiated.GetComponent<Rigidbody>().AddForce(new Vector3(target.transform.position.x - transform.position.x, height,target.transform.position.z - transform.position.z) * speed);
    }

}
