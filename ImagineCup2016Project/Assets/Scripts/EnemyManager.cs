using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class EnemyManager : MonoBehaviour
{
	public LayerMask layerMask;

	//Remaining 'health' of the enemy. (Pistols can take away 1, rifles,2.5, maybe boomerangs 5?)
	private float enemyHealth;

	//Bool to check if enemy is alive
	private bool isAlive;

	//Bool for if they have detected player yet or not
	private bool detectedPlayer;

	public float cooldown;

	Vector2 velocity = Vector2.zero;
	public float acceleration;

    private string currentDirection;

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

        cooldown = 0;

        currentDirection = "left";
    }

	void Update()
	{
		// Fire raycast
		RaycastHit2D hit = Physics2D.Raycast(transform.position, GameManager.Player.transform.position + new Vector3(0, 0.5f, 0) - transform.position, 5f, layerMask);

		// Checks if a raycast hit the player
		detectedPlayer = false;
        if (hit.transform != null && hit.transform.tag == "Player")
        {
            cooldown -= Time.deltaTime;

            if (cooldown <= 0.0f)
            {
                attackPlayer();
                cooldown = 1.0f;
            }
            
            detectedPlayer = true;
        }
        else
		{
            idleMovement();
        }
    }

    void attackPlayer()
    {
		if (GameManager.Player.transform.position.x < transform.position.x)
        {
            Instantiate(enemyBullet, new Vector3(transform.position.x  - 0.23f, transform.position.y, 0), Quaternion.Euler(new Vector3(180, 0, 180)));
        }
        else 
        {
			Instantiate(enemyBullet, new Vector3(transform.position.x + 0.23f, transform.position.y, 0), Quaternion.identity);
        }
    }
	void idleMovement ()
	{
        acceleration = 0.1f;
        velocity = GetComponent<Rigidbody2D>().velocity;

        //velocity += new Vector2(-acceleration, 0);

        if (walkingLeft == true)
        {
            transform.localScale = new Vector3(1, 1, 1);

            currentDirection = "left";
            velocity += new Vector2(-acceleration, 0);
            GetComponent<Rigidbody2D>().velocity = velocity;

            if (origX > transform.position.x + 2.5f)
            {
                currentDirection = "right";
                walkingLeft = false;
                walkingRight = true;
            }
        }

        if (walkingRight == true)
        {
            transform.localScale = new Vector3(-1, 1, 1);
            velocity += new Vector2(acceleration, 0);
            GetComponent<Rigidbody2D>().velocity = velocity;

            if (transform.position.x >= origX)
            {
                currentDirection = "left";
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
