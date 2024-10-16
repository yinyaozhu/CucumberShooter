using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PooledObject : MonoBehaviour
{
    [SerializeField] private UnityEvent OnRest;

    ObjectPool _associatedPool;

    private float _timer;
    private bool _setToDestroy = false;
    private float _destroyTime = 0;

    public void SetObjectPool(ObjectPool pool)
    {
        _associatedPool = pool;
        _timer = 0;
        _destroyTime = 0;
        _setToDestroy = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (_setToDestroy) {
            _timer += Time.deltaTime;

            if (_timer >= _destroyTime) {
                _setToDestroy = false;
                _timer = 0;
                _setToDestroy = false;
                Destroy();
            }
        }
    }

    public void ResetObject()
    {
        OnRest?.Invoke();

    }

    public void Destroy() {
        if (_associatedPool != null) {
            _associatedPool.RestoreObject(this);
        }
    }

    public void Destroy(float time) {
        _setToDestroy = true;
        _destroyTime = time;
    }

}
