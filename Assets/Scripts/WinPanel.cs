using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinPanel : MonoBehaviour
{
    public GameObject winUI;
    public GameObject inGameUI;

    public bool playerWin = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (playerWin == true){
            winUI.SetActive(true);
            inGameUI.SetActive(false);
            Manager.instance.Win();
            playerWin = false;
        }
    }
}
