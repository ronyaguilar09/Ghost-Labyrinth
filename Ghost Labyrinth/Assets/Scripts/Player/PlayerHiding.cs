using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHiding : MonoBehaviour
{
	private bool inRange;
	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag("Door"))
		{
			inRange = true;
		}
		else
		{
			inRange = false;
		}
	}

	private void Update()
	{
		if (inRange && Input.GetKeyDown(KeyCode.Space))
		{
			
		}
	}
}
