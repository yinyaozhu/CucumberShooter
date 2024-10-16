using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    [SerializeField] private LevelManager[] _levels;

    public static GameManager Instance;

    private GameState _currentState;
    private LevelManager _currentLevel;
    private int _currentLevelIndex = 0;
    private bool _isInputActive = true;

    public UnityEvent _ifGameOver;
    public UnityEvent _ifGameEnd;

    [SerializeField] private Health _playerHealth;

    public enum GameState
    {
        Briefing,
        LevelStart,
        LevelIn,
        LevelEnd,
        GameOver,
        GameEnd
    }

    private void Awake()
    {
        if (Instance != null && Instance != this) {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public static GameManager GetInstance()
    {
        return Instance;
    }

    public bool IsInputActive() { 
        return _isInputActive;
    }

    public void ChangeState(GameState state, LevelManager level) { 
        _currentLevel = level;
        _currentState = state;

        switch (_currentState) {
            case GameState.Briefing:
                StartBriefing();
                break;

            case GameState.LevelStart:
                InitiateLevel();
                break;

            case GameState.LevelIn:
                RunLevel();
                break;

            case GameState.LevelEnd:
                CompleteLevel();
                break;

            case GameState.GameOver:
                GameOver();
                break;

            case GameState.GameEnd:
                GameEnd();
                break;

        }
    }

    private void Start()
    {
        // go to the first state
        if (_levels.Length > 0)
        {
            // change state
            ChangeState(GameState.Briefing, _levels[_currentLevelIndex]);
        }
    }

    private void StartBriefing() {
        Debug.Log("Briefing");

        _isInputActive = false;

        //Start the level
        ChangeState(GameState.LevelStart, _currentLevel);
    }

    private void InitiateLevel() {
        Debug.Log("Level Starting");

        _isInputActive = true;
        _currentLevel.StartLevel();

        //change state here
        ChangeState(GameState.LevelIn, _currentLevel);
    }

    private void RunLevel() {
        Debug.Log("Level in " + _currentLevel.gameObject.name);
    }

    private void CompleteLevel() {
        Debug.Log("Level End");

        //increment level index -> so this can increase current level index?
        ChangeState(GameState.LevelStart, _levels[++_currentLevelIndex]);
    }

    public void GameOver() {
        Debug.Log("Game over, you lose.");
        _ifGameOver?.Invoke();
    }

    public void GameEnd() {
        Debug.Log("Game over, you win!");
        _ifGameEnd?.Invoke();
    }

}
