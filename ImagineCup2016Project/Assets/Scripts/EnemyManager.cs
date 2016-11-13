using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class EnemyManager : MonoBehaviour
{
    public LayerMask layerMask;

    //Remaining 'health' of the enemy. (Pistols can take away 1, rifles,2.5, maybe boomerangs 5?)
    private float enemyHealth;

    //Bool to check if enemy is alive
    public bool isAlive;

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

    public AudioClip gunshotSound;
    public GameObject bangPowTextPopup;

    public float jumpPower = 15f;
    public float minJumpDelay = 3f;
    float lastJumpTime;

    public float yCloseness;

    // Use this for initialization
    void Start()
    {
        //Set starting health to 100 and isAlive to true
        enemyHealth = 100.0f;
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
		// Enemy is dead
		if (!isAlive)
			return;

        // Weapon cooldown
        cooldown -= Time.deltaTime;

        // Fire raycast
        RaycastHit2D hit = Physics2D.Raycast(transform.position, GameManager.Player.transform.position + new Vector3(0, 0.5f, 0) - transform.position, 5f, layerMask);

        // Check if player Y position is close enough
        bool closeY = Mathf.Abs(GameManager.Player.transform.position.y - transform.position.y) <= yCloseness;

        // Checks if a raycast hit the player
        detectedPlayer = false;
		if (hit.transform != null && hit.transform.tag == "Player" && closeY && !GameManager.PlayerDead)
        {
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

        // Randomly jump
        if (Random.Range(0, 100) == 0 && Time.time - lastJumpTime >= minJumpDelay)
        {
            lastJumpTime = Time.time;
            GetComponent<Rigidbody2D>().velocity += new Vector2(0, jumpPower);
            velocity += new Vector2(0, jumpPower);
		}

		// Keep in the level
		if (transform.position.x < PlayerController.MinimumX)
		{
			transform.position = new Vector3(PlayerController.MinimumX, transform.position.y, transform.position.z);
			velocity = new Vector2(0f, velocity.y);
		}
		if (transform.position.x > PlayerController.MaximumX)
		{
			transform.position = new Vector3(PlayerController.MaximumX, transform.position.y, transform.position.z);
			velocity = new Vector2(0f, velocity.y);
		}

        if (enemyHealth <= 0.0f)
        {
            onDeath();
        }
    }

    void attackPlayer()
    {
        // Create bullet
        if (GameManager.Player.transform.position.x < transform.position.x)
        {
            Instantiate(enemyBullet, new Vector3(transform.position.x + 0.23f, transform.position.y, 0), Quaternion.Euler(new Vector3(180, 0, 180)));
            currentDirection = "left";
        }
        else
        {
            Instantiate(enemyBullet, new Vector3(transform.position.x + 0.23f, transform.position.y, 0), Quaternion.identity);
            currentDirection = "right";
        }

        // Play gunshot sound
        Camera.main.transform.GetChild(0).GetComponent<AudioSource>().PlayOneShot(gunshotSound);

        // Shake camera
        CameraShake.Kick(0.025f);

        // Text popup
        //GameObject clonedPowText = Instantiate(bangPowTextPopup, Camera.main.WorldToScreenPoint(transform.position), Quaternion.identity) as GameObject;
        //clonedPowText.transform.SetParent(GameManager.UserInterface);
    }

    void idleMovement()
    {
        // Get velocity
        velocity = GetComponent<Rigidbody2D>().velocity;

        acceleration = 0.1f;

        // Walk back and forth
        if (walkingLeft)
        {
            transform.localScale = new Vector3(1, 1, 1);

            currentDirection = "left";
            velocity += new Vector2(-acceleration, 0);

            if (origX > transform.position.x + 2f)
            {
                currentDirection = "right";
                walkingLeft = false;
                walkingRight = true;
            }
        }
        else
        {
            transform.localScale = new Vector3(-1, 1, 1);
            velocity += new Vector2(acceleration, 0);

            if (transform.position.x >= origX)
            {
                currentDirection = "left";
                walkingRight = false;
                walkingLeft = true;
            }
        }

        // Set velocity
        GetComponent<Rigidbody2D>().velocity = velocity;
    }

    public void TakeDamage(float damage)
    {
        enemyHealth -= damage;
    }

    void onDeath()
    {
		isAlive = false;
		GetComponentInChildren<SpriteRenderer>().enabled = false;
		GetComponent<BoxCollider2D>().enabled = false;
        //Destroy(gameObject);
    }

    void OnDrawGizmos()
    {
        if (GameManager.Player == null)
            return;

        if (detectedPlayer)
            Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, GameManager.Player.transform.position + new Vector3(0, 0.5f, 0));
    }
}