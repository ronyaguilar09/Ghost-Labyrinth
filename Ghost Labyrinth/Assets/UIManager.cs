using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour {
	
	public GameObject CompleteUI;

	public HUDManager HUD;
	
	public void DisplayComplete()
	{
		CompleteUI.SetActive(true);
	}

	public void PrepareHUD(int level)
	{
		HUDManager.PrepareLevel(level);
	}

	public void HideMenu()
	{
		CompleteUI.SetActive(false);
	}
}
