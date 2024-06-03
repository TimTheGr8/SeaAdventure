using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggeredEvent : MonoBehaviour
{
    [Tooltip("Requires the player character to ahve the 'Player' tag assigned.")]
    public bool _isTriggeredByPlayer = true;
    public UnityEvent _onTriggered;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(_isTriggeredByPlayer && !collision.CompareTag(Tags.Player))
            return;

        _onTriggered?.Invoke();
    }
}