using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class HideHand : MonoBehaviour
{
    public SkinnedMeshRenderer handMesh;
    public void ToggleHand()
    {
        handMesh.enabled = !handMesh.enabled;
        XRDirectInteractor directInteractor = this.gameObject.GetComponent<XRDirectInteractor>();
        
    }
    public void SetParent()
    {

        //this.gameObject.transform.SetParent();
    }
}
