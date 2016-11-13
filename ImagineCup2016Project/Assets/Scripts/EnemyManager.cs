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

	Vector2 velocity = Vector2.zero;
	public float acceleration;

	private string currentDirection = "right";

    private bool walkingLeft;
    private bool walkingRight;

    public GameObject enemyBullet;

    private float origX;
    private float origY;

    // Use this for initialization
    void Start ()
	{
		//Set starting health to 5 and isAlive to true
		enemyHealth = 5.0f;
		isAlive = true;
		detectedPlayer = false;

        origX = transform.position.x;
        origY = transform.position.y;

        walkingLeft = true;
        walkingRight = false;

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
		// Don't detect enemies
		int layerMask = 1 << 9;
		layerMask = ~layerMask;


		// Fire raycast
		RaycastHit2D hit = Physics2D.Raycast(transform.position, GameManager.Player.transform.position + new Vector3(0, 0.5f, 0) - transform.position, 10f, layerMask);

		// Checks if a raycast hit the player
		detectedPlayer = false;
        if (hit.transform != null && hit.transform.tag == "Player")
        {

            Debug.Log("Freedom");
            detectedPlayer = true;
        }
        else {

            idleMovement();
            Debug.Log("Fresnos");
        }

        if (detectedPlayer == true)
        {
           // Instantiate(enemyBullet)
        }
		// The player has been detected

	}

	void idleMovement ()
	{

        acceleration = 0.1f;
        velocity = GetComponent<Rigidbody2D>().velocity;

        //velocity += new Vector2(-acceleration, 0);





        if (walkingLeft == true)
        {




            velocity += new Vector2(-acceleration, 0);
            GetComponent<Rigidbody2D>().velocity = velocity;

            if (origX > transform.position.x + 2.5f)
            {
                walkingLeft = false;
                walkingRight = true;
            }

        }

        if (walkingRight == true)
        {

            velocity += new Vector2(acceleration, 0);
            GetComponent<Rigidbody2D>().velocity = velocity;


            if (transform.position.x >= origX)
            {
                walkingRight = false;
                walkingLeft = true;

            }



        }


    }

	void OnDrawGizmos()
	{
		if (detectedPlayer)
			Gizmos.color = Color.red;
		Gizmos.DrawLine(transform.position, GameManager.Player.transform.position + new Vector3(0, 0.5f, 0));
	}
}
