using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIHandler : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ButtonClick()
    {
        AudioManager.instance.ButtonClick();
    }

    public void GoToMain()
    {
        Manager.instance.GoToMain();
    }

    public void QuitGame()
    {
        Manager.instance.QuitGame();
    }

    public void Restart()
    {
        Manager.instance.Restart();
    }

    public void StartGame()
    {
        Manager.instance.StartGame();
    }
}
