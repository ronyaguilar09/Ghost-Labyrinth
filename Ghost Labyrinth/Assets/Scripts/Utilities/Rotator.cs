using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
	public Vector3 direction;	
	
	// Update is called once per frame
	void Update () {
		transform.Rotate(direction*Time.deltaTime);	
	}
}
