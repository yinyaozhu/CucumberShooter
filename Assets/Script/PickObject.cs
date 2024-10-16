using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickObject : MonoBehaviour, IPickable
{
    FixedJoint _joint;
    Rigidbody _objectRb;

    // Start is called before the first frame update
    void Start()
    {
        _objectRb = GetComponent<Rigidbody>();
    }

    public void OnPicked(Transform attachPoint) {
        transform.position = attachPoint.position;
        transform.rotation = attachPoint.rotation;
        transform.SetParent(attachPoint);

        _objectRb.isKinematic = true;
        _objectRb.useGravity = false;
    }

    public void OnDropped()
    {
        Destroy(_joint);
        _objectRb.isKinematic = false;
        _objectRb.useGravity = true;
        transform.SetParent(null);
    }

}
