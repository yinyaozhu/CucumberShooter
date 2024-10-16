using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketShootStrategy : IShootStrategy
{
    ShootInteractor _interactor;
    Transform _shootPoint;

    public RocketShootStrategy(ShootInteractor interactor)
    {
        Debug.Log("Switch to Bullet Mode");
        _interactor = interactor;
        _shootPoint = interactor.GetShootPoint();

        // change gun color
        _interactor._gunRenderer.material.color = _interactor._rocketGunColor;

    }

    public void Shoot()
    {

        // change to pooling
        PooledObject pooledRocket = _interactor._rocketPool.GetPooledObject();
        pooledRocket.gameObject.SetActive(true);

        Rigidbody rocket = pooledRocket.GetComponent<Rigidbody>();
        rocket.transform.position = _shootPoint.position;
        rocket.transform.rotation = _shootPoint.rotation;

        rocket.velocity = _shootPoint.forward * _interactor.GetShootVelocity();
        //Destroy(bullet.gameObject, 5.0f);
        _interactor._rocketPool.DestroyPooledObject(pooledRocket, 5.0f);
    }
}
