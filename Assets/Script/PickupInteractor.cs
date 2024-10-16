using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupInteractor : Interactor
{
    [SerializeField] private Camera _cam;
    [SerializeField] private LayerMask _pickupLayer;
    [SerializeField] private float _pickupDistance;
    [SerializeField] private Transform _attachTransform;

    private bool isPicked = false;
    private RaycastHit _rayCastHit;
    private IPickable _pickable;

    public override void Interact()
    {
        Ray ray = _cam.ScreenPointToRay(new Vector3(Screen.width/2, Screen.height/2, 0));
        if (Physics.Raycast(ray,out _rayCastHit, _pickupDistance, _pickupLayer))
        {
            //_pickable = _rayCastHit.transform.GetComponent<IPickable>();
            if (_input._pickUpPressed && !isPicked) {

                _pickable = _rayCastHit.transform.GetComponent<IPickable>();// why twice?
                if(_pickable == null) { return; }
                
                _pickable.OnPicked(_attachTransform);
                isPicked = true;
                return;
            }
        }

        if (_input._pickUpPressed && isPicked && _pickable != null)
        {
            _pickable.OnDropped();
            isPicked = false;
        }

    }
}
