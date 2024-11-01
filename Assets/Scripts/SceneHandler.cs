using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler : MonoBehaviour
{
    public string typeVal;
    public int sizeVal;

    PA_Maze primsVal;
    RB_Maze recVal;

    CSVWriter writer;

    // Start is called before the first frame update
    void Start()
    {
        
        writer = GetComponent<CSVWriter>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MazeType(string buttonValue)
    {
        typeVal = buttonValue;
    }

    public void MazeSize(string buttonValue)
    {
        sizeVal = int.Parse(buttonValue);
    }

    public void GoToNextScene()
    {
        // ValueHandler.mazeType = typeVal;
        // ValueHandler.mazeSize = sizeVal;
        Manager.instance.mazeSize = sizeVal;
        Manager.instance.gridSize = (sizeVal*5)+1;

        if (typeVal == "Recursive")
        {
            SceneManager.LoadSceneAsync(1);
            Manager.instance.LevelStart();
            /*if (sizeVal == "20")
            {
                SceneManager.LoadSceneAsync(1);
                //writer.myPlayerList.mazeType = "Recursive";
                //writer.myPlayerList.mazeSize = "20x20";
            }
            else if (sizeVal == "35")
            {
                int val = 35;
                
                SceneManager.LoadSceneAsync(1);
            }
            else if (sizeVal == "50")
            {
                SceneManager.LoadSceneAsync(1);
            }*/
        }
        else if (typeVal == "Prims")
        {
            SceneManager.LoadSceneAsync(2);
            Manager.instance.LevelStart();
            /*if (sizeVal == "20")
            {
                SceneManager.LoadSceneAsync(2);
            }
            else if (sizeVal == "35")
            {
                SceneManager.LoadSceneAsync(2);
            }
            else if (sizeVal == "50")
            {
                SceneManager.LoadSceneAsync(2);
            }*/
        }
    }
}
