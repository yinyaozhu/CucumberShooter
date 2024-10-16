using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DoorKeyCard : MonoBehaviour
{
    public UnityEvent onKeyPicked;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) { 
            onKeyPicked?.Invoke();
            Destroy(gameObject);
        }
    }
}
