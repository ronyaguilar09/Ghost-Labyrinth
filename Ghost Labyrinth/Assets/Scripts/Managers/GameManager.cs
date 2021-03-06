﻿using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine.AI;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public MazeGenerator Maze;
	public SpawnManager Spawn_Manager;
	public UIManager UI_Manager;
	public EnemyManager Enemy_Manager;
	public NavMeshSurface surface;
	public GameObject player;
	
	public int level = 1;

	void Awake ()
	{
		StartLevel (level);
	}

	private void StartLevel(int level){
		Maze.createMaze (level);
		Spawn_Manager.SpawnPlayer();
		Enemy_Manager.setCount (level / 2);

		for (int i = 0; i < HUDManager.totalCollectibles; i++)
		{
			Spawn_Manager.SpawnCollectible();
		}

		for(int i = 0; i < Enemy_Manager.getCount(); i++){
			Spawn_Manager.SpawnEnemy ();
		}

		surface.BuildNavMesh();

		Time.timeScale = 1f;
	}

	public void CompleteLevel()
	{
		level++;
		Time.timeScale = 0f;
		UI_Manager.DisplayComplete();
		Maze.clearMaze();
		Spawn_Manager.clearSpawns ();
		Enemy_Manager.clearEnemies();
		UI_Manager.PrepareHUD(level);
	}

	public void AdvanceLevel()
	{
		StartLevel(level);
		UI_Manager.HideMenu();
	}

	public void GameOver()
	{
		Time.timeScale = 0f;
		level = 1;
		UI_Manager.DisplayGameOver();
		Maze.clearMaze();
		Spawn_Manager.clearSpawns ();
		Enemy_Manager.clearEnemies();
		UI_Manager.PrepareHUD(level);
	}

	public void RestartGame()
	{
		player.transform.Find("Avatar").GetComponent<Animator>().SetTrigger("Restart");
		player.GetComponent<PlayerMovement>().enabled = true;
		StartLevel(level);
		UI_Manager.HideMenu();
	}

	public void QuitGame()
	{
		#if UNITY_EDITOR 
		if (Application.isEditor) 
			UnityEditor.EditorApplication.isPlaying = false;
		#endif
		
		Application.Quit();
	}
	// Make manager public to hud for level progression

}
