using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    public WinPanel winPanelScript;
    public DataCreator dataCreatorScript;

    private void Awake()
    {
        
    }

    void Start()
    {
        winPanelScript = FindObjectOfType<WinPanel>();
        dataCreatorScript = FindObjectOfType<DataCreator>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Manager.instance.Win();
            Debug.Log("YESSSSSSSSSSSSSSS");
            winPanelScript.playerWin = true;
            dataCreatorScript.playerStatus = "win";
            dataCreatorScript.playerReason = "Player reached the goal";
            dataCreatorScript.AddToList();
        }
    }
}
