using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class PickUpObject : MonoBehaviour
{
    [SerializeField] LayerMask pickableObjectsLayer;
    [SerializeField] float throwForce = 50;
    [SerializeField] float grabDistance = 3;
    [SerializeField] float duration;
    Vector3 objectPos;

    public RaycastHit rayHit;
    public bool canHold = true;
    [SerializeReference] GameObject tempParent;
    private Vector3 guidePos;
    public bool isHolding = false;
    GameObject item;

    void Update()
    {
        if (Input.GetButtonDown("Use"))
        {
            if (isHolding == false)
            {
                GrabObject();
            }
        }
       // distance = Vector3.Distance(item.transform.position, tempParent.transform.position);
        //if (distance >= 1f)
        //{
        //    isHolding = false;
        //}
        //Check if isHolding is true
        if (isHolding)
        {
            if (Input.GetMouseButtonDown(1))
            {

                item.transform.SetParent(null);
                item.GetComponent<Rigidbody>().useGravity = true;
                item.GetComponent<Rigidbody>().isKinematic = false;
                item.GetComponent<Rigidbody>().AddForce(tempParent.transform.forward * throwForce);
                Physics.IgnoreCollision(item.GetComponent<Collider>(), GetComponent<Collider>(), false);
                isHolding = false;
            }

            if (Input.GetMouseButtonDown(2))
            {
                item.transform.SetParent(null);
                item.GetComponent<Rigidbody>().useGravity = true;
                item.GetComponent<Rigidbody>().isKinematic = false;
                isHolding = false;
                Physics.IgnoreCollision(item.GetComponent<Collider>(), GetComponent<Collider>(), false);  // cant collide with player if is holding item.

            }

        }
        else if (item != null)
        {
            objectPos = item.transform.position;
            item.transform.SetParent(null);
            item.GetComponent<Rigidbody>().useGravity = true;
            item.GetComponent<Rigidbody>().isKinematic = false;
            Physics.IgnoreCollision(item.GetComponent<Collider>(), GetComponent<Collider>(), false);
            item.transform.position = objectPos;
        }


    }

    private void GrabObject()
    {
        Debug.Log("mouse down");
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out rayHit, grabDistance, pickableObjectsLayer))
        {
            Debug.Log(rayHit.collider.name);
            item = rayHit.collider.gameObject;
            isHolding = true;
            item.transform.SetParent(tempParent.transform);
            item.GetComponent<Rigidbody>().useGravity = false;
            item.GetComponent<Rigidbody>().isKinematic = true;
            item.GetComponent<Rigidbody>().detectCollisions = true;
            StartCoroutine(LerpPosition(tempParent.transform.position, Quaternion.Euler(tempParent.transform.rotation.eulerAngles), duration, item));
            Physics.IgnoreCollision(item.GetComponent<Collider>(), GetComponent<Collider>(), true);
        }
    }
    IEnumerator LerpPosition(Vector3 targetPosition, Quaternion endValue, float duration,GameObject objectToMove)
    {
        float time = 0;
        Vector3 startPosition = objectToMove.transform.position;
        Quaternion startValue = objectToMove.transform.rotation;

        while (time < duration)
        {
            objectToMove.transform.position = Vector3.Lerp(startPosition, tempParent.transform.position, time / duration);
            objectToMove.transform.rotation = Quaternion.Lerp(startValue, tempParent.transform.rotation, time / duration);
            time += Time.deltaTime;
            yield return null;
        }

        objectToMove.transform.position = tempParent.transform.position;
        objectToMove.transform.rotation = tempParent.transform.rotation;
    }

    void OnMouseUp()
    {
        isHolding = false;
    }
}
