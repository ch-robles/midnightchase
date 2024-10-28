﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class PathRequestManager : MonoBehaviour {

	Queue<PathRequest> pathRequestQueue = new Queue<PathRequest>();
	PathRequest currentPathRequest;

	static PathRequestManager instance;
	Pathfinding pathfinding;

	bool isProcessingPath;




	void Awake() {
		instance = this;
		pathfinding = GetComponent<Pathfinding>();
		
	}

	void Start()
    {
		
	}

    //only requesting path but not processing it yet! will be processed later
	public static void RequestPath(Vector3 pathStart, Vector3 pathEnd, Action<Vector3[], bool> callback) {
		

		PathRequest newRequest = new PathRequest(pathStart,pathEnd,callback);
        //Debug.Log("Requested path.");

        // queues a new request to be processed later
		instance.pathRequestQueue.Enqueue(newRequest);

        // try processing this request if theres nothing processing atm
		instance.TryProcessNext();
	}

    // calls to start pathfinding
	void TryProcessNext() {
        // if not processing a path, and if pathreqqueue is not empty
		if (!isProcessingPath && pathRequestQueue.Count > 0) {
            // get the oldest path requeust
			currentPathRequest = pathRequestQueue.Dequeue();
            // set processing path trye
			isProcessingPath = true;

            // find the path
			pathfinding.StartFindPath(currentPathRequest.pathStart, currentPathRequest.pathEnd);
			Debug.Log("Process nexting");
		}
	}

    // called by pathfinder when finished finding a path, to continue finding other paths
	public void FinishedProcessingPath(Vector3[] path, bool success) {
		currentPathRequest.callback(path,success);
		isProcessingPath = false;
		TryProcessNext();
	}

    // data struct for all the path requests to be processed
	struct PathRequest {
		public Vector3 pathStart;
		public Vector3 pathEnd;
		public Action<Vector3[], bool> callback;

		public PathRequest(Vector3 _start, Vector3 _end, Action<Vector3[], bool> _callback) {
			pathStart = _start;
			pathEnd = _end;
			callback = _callback;
		}

	}
}