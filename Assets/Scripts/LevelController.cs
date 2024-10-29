using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    public WinPanel winPanelScript;
    private void Awake()
    {
        
    }

    void Start()
    {
        winPanelScript = FindObjectOfType<WinPanel>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("YESSSSSSSSSSSSSSS");
            winPanelScript.playerWin = true;
        }
    }
}
