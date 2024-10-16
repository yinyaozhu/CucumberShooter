using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float _maxHealth;
    //[SerializeField] private float _minHealth;

    public Action<float> OnHealthUpdated; // where this function comes from? didnt see a selection in inspector
    public Action OnDeath;

    public bool isDead {  get; private set; }
    public float _health;

    private GameManager gameManager;

    private void Awake()
    {
        gameManager = GameManager.GetInstance();
    }
    private void Start()
    {
        _health = _maxHealth;
        OnHealthUpdated(_maxHealth);
    }

    public void DeductHealth(float value)
    {
        if (isDead) return;

        _health -= value;
        if (_health < 0 )
        {
            isDead = true;
            OnDeath(); // this is an action
            _health = 0;
            gameManager.GameOver();
        }

        OnHealthUpdated(_health);

    }

}
