using System;
using UnityEngine;

public class AnimationEventListener : MonoBehaviour
{
    private Action<int> OnEventFired;

    public void EventFired(int eventID)
    {
        OnEventFired.Invoke(eventID);
    }

    public void SetListener(Action<int> OnEvent)
    {
        OnEventFired = OnEvent;
    }
}
