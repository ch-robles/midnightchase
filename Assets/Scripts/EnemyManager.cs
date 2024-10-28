using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState { countDown, running, raceOver };

public class EnemyManager : MonoBehaviour
{
    GameState gameState = GameState.countDown;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public GameState GetGameState()
    {
        return gameState;
    }

    public void LevelStart()
    {
        gameState = GameState.countDown;
    }

    public void OnRaceStart()
    {
        gameState = GameState.running;
    }

    public void OnRaceEnd()
    {
        gameState = GameState.raceOver;
    }
}
