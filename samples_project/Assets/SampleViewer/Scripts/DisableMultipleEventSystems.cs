using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DisableMultipleEventSystems : MonoBehaviour
{
    void Awake()
    {
        // If there are multiple EventSystems after we add the new scene disable them
        var EventSystems = FindObjectsOfType<EventSystem>();
        
        if (EventSystems.Length == 1)
        {
            EventSystems[0].enabled = true;
        }
    }
}
