using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour {
	//public PlayerStats player_stats;

	public static int livesLeft = 3;
	public static int breadcrumbs = 2;
	public static int collectiblesFound = 0;
	public static int totalCollectibles = 2;
	public static int score = 0;
    
	public Text scoreText;
	public Text breadText;
	public Text collectText;
	public Image heart1;
	public Image heart2;
	public Image heart3;

   
	// Use this for initialization
	void Awake ()
	{
		livesLeft = 3;
		breadcrumbs = 2;
		score = 0;
		collectiblesFound = 0;
		totalCollectibles = 2;
		UpdateUI();
	}

	public void UpdateUI()
	{
		scoreText.text = "SCORE: " + score;
		breadText.text = breadcrumbs.ToString();
		collectText.text = collectiblesFound + " / " + totalCollectibles + " FOUND";
		UpdateLives();
	}

	public static void PrepareLevel(int level)
	{
		collectiblesFound = 0;
		totalCollectibles++;
		breadcrumbs = (int)level / 2;
		livesLeft = 3;
	}

	

	private void UpdateLives()
	{
		switch (livesLeft)
		{
			case 3:
				heart1.gameObject.SetActive(true);
				heart2.gameObject.SetActive(true);
				heart3.gameObject.SetActive(true);
				break;
			case 2:
				heart1.gameObject.SetActive(true);
				heart2.gameObject.SetActive(true);
				heart3.gameObject.SetActive(false);
				break;
			case 1:
				heart1.gameObject.SetActive(true);
				heart2.gameObject.SetActive(false);
				heart3.gameObject.SetActive(false);
				break;
			case 0:
				heart1.gameObject.SetActive(false);
				heart2.gameObject.SetActive(false);
				heart3.gameObject.SetActive(false);
				break;
			default:
				break;
		}
	}
}
