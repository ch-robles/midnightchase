using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[System.Serializable]
    public class primNode {
        public GameObject gizmoNode, southWall, eastWall;
        public int xPosition;
        public int yPosition;
        public bool visited;
        public bool frontier;

        public primNode(GameObject _gizmoNode, GameObject _southWall, GameObject _eastWall, int _xPosition, int _yPosition, bool _visited, bool _frontier){
            eastWall = _eastWall;
            southWall = _southWall;
            gizmoNode = _gizmoNode;
            xPosition = _xPosition;
            yPosition = _yPosition;
            visited = _visited;
            frontier = _frontier;
        }
    }

// A IS X, B IS Y

public class PA_Maze : MonoBehaviour
{
    [SerializeField] GameObject gizmoPlacer;
    [SerializeField] GameObject wall, startNodePrefab, endNodePrefab, groundFloorPrefab;
    [SerializeField] Grid gridMaker;
    [SerializeField] PlayerPositioner player;
    // [SerializeField] Pathfinder pathfinder;
    public int row, column;
    private GameObject gizmoNode, southwallPrefab, eastwallPrefab,groundFloor/*, self*/;
    private primNode[,] unvisitedNodes;
    private List<primNode> frontierNodes;
    private List<primNode> passage;
    private Vector3 startPosition;
    private primNode currentNode;
    private bool drawGrid = false;
    // private Vector3 startPlayerPosition, endPlayerPosition;
    private float startPlayerRotation;

    IEnumerator Start()
    {
        // needs to be here for the pathfinder to work LMAO

        row = Manager.instance.mazeSize;
        column = Manager.instance.mazeSize;
        gridMaker = GetComponent<Grid>();
        startPosition = new Vector3(3,0,3);
        Vector3 myPos = startPosition;
        unvisitedNodes = new primNode[row,column];
        frontierNodes = new List<primNode>();
        passage = new List<primNode>();
        // self = GetComponent<GameObject>;
        // unvisitedNodes = new List<List<primNode>>();
        // visitedNodes = new List<primNode>();
        // floor position (should be middle, then a little below), resize to 5*row, 5*column, some thickness for the bottom, no rotation
        /*Vector3 floorPosition = new Vector3((column/2)*5.0f, -1.3f, (row/2)*5.0f);
        groundFloor = Instantiate(groundFloorPrefab,floorPosition,Quaternion.identity) as GameObject;
        groundFloor.transform.localScale = new Vector3(row*5.0f, 0.5f, column*5.0f);*/
        // setting down nodes
        for (int a = 0; a < column; a++){
            // unvisitedNodes.Add(new List<primNode>());
            for (int b = 0; b < row; b++){
                // 5.0f = current wall length! so ung gap between the gizmos cause they need to be centered
                myPos = new Vector3(startPosition.x + (a*5.0f), 0.0f, startPosition.z + (b*5.0f));
                Vector3 southPos = new Vector3(startPosition.x + (a*5.0f), 0.0f, startPosition.z + (b*5.0f) - 2.5f);
                Vector3 eastPos = new Vector3(startPosition.x + (a*5.0f) + 2.5f, 0.0f, startPosition.z + (b*5f));
                gizmoNode = Instantiate(gizmoPlacer,myPos,Quaternion.identity) as GameObject;
                southwallPrefab = Instantiate(wall,southPos,Quaternion.Euler(0,90,0)) as GameObject;
                eastwallPrefab = Instantiate(wall,eastPos,Quaternion.identity) as GameObject;
                gizmoNode.name = "grid no. " + a + "," + b;
                unvisitedNodes[a,b] = new primNode(/*true, true, true, true,*/ gizmoNode, southwallPrefab, eastwallPrefab, a, b, false, false);

                if (a == 0){
                    Vector3 westPos = new Vector3(startPosition.x + (a*5.0f) - 2.5f, 0.0f, startPosition.z + (b*5f));
                    GameObject westEdge = Instantiate(wall,westPos,Quaternion.identity) as GameObject;

                    /*if (b == row - 1) {
                        GameObject startBlock = Instantiate(startNodePrefab,myPos,Quaternion.identity) as GameObject;
                        // startPlayerPosition = myPos;
                        // set start position here
                    }*/
                }

                if(b == row - 1){
                    Vector3 northPos = new Vector3(startPosition.x + (a*5.0f), 0.0f, startPosition.z + (b*5f) + 2.5f);
                    GameObject northEdge = Instantiate(wall,northPos,Quaternion.Euler(0,90,0)) as GameObject;
                }

                /*if (b == 0 && a == column - 1){
                    GameObject endBlock = Instantiate(endNodePrefab,myPos,Quaternion.identity) as GameObject;
                    endPlayerPosition = myPos;
                    // set the end position block here
                }*/

                //unvisitedNodes[a].Add(new primNode(true, true, true, true, gizmoNode, a, b));
            }
        }

        // if first time visiting a node
        /*if (currentNode == null){
            int randomX = Random.Range(0, row);
            int randomY = Random.Range(0, column);
            //Debug.Log("Random Node: " + randomX + " , " + randomY);
            currentNode = unvisitedNodes[randomX, randomY];
            // currentNode.visited = true;
            //Debug.Log(currentNode.gizmoNode.name);
        }
        
        for (int a = 0; a < unvisitedNodes.Length-1; a++){
            // current frontier not updating here
            //Debug.Log("Node " + currentNode.gizmoNode.name + " selected.");
            currentNode = nextNode(currentNode);
        }*/

        // gridMaker.GridStart();
        // drawGrid = true;
        // coroutine this?
        //Debug.Log("Maze finish.");

        yield return StartCoroutine(BuildMaze());
        StopAllCoroutines();

        Manager.instance.SetMazeState(GameStates.mazeFinished);
        gridMaker.GridStart();
    }

