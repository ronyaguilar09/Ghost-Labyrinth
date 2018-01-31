using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	public float moveSpeed = 5f;
	public float turnSpeed = 0.5f;

	private float moveAxisValue;
	private float turnAxisValue;
	private Rigidbody rbody;
	// Use this for initialization
	private void Start () {
		rbody = this.GetComponent<Rigidbody> ();
	}
	
	// Update is called once per frame
	private void Update () {
		moveAxisValue = Input.GetAxisRaw("Vertical");
		turnAxisValue = Input.GetAxisRaw("Horizontal");
	}

	private void FixedUpdate(){
		Move ();
		Turn ();
	}

	private void Move(){
		Vector3 direction = rbody.transform.forward * moveAxisValue * moveSpeed * Time.deltaTime;
		rbody.MovePosition (this.transform.position + direction);
	}

	private void Turn(){
		float turn = turnAxisValue * turnSpeed * Time.deltaTime;
		Quaternion newRotation = Quaternion.Euler (0f, turn, 0f);
		rbody.MoveRotation (rbody.transform.rotation * newRotation);
	}
}
