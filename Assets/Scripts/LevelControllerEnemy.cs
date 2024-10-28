using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelControllerEnemy : MonoBehaviour
{
    public GameObject deathUI;
    public GameObject inGameUI;


    private void Awake()
    {

    }

    void Start()
    {

    }

    void OnCollisionStay(Collision other)
    {
        if (other.collider.tag == "Player")
        {
            deathUI.SetActive(true);
            inGameUI.SetActive(false);
            Manager.instance.Death();
        }
    }
}
