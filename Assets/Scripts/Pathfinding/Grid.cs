using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Grid : MonoBehaviour {

	[SerializeField] GameObject floorPrefab, startPrefab, endPrefab;
	public Vector3 floorPos, startPos, endPos;
	private GameObject floor, start, end;
	public bool onlyDisplayPathGizmos;
	public LayerMask unwalkableMask;
	public Vector2 gridWorldSize;
	public Transform player;
	public float nodeRadius;
	public List<Node> path;
	public List <Node> pathModified;
	public Pathfinding pathfinder;

	Node[,] grid;

	float nodeDiameter;
	public int gridSizeX, gridSizeY;

	public void GridStart()/*Awake()*/ {
		Debug.Log("grid started");
		nodeDiameter = nodeRadius*2;

		gridWorldSize.x = Manager.instance.gridSize;
		gridWorldSize.y = Manager.instance.gridSize;

		/*Debug.Log("Current manager gridSize: " + Manager.instance.gridSize);
		Debug.Log("Grid World Size X " + gridWorldSize.x);
		Debug.Log("Grid World Size Y " + gridWorldSize.y);*/

		gridSizeX = Mathf.RoundToInt(gridWorldSize.x/nodeDiameter);
		gridSizeY = Mathf.RoundToInt(gridWorldSize.y/nodeDiameter);

		/*Debug.Log("Node Diameter " + nodeDiameter);
		Debug.Log("gridSizeX " + gridSizeX);
		Debug.Log("gridSizeY " + gridSizeY);*/

		pathfinder = GetComponent<Pathfinding>();
		// Debug.Log("Is pathfinding empty? " + pathfinder == null);
		CreateFloor();
		PlaceStart();
		PlaceEnd();
		CreateGrid();
		// pathfinder.gridGot = true;
	}

	public int MaxSize{
		get{
			return gridSizeX * gridSizeY;
		}
	}

	void CreateFloor(){
		floorPos = new Vector3(gridSizeX/2, -1.25f, gridSizeY/2);
		floor = Instantiate(floorPrefab,floorPos,Quaternion.identity) as GameObject;
		floor.transform.localScale = new Vector3(gridSizeX + 5, 0.5f, gridSizeY + 5);
		// Debug.Log("Floor created!");
	}

	void PlaceStart(){
		startPos = new Vector3(3, 0, gridSizeY - 3);
		start = Instantiate(startPrefab,startPos,Quaternion.identity) as GameObject;
		// floor.transform.localScale = new Vector3(gridSizeX, 0.5f, gridSizeY);
		// Debug.Log("Start block created!");
	}

	void PlaceEnd(){
		endPos = new Vector3(gridSizeX - 3, 0, 3);
		end = Instantiate(endPrefab,endPos,Quaternion.identity) as GameObject;
		// floor.transform.localScale = new Vector3(gridSizeX, 0.5f, gridSizeY);
		// Debug.Log("End block created!");
	}

	void CreateGrid() {
		grid = new Node[gridSizeX,gridSizeY];
		// Vector3 worldBottomLeft = transform.position - Vector3.right * gridWorldSize.x/2 - Vector3.forward * gridWorldSize.y/2;
		Vector3 worldBottomLeft = new Vector3(0,0,0);

		for (int x = 0; x < gridSizeX; x ++) {
			for (int y = 0; y < gridSizeY; y ++) {
				Vector3 worldPoint = worldBottomLeft + Vector3.right * (x * nodeDiameter + nodeRadius) + Vector3.forward * (y * nodeDiameter + nodeRadius);
				bool walkable = !(Physics.CheckSphere(worldPoint,nodeRadius,unwalkableMask));
				grid[x,y] = new Node(walkable,worldPoint, x,y);
			}
		}

		// Debug.Log("Grid created.");

		Manager.instance.SetMazeState(GameStates.gridFinished);
		// pathfinder.SetGrid();
	}

	public List<Node> GetNeighbours(Node node) {
		List<Node> neighbours = new List<Node>();

		for (int x = -1; x <= 1; x++) {
			for (int y = -1; y <= 1; y++) {
				if (x == 0 && y == 0)
					continue;

				int checkX = node.gridX + x;
				int checkY = node.gridY + y;

				if (checkX >= 0 && checkX < gridSizeX && checkY >= 0 && checkY < gridSizeY) {
					neighbours.Add(grid[checkX,checkY]);
				}
			}
		}

		return neighbours;
	}

	public Node NodeFromWorldPoint(Vector3 worldPosition) {
		float percentX = (worldPosition.x/*+ gridWorldSize.x/2*/) / gridWorldSize.x;
		float percentY = (worldPosition.z/*+ gridWorldSize.y/2*/) / gridWorldSize.y;
		percentX = Mathf.Clamp01(percentX);
		percentY = Mathf.Clamp01(percentY);
		//Debug.Log("PercentY: " + percentY);
		//Debug.Log("PercentX: " + percentX);

		// accessing ts while the grid size is still 0,, wtf
		// Debug.Log("Is Grid Null. " + grid == null);
		//Debug.Log("GridSize " + gridSizeX + " " + gridSizeY);
		int x = Mathf.RoundToInt((gridSizeX-1) * percentX);
		int y = Mathf.RoundToInt((gridSizeY-1) * percentY);
		// Debug.Log("returning x: " + x + " y: " + y);
		//Debug.Log("Node Walkable?: " + grid[x,y].walkable);

		return grid[x,y];
		// not returning anything (??)
	}

	/*void OnDrawGizmos() {
		Gizmos.DrawWireCube(transform.position,new Vector3(gridWorldSize.x,1,gridWorldSize.y));

		if (onlyDisplayPathGizmos) {
			if (path != null) {
				foreach (Node n in path) {
					Gizmos.color = Color.black;
					Gizmos.DrawCube(n.worldPosition, Vector3.one * (nodeDiameter-.1f));
				}
			}
			if (pathModified != null) {
				foreach (Node n in pathModified) {
					Gizmos.color = Color.magenta;
					Gizmos.DrawCube(n.worldPosition, Vector3.one * (nodeDiameter-.1f));
				}
			}
		}
		else {

			if (grid != null) {
				foreach (Node n in grid) {
					Gizmos.color = (n.walkable)?Color.white:Color.red;
					if (pathModified != null)
						if (path.Contains(n))
							Gizmos.color = Color.black;
						if (pathModified.Contains(n))
							Gizmos.color = Color.magenta;
					Gizmos.DrawCube(n.worldPosition, Vector3.one * (nodeDiameter-.1f));
				}
			}
		}
	}*/
}