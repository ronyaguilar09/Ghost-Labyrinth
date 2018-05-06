using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Policy;
using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
	private BoxCollider wall_collider;

	private bool playerInRange;
	private bool isHiding = false;
	private Animator anim;
	private AudioSource door_audio;

	private void Awake()
	{
		anim = this.GetComponent<Animator>();
		wall_collider = this.transform.parent.GetComponent<BoxCollider>();
		door_audio = this.GetComponent<AudioSource>();
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag("Player"))
			playerInRange = true;
	}

	private void OnTriggerExit(Collider other)
	{
		if(other.gameObject.CompareTag("Player"))
			playerInRange = false;
	}

	// Update is called once per frame
	void Update () {
		if (playerInRange && Input.GetKeyDown(KeyCode.Space) && !door_audio.isPlaying)
		{
			isHiding = !isHiding;
			door_audio.Play();
			anim.SetTrigger("Hide");
			wall_collider.isTrigger = isHiding;
		}
	}
}
