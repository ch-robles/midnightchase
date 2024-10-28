using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    public GameObject winUI;
    public GameObject inGameUI;


    private void Awake()
    {
        
    }

    void Start()
    {
        winUI = GameObject.FindWithTag("Win");
        inGameUI = GameObject.FindWithTag("inGameUI"); 
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("YESSSSSSSSSSSSSSS");
            winUI.SetActive(true);
            inGameUI.SetActive(false);
            Manager.instance.Win();
        }
    }
}
