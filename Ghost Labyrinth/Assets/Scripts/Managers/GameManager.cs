using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public GameObject mazeHolder;
	private int level = 1;
	// Use this for initialization
	void Awake () {
		StartLevel (level);
	}

	private void StartLevel(int level){
		GameObject maze = Instantiate (mazeHolder, new Vector3 (0, 0, 0), Quaternion.identity);
		MazeGenerator mazeGen = maze.GetComponent<MazeGenerator> ();
		mazeGen.createMaze (level);
		mazeGen.spawnEntities ();
	}
}
