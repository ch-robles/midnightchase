using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Manager manager;

    // Start is called before the first frame update
    void Start()
    {
        manager = FindObjectOfType<Manager>();
    }


    // Update is called once per frame
    void Update()
    {
        if (manager.GetGameState() == GameStates.countDown)
        {
            return;
        }

        float speed = 5f;
        float x = Input.GetAxis("Horizontal");
        float y = 0;
        float z = Input.GetAxis("Vertical");
        gameObject.transform.Translate(x * speed * Time.deltaTime, y, z * speed * Time.deltaTime);
    }
}
