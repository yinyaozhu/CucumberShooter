using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;

public class Lv3EventListener : MonoBehaviour
{

    // listeners
    [SerializeField] private RobotSubscriber[] _robotSubscribers;

    // inputs
    [SerializeField] private targetPublisher[] _targetPublishers;

    [SerializeField] private Animator _doorAnimator;
    private void Update()
    {
        checkSubscribers();
    }

    public void notifySubscribers(int[] notifyRobot, bool aSwitch) {
        foreach (int index in notifyRobot) {
            _robotSubscribers[index].subscribeUpdate(aSwitch);
        }
    }

    private void checkSubscribers()
    {
        bool temp = true;
        foreach(RobotSubscriber subscriber in _robotSubscribers)
        {
            temp = temp & subscriber._isMarkActive;
        }

        if (temp)
        {
            EndLevel3();
        }
    }

    private void EndLevel3()
    {
        _doorAnimator.SetBool("Door", true);
    }

}
