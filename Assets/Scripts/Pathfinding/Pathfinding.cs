using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
//using System.Diagnostics;
using UnityEngine;

// for measuring performance :3


public class Pathfinding : MonoBehaviour
{
    Grid grid;
	PathRequestManager requestManager;
    [SerializeField] Transform seeker, target;
	// [SerializeField] EnemyMover enemyMover;
	int maxCost = 200;
	// temp maxcost

	void Awake(){
		requestManager = GetComponent<PathRequestManager>();
	}

    /*void Update(){
        // could be optimized to only do the grid grabbing once maybe?
		// future isha: cant bc it needs to consistently grab the player positions :3
        grid = GetComponent<Grid>();
		// requestManager = GetComponent<PathRequestManager>();

        if(grid != null && grid.gridSizeX != 0 && grid.gridSizeY != 0 /*&& requestManager != null){
            // Debug.Log("Grid got!");
            //FindPath(seeker.position ,target.position); 
			// A* Algo
			FindPathModified(seeker.position, target.position);
			// Modified A* Algo
        }
    }*/

	public void StartFindPath(Vector3 startPos, Vector3 targetPos){
		//requestManager = GetComponent<PathRequestManager>();
		grid = GetComponent<Grid>();
		// Debug.Log("Grid got!");
		if(grid != null && grid.gridSizeX != 0 && grid.gridSizeY != 0 && requestManager != null){
			// Debug.Log("Finding Path");
			// only runs once???
			StartCoroutine(FindPathModified(startPos, targetPos));
        } else {
			Debug.Log("No grid silly!");
		}
	}

    IEnumerator /*void*/ FindPath(Vector3 startPos, Vector3 targetPos){
		Vector3[] waypoints = new Vector3[0];
		bool pathSuccess = false;

        Node startNode = grid.NodeFromWorldPoint(startPos);
		Node targetNode = grid.NodeFromWorldPoint(targetPos);
		/*Debug.Log("Seeker Pos In World: " + "X: " + startNode.gridX + " Y: " + startNode.gridY);
		Debug.Log("Target Pos In World: " + targetPos);*/

		Heap<Node> openSet = new Heap<Node>(grid.MaxSize);
		HashSet<Node> closedSet = new HashSet<Node>();
		openSet.Add(startNode);

		while (openSet.Count > 0) {
			Node node = openSet.RemoveFirst();

			closedSet.Add(node);

			if (node == targetNode) {
				// RetracePath(startNode,targetNode);
				pathSuccess = true;
				break;
			}

			foreach (Node neighbour in grid.GetNeighbours(node)) {
				if (!neighbour.walkable || closedSet.Contains(neighbour)) {
					continue;
				}

				int newCostToNeighbour = node.gCost + GetDistance(node, neighbour);
				if (newCostToNeighbour < neighbour.gCost || !openSet.Contains(neighbour)) {
					neighbour.gCost = newCostToNeighbour;
					neighbour.hCost = GetDistance(neighbour, targetNode);
					neighbour.parent = node;

					if (!openSet.Contains(neighbour))
						openSet.Add(neighbour);
					else
						openSet.UpdateItem(neighbour);
				}
			}
		}

		yield return null;
		if (pathSuccess) {
			// Debug.Log("Path Found For startNode: [" + startNode.gridX + ", " + startNode.gridY + "] and targetNode: [" + targetNode.gridX + ", " + targetNode.gridY + "]");
			waypoints = RetracePath(startNode,targetNode);
		}
		requestManager.FinishedProcessingPath(waypoints,pathSuccess);
    }

