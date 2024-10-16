using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleDoorClose : MonoBehaviour
{
    [SerializeField] private Animator _doorAnimator;
    [SerializeField] private GameObject _objToDelete;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) {
            _doorAnimator.SetBool("Door", false);
            if (_objToDelete != null) Destroy(_objToDelete);
        }
        
    }
}
