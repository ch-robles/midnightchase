using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
//using System.Diagnostics;
using UnityEngine;

public class Pathfinding : MonoBehaviour {
	
	PathRequestManager requestManager;
	Grid grid;
	int maxCost = 2;
	
	/*void Awake() {
		if (GameManager.instance.GetGameState() == GameStates.gridFinished){
			requestManager = GetComponent<PathRequestManager>();
			grid = GetComponent<Grid>();
		}
	}*/

	void Awake() {
		requestManager = GetComponent<PathRequestManager>();
	}
	
	
	public void StartFindPath(Vector3 startPos, Vector3 targetPos) {
		grid = GetComponent<Grid>();
		if (Manager.instance.GetMazeState() == GameStates.gridFinished && grid != null && grid.gridSizeX != 0 && grid.gridSizeY != 0 && requestManager != null){
			// StopAllCoroutines();
			Debug.Log("StartFindPath");
			StartCoroutine(FindPath(startPos,targetPos));
		} else {
			Debug.Log("No grid yet.");
			/*Vector3 still = new Vector3(0f,0f,0f);
			Vector3[] stillStand = {still};
			requestManager.FinishedProcessingPath(stillStand, true);
			Debug.Log("Still");*/
		}
	}
	
	/*IEnumerator FindPath(Vector3 startPos, Vector3 targetPos) {

		Vector3[] waypoints = new Vector3[0];
		bool pathSuccess = false;
		
		Node startNode = grid.NodeFromWorldPoint(startPos);
		Node targetNode = grid.NodeFromWorldPoint(targetPos);

		if (startNode.walkable && targetNode.walkable) {
			Heap<Node> openSet = new Heap<Node>(grid.MaxSize);
			HashSet<Node> closedSet = new HashSet<Node>();
			openSet.Add(startNode);
			
			while (openSet.Count > 0) {
				Node currentNode = openSet.RemoveFirst();
				closedSet.Add(currentNode);
				
				if (currentNode == targetNode) {
					pathSuccess = true;
					break;
					Debug.Log("Current node is target node.");
				}
				
				foreach (Node neighbour in grid.GetNeighbours(currentNode)) {
					if (!neighbour.walkable || closedSet.Contains(neighbour)) {
						continue;
					}
					
					int newMovementCostToNeighbour = currentNode.gCost + GetDistance(currentNode, neighbour);
					if (newMovementCostToNeighbour < neighbour.gCost || !openSet.Contains(neighbour)) {
						neighbour.gCost = newMovementCostToNeighbour;
						neighbour.hCost = GetDistance(neighbour, targetNode);
						neighbour.parent = currentNode;
						
						if (!openSet.Contains(neighbour))
							openSet.Add(neighbour);
						else 
							openSet.UpdateItem(neighbour);
					}
				}
			}
		}

		yield return null;

		if (pathSuccess) {
			waypoints = RetracePath(startNode,targetNode);
		} else {
			Debug.Log("Path unsuccesful!");
		}

		requestManager.FinishedProcessingPath(waypoints,pathSuccess);
		
	}*/

	// modified a* algorithm
	IEnumerator/*void*/ FindPath(Vector3 startPos, Vector3 targetPos){
		Vector3[] waypoints = new Vector3[0];
		bool pathSuccess = false;

        Node startNode = grid.NodeFromWorldPoint(startPos);
		Node targetNode = grid.NodeFromWorldPoint(targetPos);

		Debug.Log("START " + startNode.walkable);
		Debug.Log("STARTX " + startNode.gridX);
		Debug.Log("STARTY " + startNode.gridY);
		Debug.Log("TARGET " + targetNode.walkable);
		Debug.Log("TARGETX " + targetNode.gridX);
		Debug.Log("TARGETY " + targetNode.gridX);


		if (startNode.walkable && targetNode.walkable){
			Heap<Node> openSet = new Heap<Node>(grid.MaxSize);
			HashSet<Node> closedSet = new HashSet<Node>();
			openSet.Add(startNode);

			while (openSet.Count > 0) {
				Node node = openSet.RemoveFirst();
				closedSet.Add(node);

				if (node == targetNode) {
					// RetracePathModified(startNode,targetNode);
					Debug.Log("Node is target node.");
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
			waypoints = RetracePath(startNode,targetNode);
		} else {
			Debug.Log("Path unsuccessful!");
		}

		requestManager.FinishedProcessingPath(waypoints,pathSuccess);
    }
	
	Vector3[] RetracePath(Node startNode, Node endNode) {
		List<Node> path = new List<Node>();
		Node currentNode = endNode;
		
		while (currentNode != startNode) {
			path.Add(currentNode);
			currentNode = currentNode.parent;
		}
		Vector3[] waypoints = SimplifyPath(path);
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
