using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RobotSubscriber : Subscriber
{
    [SerializeField] private GameObject happyMark;
    public bool _isMarkActive;

    void Start()
    {
        _isMarkActive = false;
        turnOffMark();
    }

    public override void subscribeUpdate(bool aSwitch) {
        if (aSwitch)
        {
            turnOnMark();
        }
        else
        {
            turnOffMark();
        }
    }

    private void turnOnMark()
    {
        happyMark.SetActive(true);
        _isMarkActive = true;
    }

    private void turnOffMark()
    {
        happyMark.SetActive(false);
        _isMarkActive = false;
    }

}
