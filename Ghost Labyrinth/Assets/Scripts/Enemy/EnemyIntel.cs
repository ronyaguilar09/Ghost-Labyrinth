using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIntel : MonoBehaviour {
	public MazeGenerator maze;
	private int[,] grid;

	struct Node{
		public int f;
		public int g;
		public int h;
		public Vector3 position;

		Node(Vector3 pos, Vector3 target){
			position = pos;
			g = 0;
			f = 0;
			h = 0;
			h = ManhattanDistance(position, target);
		}
		Node(Vector3 pos, Vector3 target, Node p){
			position = pos;
			g = 1 + p.g;
			f = 0;
			h = 0;
			h = ManhattanDistance(position, target);
		}

		// Heuristic to use for h
		private int ManhattanDistance(Vector3 start, Vector3 end){
			int xDist = (int)Mathf.Abs (end.x - start.x);
			int zDist = (int)Mathf.Abs (end.z - start.z);
			return xDist + zDist;
		}

	}
		

	// Use this for initialization
	void Start () {
		grid = maze.GetMazeGrid ();
	}

	void OnDrawGizmos(){
		for(int x = 0; x < grid.GetLength(0); x++){
			for(int z = 0; z < grid.GetLength(1); z++){
				if (x == 0 || z == 0 || x == grid.GetLength (0) - 1 || z == grid.GetLength (1) - 1 || grid [x, z] == 1){
					Gizmos.color = Color.black;
					Gizmos.DrawCube (new Vector3 (x, 0f, z), new Vector3(1,1,1));
				} else {
					Gizmos.color = Color.white;
					Gizmos.DrawCube (new Vector3 (x, 0f, z), new Vector3(1,1,1));
				}
			}
		}
	}
}