	IEnumerator/*void*/ FindPathModified(Vector3 startPos, Vector3 targetPos){
		Vector3[] waypoints = new Vector3[0];
		bool pathSuccess = false;

		//Debug.Log("Start Pos: " + startPos);
		//Debug.Log("Target Pos: " + targetPos);
		//Debug.Log("Grid: " + grid == null);
		//Debug.Log("GridSizeX: " + grid.gridWorldSize.x);
		//Debug.Log("GridSizeY: " + grid.gridWorldSize.y);

        Node startNode = grid.NodeFromWorldPoint(startPos);
		Node targetNode = grid.NodeFromWorldPoint(targetPos);

		if (startNode.walkable && targetNode.walkable){
			Heap<Node> openSet = new Heap<Node>(grid.MaxSize);
			HashSet<Node> closedSet = new HashSet<Node>();
			openSet.Add(startNode);

			while (openSet.Count > 0) {
				Node node = openSet.RemoveFirst();
				closedSet.Add(node);

				if (node == targetNode) {
					// RetracePathModified(startNode,targetNode);
					pathSuccess = true;
					break;
				}

				// modifications to algo made here
				List<Node> neighbourList = grid.GetNeighbours(node);
				foreach (Node neighbour in neighbourList) {
					if (!neighbour.walkable || closedSet.Contains(neighbour)) {
						continue;
					}

					int newCostToNeighbour = node.gCost + GetDistance(node, neighbour);
					int n = node.gCost + GetDistance(node, neighbour);
					if (node.gCost < maxCost){
						if (newCostToNeighbour < neighbour.gCost || !openSet.Contains(neighbour)) {
							neighbour.gCost = newCostToNeighbour;
							neighbour.hCost = GetDistance(neighbour, targetNode);
							neighbour.parent = node;

							if (!openSet.Contains(neighbour))
								openSet.Add(neighbour);
							else
								openSet.UpdateItem(neighbour);
						}
					} 
					else {
						Node closestNeighbour = neighbourList.Aggregate((x, y) => Math.Abs(x.gCost - maxCost) < Math.Abs(y.gCost - maxCost) ? x : y);
						newCostToNeighbour = node.gCost + GetDistance(node, closestNeighbour);

						if (newCostToNeighbour < neighbour.gCost || !openSet.Contains(neighbour)){
							neighbour.gCost = newCostToNeighbour;
							neighbour.hCost = GetDistance(closestNeighbour, targetNode);
							neighbour.parent = node;

							if (!openSet.Contains(neighbour))
								openSet.Add(neighbour);
							else 
								openSet.UpdateItem(neighbour);
						}
					}
				}
			}
		}
		
		// after waiting for one frame
		yield return null;
		if (pathSuccess) {
			waypoints = RetracePathModified(startNode,targetNode);
		}
		requestManager.FinishedProcessingPath(waypoints,pathSuccess);
    }

    Vector3[] /*void*/ RetracePath(Node startNode, Node endNode) {
		List<Node> path = new List<Node>();
		Node currentNode = endNode;

		while (currentNode != startNode) {
			path.Add(currentNode);
			currentNode = currentNode.parent;
		}
		//path.Reverse();
		
		Debug.Log("A* Path Length: " + path.Count + " blocks.");
		// put csv print here!

		//grid.path = path;
		Vector3[] waypoints = SimplifyPath(path);
		Array.Reverse(waypoints);
		return waypoints;
	}

	Vector3[] /*void*/ RetracePathModified(Node startNode, Node endNode) {
		List<Node> pathModified = new List<Node>();
		Node currentNode = endNode;

		while (currentNode != startNode) {
			pathModified.Add(currentNode);
			currentNode = currentNode.parent;
		}
		//pathModified.Reverse();
		
		Debug.Log("Modified A* Path Length: " + pathModified.Count + " blocks.");
		// put csv print here!

		//grid.pathModified = pathModified;

		Vector3[] waypoints = SimplifyPath(pathModified);
		Array.Reverse(waypoints);
		return waypoints;
	}

	Vector3[] SimplifyPath(List<Node> path) {
		List<Vector3> waypoints = new List<Vector3>();
		Vector2 directionOld = Vector2.zero;
		
		for (int i = 1; i < path.Count; i ++) {
			Vector2 directionNew = new Vector2(path[i-1].gridX - path[i].gridX,path[i-1].gridY - path[i].gridY);
			if (directionNew != directionOld) {
				waypoints.Add(path[i].worldPosition);
			}
			directionOld = directionNew;
		}
		return waypoints.ToArray();
	}

	int GetDistance(Node nodeA, Node nodeB) {
		int dstX = Mathf.Abs(nodeA.gridX - nodeB.gridX);
		int dstY = Mathf.Abs(nodeA.gridY - nodeB.gridY);

		if (dstX > dstY)
			return 14*dstY + 10* (dstX-dstY);
		return 14*dstX + 10 * (dstY-dstX);
	}
}