    IEnumerator BuildMaze(){
        if (currentNode == null){
            int randomX = Random.Range(0, row);
            int randomY = Random.Range(0, column);
            currentNode = unvisitedNodes[randomX, randomY];
        }
        
        for (int a = 0; a < unvisitedNodes.Length-1; a++){
            // current frontier not updating here
            //Debug.Log("Node " + currentNode.gizmoNode.name + " selected.");
            currentNode = nextNode(currentNode);

            /*if (a == unvisitedNodes.Length-2){
                Debug.Log(a);
                GameManager.instance.SetGameState(GameStates.mazeFinished);
                gridMaker.StartGrid();
            }*/
        }

        yield return null;
    }

    public primNode nextNode(primNode currentNode){
        // find neighbors
        // passage = new List<primNode>();
        int randomIndex;
        primNode nextNode = null;
        
        // check if these neighbors are unvisited (null checking) and if a corner or edge
        // add node in north
        if (currentNode.xPosition < row - 1){
            if(unvisitedNodes[currentNode.xPosition + 1, currentNode.yPosition].visited == false && unvisitedNodes[currentNode.xPosition + 1, currentNode.yPosition].frontier == false /*!= null*/){
                frontierNodes.Add(unvisitedNodes[currentNode.xPosition + 1, currentNode.yPosition]);
                unvisitedNodes[currentNode.xPosition + 1, currentNode.yPosition].frontier = true;
                //Debug.Log("Added " + unvisitedNodes[currentNode.xPosition + 1, currentNode.yPosition].gizmoNode.name + " to current frontier.");
            }
        }

        // add node in east
        if (currentNode.yPosition < column - 1){
            if(unvisitedNodes[currentNode.xPosition, currentNode.yPosition + 1].visited == false && unvisitedNodes[currentNode.xPosition, currentNode.yPosition + 1].frontier == false /*!= null*/){
                frontierNodes.Add(unvisitedNodes[currentNode.xPosition, currentNode.yPosition + 1]);
                unvisitedNodes[currentNode.xPosition, currentNode.yPosition + 1].frontier = true;
                //Debug.Log("Added " + unvisitedNodes[currentNode.xPosition, currentNode.yPosition + 1].gizmoNode.name + " to current frontier.");
            }
        }

        // add node in west
        if (currentNode.xPosition > 0){
            if(unvisitedNodes[currentNode.xPosition - 1, currentNode.yPosition].visited == false && unvisitedNodes[currentNode.xPosition - 1, currentNode.yPosition].frontier == false /*!= null*/){
                frontierNodes.Add(unvisitedNodes[currentNode.xPosition - 1, currentNode.yPosition]);
                unvisitedNodes[currentNode.xPosition - 1, currentNode.yPosition].frontier = true;
                //Debug.Log("Added " + unvisitedNodes[currentNode.xPosition - 1, currentNode.yPosition].gizmoNode.name + " to current frontier.");
            }
        }

        // add node in south
        if (currentNode.yPosition > 0){
            if(unvisitedNodes[currentNode.xPosition, currentNode.yPosition - 1].visited == false && unvisitedNodes[currentNode.xPosition, currentNode.yPosition - 1].frontier == false /*!= null*/){
                frontierNodes.Add(unvisitedNodes[currentNode.xPosition, currentNode.yPosition - 1]);
                unvisitedNodes[currentNode.xPosition, currentNode.yPosition - 1].frontier = true;
                //Debug.Log("Added " + unvisitedNodes[currentNode.xPosition, currentNode.yPosition - 1].gizmoNode.name + " to current frontier.");
            }
        }

        currentNode.visited = true; 
        //Debug.Log("Node " + currentNode.gizmoNode.name + " visited.");
        // passage.Add(currentNode);

        if (frontierNodes.Any()){
            randomIndex = Random.Range(0,frontierNodes.Count);
            nextNode = frontierNodes[randomIndex];
            //Debug.Log("Node " + nextNode.gizmoNode.name + " next.");
            frontierNodes.RemoveAt(randomIndex);
        }

        // passage for the next node

        // passage node in north
        if (nextNode.xPosition < row - 1){
            if(unvisitedNodes[nextNode.xPosition + 1, nextNode.yPosition].visited == true /*!= null*/){
                passage.Add(unvisitedNodes[nextNode.xPosition + 1, nextNode.yPosition]);
                //Debug.Log("Added " + unvisitedNodes[nextNode.xPosition + 1, nextNode.yPosition].gizmoNode.name +" to current passages.");
            }
        }

        // passage node in east
        if (nextNode.yPosition < column - 1){
            if(unvisitedNodes[nextNode.xPosition, nextNode.yPosition + 1].visited == true /*!= null*/){
                passage.Add(unvisitedNodes[nextNode.xPosition, nextNode.yPosition + 1]);
                //Debug.Log("Added " + unvisitedNodes[nextNode.xPosition, nextNode.yPosition + 1].gizmoNode.name +" to current passages.");
            }
        }

        // passage node in west
        if (nextNode.xPosition > 0){
            if(unvisitedNodes[nextNode.xPosition - 1, nextNode.yPosition].visited == true /*!= null*/){
                passage.Add(unvisitedNodes[nextNode.xPosition - 1, nextNode.yPosition]);
                //Debug.Log("Added " + unvisitedNodes[nextNode.xPosition - 1, nextNode.yPosition].gizmoNode.name +" to current passages.");
            }
        }

        // passage node in south
        if (nextNode.yPosition > 0){
            if(unvisitedNodes[nextNode.xPosition, nextNode.yPosition - 1].visited == true /*!= null*/){
                passage.Add(unvisitedNodes[nextNode.xPosition, nextNode.yPosition - 1]);
                //Debug.Log("Added " + unvisitedNodes[nextNode.xPosition, nextNode.yPosition - 1].gizmoNode.name +" to current passages.");
            }
        }

        if (!passage.Any()){
            //Debug.Log("no passages");
        }
        randomIndex = Random.Range(0,passage.Count);

        switch (nextNode.yPosition - passage[randomIndex].yPosition){
            case -1:
                // passage[randomIndex].southWall.SetActive(false);
                Destroy(passage[randomIndex].southWall);
                break;
            case 1:
                // nextNode.southWall.SetActive(false);
                Destroy(nextNode.southWall);
                if (nextNode.yPosition == row - 1 && nextNode.xPosition == 0){
                    // y rotation
                    startPlayerRotation = 180;
                }
                break;
            case 0:
                switch (nextNode.xPosition - passage[randomIndex].xPosition){
                    case -1:
                        // nextNode.eastWall.SetActive(false);
                        Destroy(nextNode.eastWall);
                        if (nextNode.yPosition == row - 1 && nextNode.xPosition == 0){
                        // y rotation
                            startPlayerRotation = 90;
                        }
                        break;
                    case 1:
                        // passage[randomIndex].eastWall.SetActive(false);
                        Destroy(passage[randomIndex].eastWall);
                        break;
                }
                break;
            default:
                break;
        }

        // i feel like i have to put rotation in here idk
        
        // Debug.Log("No wall between " + nextNode.gizmoNode.name + " and " + passage[randomIndex].gizmoNode.name);

        if (nextNode != null){
            //Debug.Log("Frontier Node count: " + frontierNodes.Count());
            passage.Clear();
            return nextNode;
        } else {
            //Debug.Log("Returned null. Frontier Node count: " + frontierNodes.Count());
            passage.Clear();
            return null;
        }
    }

    /*public Vector3 getStartPosition(){
        return startPlayerPosition;
    }*/

    public float getStartRotation(){
        return startPlayerRotation;
    }

    // IM THE GOAT
    /*void Update(){
        if (drawGrid == true){
            gridMaker.GridStart();
            player.PlacePlayer();
            gameObject.SetActive(false);
            // so it stops running after the gridmakers done
        }
    }*/
}

