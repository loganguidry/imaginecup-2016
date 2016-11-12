using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
	static public GameObject Player;
	public float gravity;
	public float minX;
	public float spawnShrubberyDelay;
	float lastSpawnShrubbery;
	public GameObject[] shrubberies = new GameObject[0];

	static public float PlayerHealth = 100f;

	void Start ()
	{
		Player = GameObject.Find("Player");
		PlayerHealth = 100f;
		PlayerController.MinimumX = minX;
		Physics.gravity = new Vector3(0, -gravity, 0);

		// Player should pass through enemies
		Physics2D.IgnoreLayerCollision(8, 9, true);

		// Enemies should pass through enemies
		Physics2D.IgnoreLayerCollision(9, 9, true);
	}

	void Update()
	{
		// Spawn shrubbery
		if (Time.time - lastSpawnShrubbery >= spawnShrubberyDelay)
		{
			lastSpawnShrubbery = Time.time;

			// Choose a random shrubbery
			GameObject randomShrubbery = shrubberies[Random.Range(0, shrubberies.Length)];

			// Create the shrubbery
			GameObject clonedShrubbery = Instantiate(randomShrubbery, Vector3.zero, Quaternion.identity) as GameObject;
		}

		// Damage player (debugging)
		DamagePlayer(0.2f);
	}

	static public void DamagePlayer(float amount)
	{
		PlayerHealth -= amount;

		if (PlayerHealth <= 0)
			Die();
	}

	static public void Die()
	{
		print("PLAYER DIED [GAMEMANAGER.CS]");
	}
}
