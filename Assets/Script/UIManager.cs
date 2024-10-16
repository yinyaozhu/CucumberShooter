using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Health _playerHealth;

    public TMP_Text _textHealth;
    public GameObject _gameOverText;

    public static UIManager Instance;
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public static UIManager GetInstance()
    {
        return Instance;
    }

    // Start is called before the first frame update
    void Start()
    {
        _gameOverText.SetActive(false);
    }

    private void OnEnable()
    {
        // subscriptions
        _playerHealth.OnHealthUpdated += OnHealthUpdate;
        _playerHealth.OnDeath += OnDeath;
    }

    private void OnDestroy()
    {
        // unsubscribe
        _playerHealth.OnHealthUpdated -= OnHealthUpdate;
    }

    void OnHealthUpdate(float health)
    {
        _textHealth.text = Mathf.Floor(health).ToString();
    }

    void OnDeath() { 
        _gameOverText.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
