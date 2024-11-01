using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataCreator : MonoBehaviour
{
    public Manager manager;
    public CSVWriter csvWriter;

    public int i;
    public string chosenType;
    public int chosenSize;
    public int maxDistance;
    public string playerStatus;
    public string playerReason;
    

    void Start(){
        // Find and assign CSVWriter from DataManager GameObject
        csvWriter = GameObject.Find("DataManager").GetComponent<CSVWriter>();

        manager = FindObjectOfType<Manager>();
        i = 1;

    }


    public void AddToList(){
        if (manager.GetGameState() == GameStates.raceOver){

            // reating and adding a new entry
            CSVWriter.Player newList = new CSVWriter.Player
            {
                testNum = i++,
                mazeType = chosenType,
                mazeSize = chosenSize,
                maxDis = maxDistance,
                status = playerStatus,
                reason = playerReason
            };

            // Add to the PlayerList
            csvWriter.playerList.Add(newList);
            Debug.Log("Added to list" + newList);
        }
    } 
}
