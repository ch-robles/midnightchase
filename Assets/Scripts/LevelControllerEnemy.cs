using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelControllerEnemy : MonoBehaviour
{
    public GameObject deathUI;
    public GameObject inGameUI;
    public DataCreator dataCreatorScript;

    private void Awake()
    {

    }

    void Start()
    {
        dataCreatorScript = FindObjectOfType<DataCreator>();
    }

    void OnCollisionStay(Collision other)
    {
        if (other.collider.tag == "Player")
        {
            deathUI.SetActive(true);
            inGameUI.SetActive(false);
            Manager.instance.Death();
            dataCreatorScript.playerStatus = "lose";
            dataCreatorScript.playerReason = "Enemy caught the player";
            dataCreatorScript.AddToList();
        }
    }
}
