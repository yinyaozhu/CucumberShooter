using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootInteractor : Interactor
{
    [SerializeField] private Input _inputType;

    [Header("Gun")]
    public MeshRenderer _gunRenderer;
    public Color _bulletGunColor;
    public Color _rocketGunColor;

    [Header("Shoot")]
    public ObjectPool _bulletPool;
    public ObjectPool _rocketPool;

    [SerializeField] private float _shootVelocity;
    [SerializeField] private Transform _shootPoint;
    [SerializeField] private PlayermovementBehaviour _movementBehaviour;

    private float _finalShootVelocity;
    private IShootStrategy _currentShootStrategy;

    public enum Input
    {
        Primary,
        Secondary
    }


    public override void Interact()
    {
        // Default shoot Strat
        if (_currentShootStrategy == null)
        {
            _currentShootStrategy = new BulletShootStrategy(this);
        }

        //Change Strats 
        if (_input._weapon1Pressed)
        {
            _currentShootStrategy = new BulletShootStrategy(this);
        }

        if (_input._weapon2Pressed)
        {
            _currentShootStrategy = new RocketShootStrategy(this);
        }


        // Shoot with the selected Strat
        if (_input._primaryShootPressed && _currentShootStrategy != null)
        {
            _currentShootStrategy.Shoot();
        }
        // Add secondary Shoot Pressed

        // missing secondary shoot strat here ## TODO


    }

    public float GetShootVelocity()
    {
        _finalShootVelocity = _movementBehaviour.GetForwardSpeed() + _shootVelocity;
        return _finalShootVelocity;
    }
    public Transform GetShootPoint()
    {
        return _shootPoint;
    }

}
