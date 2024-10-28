using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CountdownUIHandler : MonoBehaviour
{
    public TMP_Text countDownText;

    void Awake()
    {
        countDownText.text = "";
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CountdownCO());
    }

    IEnumerator CountdownCO()
    {
        yield return new WaitForSeconds(0.3f);

        int counter = 3;

        while (true)
        {
            if (counter != 0)
            {
                countDownText.text = counter.ToString();
            }
            else
            {
                countDownText.text = "RUN!";
                Manager.instance.OnRaceStart();
                break;
            }

            counter--;
            yield return new WaitForSeconds(1.0f);
        }

        yield return new WaitForSeconds(0.5f);

        gameObject.SetActive(false);
    }
}