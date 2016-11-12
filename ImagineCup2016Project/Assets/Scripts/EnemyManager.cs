using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class EnemyManager : MonoBehaviour {


	//Remaining 'health' of the enemy. (Pistols can take away 1, rifles,2.5, maybe boomerangs 5?)
	private float enemyHealth;

	//Bool to check if enemy is alive
	private bool isAlive;

	//Bool for if they have detected player yet or not
	private bool detectedPlayer;

	//Player reference
	public GameObject player;

	public float cooldown;

	// Use this for initialization
	void Start () {

		//Set starting health to 5 and isAlive to true
		enemyHealth = 5.0f;
		isAlive = true;
		detectedPlayer = false;

		cooldown = 5;
	
	}

	void FixedUpodate ()
	{

		//Raycast left
		Ray rayLeft = new Ray (transform.position, new Vector3(-5.0f,0.0f,0.0f));
		
		//Distance to check for player (temp for now till we figure out what we want)
		float rayDistance = 100.0f;
		
		//Hit Info
		RaycastHit hitInfo;
		
		// Set bool for if enemy detected player according to Raycast
		detectedPlayer = Physics.Raycast (rayLeft, out hitInfo, rayDistance);
		
		if (detectedPlayer) {
			
			Debug.Log("Test");
		}
	
	}
	// Update is called once per frame
	void Update () {

	
		if (detectedPlayer == true) {
		
		
		
		
		}
	}


}
