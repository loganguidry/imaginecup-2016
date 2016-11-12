using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class EnemyManager : MonoBehaviour
{
	//Remaining 'health' of the enemy. (Pistols can take away 1, rifles,2.5, maybe boomerangs 5?)
	private float enemyHealth;

	//Bool to check if enemy is alive
	private bool isAlive;

	//Bool for if they have detected player yet or not
	private bool detectedPlayer;

	public float cooldown;

	// Use this for initialization
	void Start ()
	{
		//Set starting health to 5 and isAlive to true
		enemyHealth = 5.0f;
		isAlive = true;
		detectedPlayer = false;

		cooldown = 5;
	}

	/*
	void FixedUpdate ()
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
	*/

	void Update()
	{
		// Only detect player [DON'T USE]
		//int layerMask = 1 << 8;

		// Fire raycast
		RaycastHit2D hit = Physics2D.Raycast(transform.position, GameManager.Player.transform.position + new Vector3(0, 0.5f, 0) - transform.position, 10f);//, layerMask);

		// Checks if a raycast hit the player
		detectedPlayer = false;
		if (hit.transform != null && hit.transform.tag == "Player")
			detectedPlayer = true;

		// The player has been detected
		if (detectedPlayer)
		{
			print("detected player");
		}
	}

	void OnDrawGizmos()
	{
		if (detectedPlayer)
			Gizmos.color = Color.red;
		Gizmos.DrawLine(transform.position, GameManager.Player.transform.position + new Vector3(0, 0.5f, 0));
	}
}
