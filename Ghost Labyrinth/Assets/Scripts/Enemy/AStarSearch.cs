using System;
using System.Collections.Generic;
using UnityEngine;

public class AStarSearch {
	class Cell : IComparable {
		public Vector3 position;
		public Cell parent;
		public int f;
		int g;
		int h;

		public Cell(Vector3 pos, Vector3 target){
			position = pos;
			g = 0;
			h = 0;
			f = 0; 
			h = ManhattanDistance(position, target);
		}

		public Cell(Vector3 pos, Vector3 target, Cell parent) {
			this.parent = parent;
			position = pos;
			g = 1 + parent.g;
			h = 0;
			f = 0; 
			h = ManhattanDistance(position, target);
			f = g + h;
		}

		// Hueristic value for h
		private int ManhattanDistance(Vector3 start, Vector3 end) {
			int xDist = (int) Mathf.Abs(end.x - start.x);
			int zDist = (int) Mathf.Abs(end.z - start.z);
			return xDist + zDist;
		}

		public int CompareTo(object obj) {
			if(obj == null) return 1;

			Cell otherCell = obj as Cell;
			if(otherCell != null){
				return this.f.CompareTo(otherCell.f);
			} else {
				throw new ArgumentException("Object is not a Cell");
			}
		}
	}

	private SortedList<Cell, int> frontier;
	private SortedList<Cell, int> visited;
	private Cell source;
	private Vector3 destination;
	private int[,] grid;
	private List<Cell> neighbors;
	private bool foundDestination;

	public AStarSearch (Vector3 start, Vector3 end, int[,] map)
	{
		source = new Cell (start, end);
		destination = end;
		foundDestination = false;
		grid = map;
		frontier = new SortedList<Cell, int> ();
		visited = new SortedList<Cell, int> ();
		frontier.Add (source, source.f);

		if (!isValid (source)) {
			Debug.Log ("Source is invalid");
			return;
		}
		if (!isValid (new Cell (destination, destination))) {
			Debug.Log ("Destination is invalid");
			return;
		}
		if (isDestination (source)) {
			Debug.Log ("We're already at destination");
			return;
		}
	}

	bool isValid(Cell cell) {
		int x = (int)cell.position.x;
		int z = (int)cell.position.z;
		if( x <= 0 || z <= 0 || x >= grid.GetLength(0) - 1 || z >= grid.GetLength(1) - 1 || grid[x,z] == 1)
			return false;

		return true;
	}

	bool isDestination(Cell cell){
		if(cell.position.x == destination.x && cell.position.z == destination.z)
			return true;
		return false;
	}

	List<Cell> createNeighbors(Cell current){
		List<Cell> neighbors = new List<Cell>();

		// Above Neighbor
		Cell newCell = new Cell(new Vector3(current.position.x, 0f, current.position.z - 1), destination, current);
		if(isValid(newCell))
			neighbors.Add(newCell);

		// Right Neighbor
		newCell = new Cell(new Vector3(current.position.x + 1, 0f, current.position.z), destination, current);
		if(isValid(newCell))
			neighbors.Add(newCell);

		// Bottom Neighbor
		newCell = new Cell(new Vector3(current.position.x, 0f, current.position.z + 1), destination, current);
		if(isValid(newCell))
			neighbors.Add(newCell);

		// Left Neighbor
		newCell = new Cell(new Vector3(current.position.x - 1, 0f, current.position.z), destination, current);
		if(isValid(newCell))
			neighbors.Add(newCell);

		return neighbors;
	}
/*
	public List<Vector3> findAStarPath(){
		while(frontier.Count > 0 || foundDestination == false){
			// Cell current = frontier.; FIND WAY TO GET SMALLEST ELEMENT!!!!!
			frontier.RemoveAt(0);
			neighbors = createNeighbors(current);
			foreach(Cell cell in neighbors){
				if(isDestination(cell)){
					foundDestination = true;
					return tracePath(cell, destination);
				}
				if(frontier.ContainsKey(cell) && frontier[cell] < cell.f)
					continue;
			}
		}
	}
*/
	List<Vector3> tracePath(Cell child, Vector3 destination){
		return new List<Vector3>();
	}

}
