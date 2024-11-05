using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class Timer : MonoBehaviour
{
    public TMP_Text text;

    public float currentTime;

    public float timeLimit = 0f;

    public GameObject drawUI;
    public GameObject inGameUI;
    public DataCreator dataCreatorScript;

    // Start is called before the first frame update
    void Start()
    {
        dataCreatorScript = FindObjectOfType<DataCreator>();

        if(dataCreatorScript.chosenSize == 20){
            currentTime = 300f; //5mins
        }
        else if(dataCreatorScript.chosenSize == 35){
            currentTime = 480f; //8mins
        }
        else if(dataCreatorScript.chosenSize == 50){
            currentTime = 600f; //10mins
        } else {
            currentTime = 10000f;
            // this is for the tutorial
        }
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Manager.instance.GetGameState() == GameStates.running)
        {
            currentTime -= Time.deltaTime;
            
            if (currentTime <= timeLimit)
            {
                OnTimeLimitReached();
            }
            else
            {
                text.text = FormatTime(currentTime);
            }
        }

    }

    void OnTimeLimitReached()
    {
       
        currentTime = timeLimit;
        text.text = FormatTime(currentTime);
        Debug.Log("Time limit reached!");

        drawUI.SetActive(true);
        inGameUI.SetActive(false);
        Manager.instance.Draw();
        dataCreatorScript.playerStatus = "draw";
        dataCreatorScript.playerReason = "Both ran out of time";
        dataCreatorScript.AddToList();
        
    }

    string FormatTime(float timeInSeconds)
    {
        int minutes = Mathf.FloorToInt(timeInSeconds / 60F);
        int seconds = Mathf.FloorToInt(timeInSeconds % 60F);
        int centiseconds = Mathf.FloorToInt((timeInSeconds * 100F) % 100F);

        return string.Format("{0:00}:{1:00}:{2:00}", minutes, seconds, centiseconds);
    }


}
