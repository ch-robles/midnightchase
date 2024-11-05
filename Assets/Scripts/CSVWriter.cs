using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class CSVWriter : MonoBehaviour
{
    string filename = "";
    public static CSVWriter instance;


    public bool quitGame;
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
        public int initDis;
        public int maxDis;
        public int minDis;
        public string status;
        public string reason;

    }

    public List<Player> playerList = new List<Player>();


    // Start is called before the first frame update
    void Start()
    {
        Directory.CreateDirectory(Application.streamingAssetsPath + "/CSVFiles/");

        int i = 1;
        do
        {
            filename = Application.streamingAssetsPath + "/CSVFiles/test" + i + ".csv";
            i++;
        } while (File.Exists(filename));
    }

    // Update is called once per frame
    void Update()
    {
        if (quitGame == true)
        {
            WriteCSV();
            quitGame = false;
        }
    }

    public void WriteCSV()
    {
        if(playerList.Count > 0)
        {
            using (TextWriter tw = new StreamWriter(filename, false))
            {
                tw.WriteLine("Test #, Maze Type, Maze Size, Initial Distance, Max Distance, Min Distance, Status, Reason");
            }

            using (TextWriter tw = new StreamWriter(filename, true))
            {
                foreach (var player in playerList)
                {
                    tw.WriteLine(player.testNum + ", " +
                                 player.mazeType + ", " +
                                 player.mazeSize + ", " +
                                 player.initDis + ", " +
                                 player.maxDis + ", " +
                                 player.minDis + ", " +
                                 player.status + ", " +
                                 player.reason);
                }
            }
        }
    }
}
