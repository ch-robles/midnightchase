using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapHandler : MonoBehaviour
{
    public GameObject mapPanel;
    public bool map = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (map == false){
            if (Input.GetKeyDown(KeyCode.M))
            {
                mapPanel.SetActive(true);
                map = true;
            }
        }
        else if (map == true){
            if (Input.GetKeyDown(KeyCode.M))
            {
                mapPanel.SetActive(false);
                map = false;
            }
        }
    }
}
