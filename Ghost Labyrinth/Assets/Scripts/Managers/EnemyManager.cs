using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour {
	private List<GameObject> enemies;
	private int enemyCount;
	public EnemyStats stats;

	private void Awake(){
		enemies = new List<GameObject> ();
		stats = new EnemyStats();
	}

	public void AddEnemy(GameObject enemy){
		enemies.Add (enemy);
	}

	public void setCount(int count){
		enemyCount = count;
	}

	public int getCount(){
		return enemyCount;
	}

	public void clearEnemies(){
		for(int i = 0; i < enemies.Count; i++){
			Destroy (enemies [i]);
		}
	}

}
