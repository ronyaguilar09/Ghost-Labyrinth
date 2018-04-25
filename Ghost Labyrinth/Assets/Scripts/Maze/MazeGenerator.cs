using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeGenerator : MonoBehaviour {
	public int rows = 15;
	public int cols = 15;
	public GameObject wall;
	public GameObject player;
	public GameObject enemy;
	public GameObject floor;
	public SpawnManager spawns;

	private int[,] grid;
	private Vector3 [] cellStack;
	private GameObject wall_parent;
	private GameObject floorInstance;
	public static Vector3 startingCell;

	public void clearMaze()
	{
		Destroy(wall_parent);
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

	    wall_parent = new GameObject("Wall Parent");
		wall_parent.transform.SetParent(gameObject.transform);

		recurse((int)startingCell.x, (int)startingCell.z);
		populateWalls ();
		scaleAndPositionFloor ();
	}

	public List<Vector3> GetAvailableSpaces()
	{
		List<Vector3> openSpaces = new List<Vector3>();
		
		for(int z = 0; z < grid.GetLength(1); z++){
			for(int x = 0; x < grid.GetLength(0); x++){
				if (grid [x, z] == 0) {
						openSpaces.Add(new Vector3(x, 0f, z));		
				}
			}
		}

		return openSpaces;
	}

	public int[,] GetMazeGrid(){
		return this.grid;
	}

	private void scaleAndPositionFloor() {
		//floorInstance = Instantiate (floor, gameObject.transform.position, Quaternion.identity);
		//floorInstance.transform.SetParent (gameObject.transform);
		floor.transform.position = new Vector3 (cols / 2 + 1, -0.7f, rows / 2 + 1);
		floor.transform.localScale = new Vector3 ((float)cols+1, 0.2f, (float)rows+1);
		//floor.transform.localScale = new Vector3 ((float)(cols + 2) / 10, 1f, (float)(rows + 2) / 10);
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

		if(isADeadEnd(x,y))
			spawns.item_spawns.Push(new Vector3(x, 0f, y));

	}

	private bool isADeadEnd(int x, int z)
	{
		// Skip player's position
		if (x == startingCell.x && z == startingCell.z)
			return false;

		// 4 cases: ends below, to right, to left, to top
		// ends below
		if (grid[x + 1, z] == 1 && grid[x - 1, z] == 1 && grid[x, z - 1] == 1)
			return true;
		// ends to left
		if (grid[x - 1, z] == 1 && grid[x, z - 1] == 1 && grid[x, z+1] == 1)
			return true;
		// ends to top
		if (grid[x + 1, z] == 1 && grid[x - 1, z] == 1 && grid[x, z + 1] == 1)
			return true;
		// ends to right
		if (grid[x + 1, z] == 1 && grid[x, z + 1] == 1 && grid[x, z - 1] == 1)
			return true;

		return false;
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
		
	private void populateWalls(){
		for(int z = 0; z < grid.GetLength(1); z++){
			for(int x = 0; x < grid.GetLength(0); x++){
				if (x == 0 || z == 0 || x == grid.GetLength (0) - 1 || z == grid.GetLength (1) - 1 || grid [x, z] == 1) {
					GameObject instance = Instantiate (wall, new Vector3 (x, 0f, z), Quaternion.identity);
					instance.transform.SetParent (wall_parent.transform);
				}
			}
		}
	}

}
