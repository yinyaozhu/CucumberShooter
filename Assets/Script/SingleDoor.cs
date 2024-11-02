using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleDoor : MonoBehaviour
{
    Animator animator;

    [SerializeField] private float _waitTime = 1.0f;
    [SerializeField] private bool _isLocked = true;
    private float _timer = 0.0f;

    private void Start()
    {
        animator = gameObject.transform.GetChild(0).GetComponent<Animator>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (_isLocked) return;

        if (!other.CompareTag("Player")) return;

        _timer += Time.deltaTime;

        if (_timer > _waitTime)
        {
            _timer = _waitTime;
            OpenDoor(true);
        }

    }

    private void OnTriggerExit(Collider other)
    {
        OpenDoor(false);
    }


    public void LockDoor()
    {
        _isLocked = true;
    }

    public void UnlockDoor()
    {
        _isLocked = false;
    }

    public void OpenDoor(bool state)
    {

        if (!_isLocked)
        {
            Debug.Log("OpenDoor: " + state);
            animator.SetBool("DoorStay", state);
        }
    }

}
