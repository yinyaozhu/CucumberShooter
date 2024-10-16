using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class targetPublisher : Publisher
{
    [SerializeField] private GameObject exclamationMark;

    // ## TODO, 3 is due to the amount of robot -> can we make it dynamic?
    [SerializeField, Range(0, 3)] private int[] notifyRobot;// the index will notify robots

    [SerializeField] private Lv3EventListener lv3EventListener;

    private bool _isActive;

    private void Start()
    {
        _isActive = false;
        turnOffMark();
    }

    private void turnOnMark() {
        exclamationMark.SetActive(true);    
    }

    private void turnOffMark() {
        exclamationMark.SetActive(false);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet")) {
            notifyListener();
        }
    }

    public override void notifyListener() {
        if (_isActive)
        {
            turnOffMark();
        }
        else
        {
            turnOnMark();
        }
        _isActive = !_isActive;
        lv3EventListener.notifySubscribers(notifyRobot, _isActive);
    }
}
