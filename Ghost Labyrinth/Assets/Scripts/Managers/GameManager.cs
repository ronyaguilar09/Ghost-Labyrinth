using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine.AI;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public MazeGenerator Maze;
	public SpawnManager Spawn_Manager;
	
	private int level = 1;

	void Awake ()
	{
		StartLevel (level);
	}

	private void StartLevel(int level){
		Maze.createMaze (level);
		Spawn_Manager.SpawnPlayer();
		for (int i = 0; i < HUDManager.totalCollectibles; i++)
		{
			Spawn_Manager.SpawnCollectible();
		}

		StartCoroutine(CheckProgress());
	}

	private void AdvanceLevel()
	{
		level++;
		Spawn_Manager.clearLevel();
		Maze.clearMaze();
		HUDManager.PrepareLevel(level);
		StartLevel(level);
	}
	
	// Make manager public to hud for level progression

}
