using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LevelTrigger : MonoBehaviour
{
    [SerializeField] private LevelManager _endingLevel;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _endingLevel.EndLevel();
            Destroy(gameObject);// sometimes need to consider delay cases?
        }
    }
}
