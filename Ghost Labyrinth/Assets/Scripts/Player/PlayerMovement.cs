using System.Collections;
using System.Collections.Generic;
using System.Security;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	public float moveSpeed = 5f;
	public float turnSpeed = 0.5f;
	public float audio_pitch = 1.5f;
	Animator anim;

	private float moveAxisValue;
	private float turnAxisValue;
	private Rigidbody rbody;

	private AudioSource footstepAudio;
	
	// Use this for initialization
	private void Start () {
		rbody = this.GetComponent<Rigidbody> ();
		anim = this.transform.Find("Avatar").gameObject.GetComponent<Animator>();
		footstepAudio = this.GetComponent<AudioSource>();
		//anim = this.GetComponent<Animator>();
	}
	
	private void FixedUpdate(){
		moveAxisValue = Input.GetAxisRaw("Vertical");
		turnAxisValue = Input.GetAxisRaw("Horizontal");
		Move ();
		Turn ();
		Animate(moveAxisValue, turnAxisValue);
		
		if (moveAxisValue != 0 && !footstepAudio.isPlaying)
		{
			footstepAudio.pitch = Random.Range(1.2f, 1.5f);
			footstepAudio.Play();
		}
	}

	private void Move(){
		Vector3 direction = rbody.transform.forward * moveAxisValue * moveSpeed * Time.deltaTime;
		rbody.MovePosition (this.transform.position + direction);
	}

	private void Animate(float m, float t)
	{
			anim.SetBool("RunningForward", m > 0);
			anim.SetBool("RunningBackward", m < 0);
			anim.SetBool("TurningLeft", (t < 0 && m == 0));
			anim.SetBool("TurningRight", (t > 0 && m == 0));
	}

	private void Turn(){
		float turn = turnAxisValue * turnSpeed * Time.deltaTime;
		Quaternion newRotation = Quaternion.Euler (0f, turn, 0f);
		rbody.MoveRotation (rbody.transform.rotation * newRotation);
	}
}
