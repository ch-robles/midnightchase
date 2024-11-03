using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameStates { countDown, running, raceOver, noMaze, mazeFinished, gridFinished};

public class Manager : MonoBehaviour
{
    public static Manager instance;

    GameStates gameState = GameStates.countDown;
    GameStates mazeState = GameStates.noMaze;

    public static bool GameIsPaused = false;

    public int mazeSize, gridSize;
    public string mazeType;

    CSVWriter csvWriter;


    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

    }

    // Start is called before the first frame update
    void Start()
    {
        csvWriter = GameObject.Find("DataManager").GetComponent<CSVWriter>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Pause()
    {
        Time.timeScale = 0f;
        GameIsPaused = true;
        ButtonClick();
        Debug.Log("Paused");
    }

    public void Resume()
    {
        Time.timeScale = 1f;
        GameIsPaused = false;
        ButtonClick();
        Debug.Log("Resumed");
    }


    //----------------------------//

    public void ButtonClick()
    {
        AudioManager.instance.ButtonClick();
        Debug.Log("ButtonClick");
    }

    public void Death()
    {
        OnRaceEnd();
        AudioManager.instance.Death();
        Pause();
        Debug.Log("Death");
        SetMazeState(GameStates.noMaze);
    }

    public void Win()
    {
        OnRaceEnd();
        AudioManager.instance.Win();
        Pause();
        Debug.Log("Win");
    }

    public void Draw()
    {
        OnRaceEnd();
        AudioManager.instance.Win();
        Pause();
        Debug.Log("Draw");
    }


    //----------------------------//

    public void GoToMain()
    {
        Resume();
        SceneManager.LoadSceneAsync(0);
        OnRaceEnd();
        Debug.Log("GoToMain");
        AudioManager.PlayMenuMusic();
    }

    public void QuitGame()
    {
        ButtonClick();
        Application.Quit();
        Debug.Log("Quit");
        csvWriter.quitGame = true;
    }

    public void Restart()
    {
        Destroy(gameObject);
        instance = null;
        Resume();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        // OnRaceStart();
        LevelStart();
        Debug.Log("Restart");
    }

    /*public void StartGame()
    {
        Destroy(gameObject);
        instance = null;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        LevelStart();
        Resume();
        Debug.Log("StartGame");
    }*/

    //----------------------------//

    public GameStates GetGameState()
    {
        return gameState;
    }

    public void LevelStart()
    {
        gameState = GameStates.countDown;
        AudioManager.PlayGameMusic();
    }

    public void OnRaceStart()
    {
        gameState = GameStates.running;
    }

    public void OnRaceEnd()
    {
        gameState = GameStates.raceOver;
    }

    //----------------------------//

    public GameStates GetMazeState()
    {
        return mazeState;
    }

    public void SetMazeState(GameStates _mazeState)
    {
        mazeState = _mazeState;
    }
    

}
