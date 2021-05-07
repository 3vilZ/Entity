using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEvents : MonoBehaviour
{
    public void CoreOff()
    {
        InGameCanvas.Instance.CoreOff();
    }

    public void Startdialogue()
    {
        InGameCanvas.Instance.StartDialogue();
    }

    public void NarrativeOff()
    {
        InGameCanvas.Instance.NarrativeOff();
    }

    public void CollectableOff()
    {
        InGameCanvas.Instance.CollectableOff();
    }
}
