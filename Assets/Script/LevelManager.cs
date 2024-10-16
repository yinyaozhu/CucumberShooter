using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private bool _isFinalLevel;

    public UnityEvent _onLevelStart;
    public UnityEvent _onLevelEnd;

    public void StartLevel() { 
        _onLevelStart?.Invoke();
    }

    public void EndLevel() { 
        _onLevelEnd?.Invoke();

        // Inform the game manager that the level is over. Game win if its the final level.
        if (_isFinalLevel)
        {
            GameManager.GetInstance().ChangeState(GameManager.GameState.GameEnd, this);
        }
        else { 
            GameManager.GetInstance().ChangeState(GameManager.GameState.LevelEnd, this);
        }
    }
}
