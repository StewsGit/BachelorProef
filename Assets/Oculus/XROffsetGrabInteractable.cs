using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class XROffsetGrabInteractable : XRGrabInteractable
{
    private Vector3 interactorPosition;
    private Quaternion interactorRotation;

    protected override void OnSelectEntering(SelectEnterEventArgs interactor)
    {
        base.OnSelectEntering(interactor);
        StoreInteractor(interactor.interactor);
        MatchAttachmentPoints(interactor.interactor);
    }
    private void StoreInteractor(XRBaseInteractor interactor)
    {
        interactorPosition = interactor.attachTransform.localPosition;
        interactorRotation = interactor.attachTransform.localRotation;
    }
    private void MatchAttachmentPoints(XRBaseInteractor interactor)
    {
        bool hasAttach = attachTransform != null;
        interactor.attachTransform.position = hasAttach ? attachTransform.position : transform.position;
        interactor.attachTransform.rotation = hasAttach ? attachTransform.rotation : transform.rotation;
    }
    protected override void OnSelectExiting(SelectExitEventArgs interactor)
    {
        base.OnSelectExiting(interactor);
        ResetAttachmentMode(interactor.interactor);
        ClearInteractor(interactor.interactor);
    }
    private void ResetAttachmentMode(XRBaseInteractor interactor)
    {
        interactor.attachTransform.localPosition = interactorPosition;
        interactor.attachTransform.localRotation = interactorRotation;
    }

    private void ClearInteractor(XRBaseInteractor interactor)
    {
        interactorPosition = Vector3.zero;
        interactorRotation = Quaternion.identity;
    }
}