using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class CSVWriter : MonoBehaviour
{
    string filename = "";
    public static CSVWriter instance;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

    }

    public class Player
    {

        public int testNum;
        public string mazeType;
        public int mazeSize;
        public int maxDis;
        public string status;
        public string reason;

    }

    public List<Player> playerList = new List<Player>();


    // Start is called before the first frame update
    void Start()
    {
        int i = 1;
        do
        {
            filename = Application.dataPath + "/csvFiles/test" + i + ".csv";
            i++;
        } while (File.Exists(filename));
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
        if(playerList.Count > 0)
        {
            using (TextWriter tw = new StreamWriter(filename, false))
            {
                tw.WriteLine("Test #, Maze Type, Maze Size, Max Distance, Status, Reason");
            }

            using (TextWriter tw = new StreamWriter(filename, true))
            {
                foreach (var player in playerList)
                {
                    tw.WriteLine(player.testNum + "," +
                                 player.mazeType + "," +
                                 player.mazeSize + "," +
                                 player.maxDis + "," +
                                 player.status + "," +
                                 player.reason);
                }
            }
        }
    }
}
