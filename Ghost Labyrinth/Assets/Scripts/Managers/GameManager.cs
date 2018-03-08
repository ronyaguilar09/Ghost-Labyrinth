using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine.AI;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public MazeGenerator Maze;
	public SpawnManager Spawn_Manager;
	public UIManager UI_Manager;
	
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

		Time.timeScale = 1f;
	}

	public void CompleteLevel()
	{
		level++;
		Time.timeScale = 0f;
		UI_Manager.DisplayComplete();
		Maze.clearMaze();
		UI_Manager.PrepareHUD(level);
	}

	public void AdvanceLevel()
	{
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
