using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Security.Policy;
using UnityEngine;
using UnityEngine.UI;

public class SpawnManager : MonoBehaviour
{
	public MazeGenerator MazeGen;
	public GameObject Player;
	public GameObject Collectible;

	private int minRadius = 7;


	public void SpawnPlayer()
	{
		Vector3 nextCell = ClosestOpenVector();
		Player.transform.position = MazeGenerator.startingCell;
		Player.transform.LookAt(nextCell);
	}

	private Vector3 ClosestOpenVector()
	{
		int [,] grid = MazeGen.GetMazeGrid();
		Vector3 playerSpawn = MazeGenerator.startingCell;
		
		return FindNextOpenSpace((int)playerSpawn.x, (int)playerSpawn.z, grid);
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
		// Need to find random available spot to pass
		Vector3 randomPosition = GetRandomPosition();
		Instantiate(obj, randomPosition, Quaternion.identity);
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

	public void clearLevel()
	{
		
	}
}
