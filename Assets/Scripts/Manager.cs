using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameStates { countDown, running, raceOver };

public class Manager : MonoBehaviour
{
    public static Manager instance;

    GameStates gameState = GameStates.countDown;

    public static bool GameIsPaused = false;



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
    }

    public void Resume()
    {
        Time.timeScale = 1f;
        GameIsPaused = false;
        ButtonClick();
    }


    //----------------------------//

    public void ButtonClick()
    {
        AudioManager.instance.ButtonClick();
    }

    public void Death()
    {
        OnRaceEnd();
        AudioManager.instance.Death();
        Pause();
    }

    public void Win()
    {
        OnRaceEnd();
        AudioManager.instance.Win();
        Pause();
    }


    //----------------------------//

    public void GoToMain()
    {
        Resume();
        SceneManager.LoadSceneAsync(0);
        OnRaceEnd();
    }

    public void QuitGame()
    {
        ButtonClick();
        Application.Quit();
    }

    public void Restart()
    {
        Destroy(gameObject);
        instance = null;
        Resume();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        OnRaceStart();
    }

    public void StartGame()
    {
        Destroy(gameObject);
        instance = null;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        LevelStart();
        Resume();
    }

    //----------------------------//

    public GameStates GetGameState()
    {
        return gameState;
    }

    public void LevelStart()
    {
        gameState = GameStates.countDown;
    }

    public void OnRaceStart()
    {
        gameState = GameStates.running;
    }

    public void OnRaceEnd()
    {
        gameState = GameStates.raceOver;
    }

}
