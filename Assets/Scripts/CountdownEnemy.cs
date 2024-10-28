using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountdownEnemy : MonoBehaviour
{
    EnemyManager enemy;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CountdownCO());
        enemy = GetComponent<EnemyManager>();
    }

    IEnumerator CountdownCO()
    {
        yield return new WaitForSeconds(0.3f);

        //5, 10, 15
        int counter = 10;

        while (true)
        {
            if (counter != 0)
            {

            }
            else
            {
                enemy.OnRaceStart();
                break;
            }

            counter--;
        }

    }
}