using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyActivator : MonoBehaviour
{
    [SerializeField] GameObject enemyObject;

    void Start(){
        enemyObject.SetActive(false);
        StartCoroutine(EnemyCountdown());
    }

    IEnumerator EnemyCountdown(){
        Debug.Log("Started level at timestamp: " + Time.time);
        
        int seconds = 5;

        if (Manager.instance.mazeSize == 20){

            // yield return new WaitForSeconds(5);
            // Debug.Log("Current mazesize is: " + Manager.instance.mazeSize);
            seconds = 5;

        } else if (Manager.instance.mazeSize == 35){
            // yield return new WaitForSeconds(10);
            // Debug.Log("Current mazesize is: " + Manager.instance.mazeSize);
            seconds = 10;

        } else if (Manager.instance.mazeSize == 50){

            // yield return new WaitForSeconds(20);
            // Debug.Log("Current mazesize is: " + Manager.instance.mazeSize);
            seconds = 20;
        }

        yield return new WaitForSeconds(seconds);

        enemyObject.SetActive(true);
        Debug.Log("Deployed enemy at timestamp: " + Time.time);
    }
}
