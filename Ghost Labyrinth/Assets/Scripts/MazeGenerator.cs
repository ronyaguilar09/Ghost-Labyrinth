﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeGenerator : MonoBehaviour {
	public int rows = 15;
	public int cols = 15;
	public GameObject wall;
	public GameObject player;
	public GameObject floor;

	private int[,] grid;
	private Vector3 [] cellStack;
	private Vector3 startingCell;

	public void spawnEntities(){
		Instantiate (player, new Vector3(startingCell.x, 1, startingCell.z), Quaternion.identity);
	}

	public void createMaze(int level) {
		cols += level;
		rows += level;

		grid = new int[cols+2, rows+2];

		for(int y = 0; y < grid.GetLength(1); y++){
			for(int x = 0; x < grid.GetLength(0); x++){
				grid[x,y] = 1;
			}
		}

		startingCell = new Vector3(Random.Range(1, grid.GetLength(0) - 1), 0f, Random.Range(1, grid.GetLength(1) - 1));
		grid[(int)startingCell.x, (int)startingCell.z] = 0;

		recurse((int)startingCell.x, (int)startingCell.z);
		populateWalls ();
		scaleAndPositionFloor ();
	}

	public int[,] GetMazeGrid(){
		return this.grid;
	}

	private void scaleAndPositionFloor() {
		floor = Instantiate (floor, gameObject.transform.position, Quaternion.identity);
		floor.transform.SetParent (gameObject.transform);
		floor.transform.position = new Vector3 (cols / 2, -0.5f, rows / 2);
		floor.transform.localScale = new Vector3 ((float)(cols + 2) / 10, 1f, (float)(rows + 2) / 10);
	}
	private void recurse (int x, int y)
	{
		List<int> randomDirections = generateRandomDirections ();

		for (int i = 0; i < randomDirections.Count; i++) {
			switch (randomDirections [i]) {
			// Up
			case 0:
				if (y - 2 <= 0)
					continue;
				if (grid [x, y - 2] != 0 && y - 2 != 0) {
					grid [x, y - 2] = 0;
					grid [x, y - 1] = 0;
					recurse(x, y - 2);
				}
				break;
			// Down
			case 1:
				if (y + 2 >= grid.GetLength(1))
					continue;
				if (grid [x, y + 2] != 0 && y + 2 != grid.GetLength(1) - 1) {
					grid [x, y + 2] = 0;
					grid [x, y + 1] = 0;
					recurse(x, y + 2);
				}
				break;
			// Left
			case 2:
				if (x - 2 <= 0)
					continue;
				if (grid [x - 2, y] != 0 && x - 2 != 0) {
					grid [x - 2, y] = 0;
					grid [x - 1, y] = 0;
					recurse(x - 2, y);
				}
				break;
			// Right
			case 3:
				if (x + 2 >= grid.GetLength(0))
					continue;
				if (grid [x + 2, y] != 0 && x + 2 != grid.GetLength(0) - 1) {
					grid [x + 2, y] = 0;
					grid [x + 1, y] = 0;
					recurse(x + 2, y);
				}
				break; 
			}
		}

	}

	private List<int> generateRandomDirections(){
		List<int> directions = new List<int>();
		List<int> randDirections = new List<int>();

		for(int i = 0; i < 4; i++){
			directions.Add(i);
		}

		for(int i = 0; i < 4; i++){
			int index = (int)Random.Range(0,directions.Count);
			randDirections.Add(directions[index]);
			directions.RemoveAt(index);
		}
		return randDirections;
	}
		
	void populateWalls(){
		for(int z = 0; z < grid.GetLength(1); z++){
			for(int x = 0; x < grid.GetLength(0); x++){
				if (x == 0 || z == 0 || x == grid.GetLength (0) - 1 || z == grid.GetLength (1) - 1 || grid [x, z] == 1) {
					GameObject instance = Instantiate (wall, new Vector3 (x, 0f, z), Quaternion.identity);
					instance.transform.SetParent (gameObject.transform);
				}
			}
		}
	}

}
