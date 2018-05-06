using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Security.Policy;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class SpawnManager : MonoBehaviour
{
	public MazeGenerator MazeGen;
	public EnemyManager Enemy_Manager;
	public GameObject Player;
	public GameObject Enemy;
	public GameObject Collectible;
	public Stack<Vector3> item_spawns = new Stack<Vector3>();
	public Stack<Vector3> enemy_spawns = new Stack<Vector3>();

	private int minRadius = 7;
	private List<GameObject> items = new List<GameObject>();


	public void SpawnPlayer()
	{
		Player.transform.position = MazeGenerator.startingCell;
		Vector3 nextCell = ClosestOpenVectorTo(Player.transform.position);
		Player.transform.LookAt(nextCell);
	}

	public Vector3 ClosestOpenVectorTo(Vector3 position)
	{
		int [,] grid = MazeGen.GetMazeGrid();
		
		return FindNextOpenSpace((int)position.x, (int)position.z, grid);
	}

	private Vector3 FindNextOpenSpace(int x, int z, int[,] grid)
	{
		// Check Up
		if (grid[x, z + 1] == 0)
		{
			return new Vector3(x, 0f, z + 1);
		} 
		// Check Right
		else if (grid[x + 1, z] == 0)
		{
			return new Vector3(x + 1, 0f, z);
		}
		// Check Below
		else if (grid[x, z - 1] == 0)
		{
			return new Vector3(x, 0f, z - 1);
		}
		// Must Be Left
		else
		{
			return new Vector3(x - 1, 0f, z);
		}
	}
	
	private void SpawnObjectAtRandom(GameObject obj)
	{
		// Using dead ends as spawn points, random open space available if not enough deadends
		try
		{
			Vector3 randomPosition = item_spawns.Pop();
			enemy_spawns.Push(randomPosition);
			Instantiate(obj, randomPosition, Quaternion.identity);
		}
		catch (InvalidOperationException e)
		{
			Instantiate(obj, GetRandomPosition(), Quaternion.identity);
		}
	}
	

	private Vector3 GetRandomPosition()
	{
		int[,] grid = MazeGen.GetMazeGrid();
		List<Vector3> spaces = MazeGen.GetAvailableSpaces();
		bool foundCell = false;
		Vector3 randomCell = new Vector3();
		while (!foundCell)
		{
			int index = (int) Random.Range(0, spaces.Count);
			Vector3 cell = spaces[index];

			if (cell.x >= minRadius && cell.x <= grid.GetLength(0) && cell.z >= minRadius && cell.z <= grid.GetLength(1))
			{
				foundCell = true;
				randomCell = cell;
			}
		}

		return randomCell;
	}

	public void SpawnCollectible()
	{
		SpawnObjectAtRandom(Collectible);
	}

	public void SpawnEnemy(){
		GameObject enemy_ = Instantiate (Enemy, enemy_spawns.Pop (), Quaternion.identity);
		Enemy_Manager.AddEnemy (enemy_);
		enemy_.transform.LookAt (ClosestOpenVectorTo(enemy_.transform.position));
	}

	public void clearSpawns()
	{
		enemy_spawns.Clear ();
		item_spawns.Clear ();
	}

	public void clearItems()
	{
		foreach (GameObject item in items)
		{
			Destroy(item);
			items.Remove(item);
		}
	}
}
