using System.Collections;
using System.Collections.Generic;
using System.Web;
using UnityEngine;

public class UIManager : MonoBehaviour {
	
	public GameObject CompleteUI;
	public GameObject GameOverUI;
	public HUDManager HUD;

	public void DisplayComplete()
	{
		CompleteUI.SetActive(true);
	}

	public void DisplayGameOver()
	{
		GameOverUI.SetActive(true);
		this.GetComponent<AudioSource>().Play();
	}

	public void PrepareHUD(int level)
	{
		HUDManager.PrepareLevel(level);
		HUD.UpdateUI();
	}

	public void HideMenu()
	{
		CompleteUI.SetActive(false);
		GameOverUI.SetActive(false);
	}
}
