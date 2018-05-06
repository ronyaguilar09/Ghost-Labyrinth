using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StateController : MonoBehaviour
{

	public State currentState;
	public State remainState;
	public EnemyStats enemyStats;
	public Transform eyes;
	public Transform target;
	public float waitTime;
	public AudioClip attackClip;
	public AudioClip chaseClip;

	[HideInInspector] public MazeGenerator maze;
	[HideInInspector] public NavMeshAgent agent;
	[HideInInspector] public List<Vector3> spaces;
	[HideInInspector] public float currentTimer;
	[HideInInspector] public Animator anim;
	[HideInInspector] public AudioSource audio;
	[HideInInspector] public bool justAttacked;

	// Use this for initialization
	void Awake ()
	{
		agent = GetComponent<NavMeshAgent>();
		anim = this.transform.Find("Avatar").gameObject.GetComponent<Animator>();
		maze = GameObject.Find("MazeHolder").GetComponent<MazeGenerator>();
		spaces = maze.GetAvailableSpaces();
		eyes = transform.Find("Eyes");
		target = GameObject.FindGameObjectWithTag("Player").transform;
		enemyStats = GameObject.Find("EnemyManager").GetComponent<EnemyManager>().stats;
		audio = this.GetComponent<AudioSource>();
		justAttacked = false;
	}
	
	// Update is called once per frame
	void Update () {
		currentState.UpdateState(this);
	}

	public void TransitionToState(State nextState)
	{
		if (nextState != remainState)
		{
			currentState = nextState;
		}
	}
}
