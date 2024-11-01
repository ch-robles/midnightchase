using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ValueHandler : MonoBehaviour
{
    public static string mazeType;
    public static string mazeSize;

    PA_Maze primsVal;
    RB_Maze recVal;

    // Start is called before the first frame update
    void Start()
    {
        primsVal = GetComponent<PA_Maze>();
        recVal = GetComponent<RB_Maze>();

        Values();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Values()
    {
        if (mazeType == "Recursive")
        {
            if (mazeSize == "20")
            {
                int val = 20;
                recVal.row = val;
                recVal.column = val;
            }
            else if (mazeSize == "35")
            {
                int val = 35;
                recVal.row = val;
                recVal.column = val;

            }
            else if (mazeSize == "50")
            {
                int val = 50;
                recVal.row = val;
                recVal.column = val;
                
            }
        }
        else if (mazeType == "Prims")
        {
            if (mazeSize == "20")
            {
                int val = 20;
                primsVal.row = val;
                primsVal.column = val;
                
            }
            else if (mazeSize == "35")
            {
                int val = 35;
                primsVal.row = val;
                primsVal.column = val;
                
            }
            else if (mazeSize == "50")
            {
                int val = 50;
                primsVal.row = val;
                primsVal.column = val;
            }
        }
    }

    
}
