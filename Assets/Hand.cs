using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    Animator animator;
    private float gripTarget;
    private float triggerTarget;
    private float secondButtonTarget;
    private float noTriggerYesGripTarget;
    private float gripCurrent;
    private float triggerCurrent;
    private float secondButtonCurrent;
    private float noTriggerYesGripCurrent;
    public float speed;
    private string animatorGripParam = "Grip";
    private string animatorTriggerParam = "Trigger";
    private string animatorSecondButtonParam = "SecondButton";
    private string animatorTriggerGripParam = "Trigger+Grip";

    private bool triggerPressed;
    private bool gripPressed;
    private bool secondButtonPressed;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        AnimateHand();
        noTriggerYesGripTarget = CheckForSoloInput(triggerTarget, gripTarget);

    }

    private void AnimateHand()
    {
        #region Individual Input

        if (gripCurrent != gripTarget)
        {
            gripCurrent = Mathf.MoveTowards(gripCurrent, gripTarget, Time.deltaTime * speed);
            animator.SetFloat(animatorGripParam, gripCurrent);
            gripPressed = checkPressed(gripCurrent);
#if UNITY_EDITOR
            //            Debug.Log("GripPressed :" + gripPressed);
#endif
        }
        if (triggerCurrent != triggerTarget)
        {
            triggerCurrent = Mathf.MoveTowards(triggerCurrent, triggerTarget, Time.deltaTime * speed);
            animator.SetFloat(animatorTriggerParam, triggerCurrent);
            triggerPressed = checkPressed(triggerCurrent);

#if UNITY_EDITOR
            //            Debug.Log("TriggerPressed :" + triggerPressed);
#endif
        }
        if (secondButtonCurrent != secondButtonTarget && !gripPressed && triggerPressed)
        {
            secondButtonCurrent = Mathf.MoveTowards(secondButtonCurrent, secondButtonTarget, Time.deltaTime * speed);
            animator.SetFloat(animatorSecondButtonParam, secondButtonCurrent);
            secondButtonPressed = checkPressed(secondButtonCurrent);

#if UNITY_EDITOR
            //            Debug.Log("SecondButtonPressed: " + secondButtonPressed);
#endif
        }
        else if (!secondButtonPressed || gripPressed || !triggerPressed)
        {
            secondButtonCurrent = Mathf.MoveTowards(secondButtonCurrent, 0, Time.deltaTime * speed);
            animator.SetFloat(animatorSecondButtonParam, secondButtonCurrent);
        }
        #endregion

        #region SoloInput


        if (noTriggerYesGripCurrent != noTriggerYesGripTarget)
        {
            noTriggerYesGripCurrent = Mathf.MoveTowards(noTriggerYesGripCurrent, noTriggerYesGripTarget, Time.deltaTime * speed);
            animator.SetFloat(animatorTriggerGripParam, noTriggerYesGripCurrent);
        }

        #endregion


    }

    #region Methods
    internal float CheckForSoloInput(float first, float second)
    {
        if (first == 0 && second > 0)
        {
            return 1;
        }
        else
        {
            return 0;
        }
    }
    internal float CheckForComboInput(float first, float second)
    {
        if (first > 0 && second > 0)
        {
            return 1;
        }
        else
        {
            return 0;
        }
    }
    internal void SetButton(float secondaryButton)
    {
        secondButtonTarget = secondaryButton;
    }

    internal void SetGrip(float v)
    {
        gripTarget = v;
    }

    internal void SetTrigger(float v)
    {
        triggerTarget = v;
    }

    private bool checkPressed(float value)
    {
        if (value > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    
    #endregion
}
