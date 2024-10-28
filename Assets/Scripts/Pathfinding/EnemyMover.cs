using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class EnemyMover : MonoBehaviour
{
    public Transform target;
	float speed = 5;
	Vector3[] path;
	int targetIndex;

	public GameObject deathUI;
	public GameObject inGameUI;

	EnemyManager enemy;

	// Animator Variables:
	public Animator anim;
	bool walkReady; // to trigger pathfinding "walk" animation wao
	// Model Rotation Variables:
	public float rotationSpeed;

	void Start()
    {
		enemy = GetComponent<EnemyManager>();
		anim = gameObject.GetComponent<Animator>();
		walkReady = false;
    }

	// EVERYTHING STARTS HERE (AFTER AFTER MAZE CREATION)
	void Update() {
		PathRequestManager.RequestPath(transform.position,target.position, OnPathFound);
        Debug.Log("EnemyMover Running");
		anim.SetBool("isPathfinding", walkReady);
	}

	public void OnPathFound(Vector3[] newPath, bool pathSuccessful) {
		if (pathSuccessful) {
			path = newPath;
			targetIndex = 0;
			walkReady = false;
			StopCoroutine("FollowPath");
			Debug.Log("Stopped Following Path");
			StartCoroutine("FollowPath");
		}
	}

	IEnumerator FollowPath() {
		walkReady = true;
		Debug.Log("Following Path");

		
		if (path == null || path.Length == 0)
		{
			deathUI.SetActive(true);
			inGameUI.SetActive(false);
			Manager.instance.Death();
			yield break; // Stop the coroutine if the path is invalid
		}

		Vector3 currentWaypoint = path[0];

		while (true)
		{
			if (transform.position == currentWaypoint)
			{
				targetIndex++;
				if (targetIndex >= path.Length)
				{
					yield break;
				}
				currentWaypoint = path[targetIndex];
			}

			transform.position = Vector3.MoveTowards(transform.position, currentWaypoint, speed * Time.deltaTime);
			// Debug.Log("Waypoint! :" + currentWaypoint);
			// waypoint staying the same WHY
			yield return null;

		}

		// probably like put here something for index out of range,,,
	}

	public void OnDrawGizmos() {
		if (path != null) {
			for (int i = targetIndex; i < path.Length; i ++) {
				Gizmos.color = Color.black;
				Gizmos.DrawCube(path[i], Vector3.one);

				if (i == targetIndex) {
					Gizmos.DrawLine(transform.position, path[i]);
				}
				else {
					Gizmos.DrawLine(path[i-1],path[i]);
				}
			}
		}
	}
}
