using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshGenerator : MonoBehaviour {

	public NavMeshSurface floor;

	// Use this for initialization
	public void BuildNav(){
		if (floor != null) {
			floor.BuildNavMesh ();
			Debug.Log ("Build was called.");
		}else
			Debug.Log ("Error: Floor Object was not found.");
	}
}
