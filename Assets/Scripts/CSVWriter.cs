using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class CSVWriter : MonoBehaviour
{
    string filename = "";


    public class Player
    {

        public int testNum;
        public string mazeType;
        public string mazeSize;
        public int maxDis;
        public string status;
        public string reason;

    }

    public class PlayerList
    {
        public Player[] player;
    }

    public PlayerList myPlayerList = new PlayerList();


    // Start is called before the first frame update
    void Start()
    {
        filename = Application.dataPath + "/test.csv";
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            WriteCSV();
        }
    }

    public void WriteCSV()
    {
        if(myPlayerList.player.Length > 0)
        {
            TextWriter tw = new StreamWriter(filename, false);
            tw.WriteLine("Test #, Maze Type, Maze Size, Max Distance, Status, Reason");
            tw.Close();

            tw = new StreamWriter(filename, true);

            for(int i = 0; i < myPlayerList.player.Length; i++)
            {
                tw.WriteLine(myPlayerList.player[i].testNum + "," +
                    myPlayerList.player[i].mazeType + "," +
                    myPlayerList.player[i].mazeSize + "," +
                    myPlayerList.player[i].maxDis + "," +
                    myPlayerList.player[i].status + "," +
                    myPlayerList.player[i].reason);
            }
            tw.Close();
        }
    }
}
