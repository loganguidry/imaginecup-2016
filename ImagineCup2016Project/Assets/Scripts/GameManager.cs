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

	void Start ()
	{
		Player = GameObject.Find("Player");
		PlayerController.MinimumX = minX;
		Physics.gravity = new Vector3(0, -gravity, 0);
	}

	void Update()
	{
		if (Time.time - lastSpawnShrubbery >= spawnShrubberyDelay)
		{
			lastSpawnShrubbery = Time.time;

			// Choose a random shrubbery
			GameObject randomShrubbery = shrubberies[Random.Range(0, shrubberies.Length)];

			// Create the shrubbery
			GameObject clonedShrubbery = Instantiate(randomShrubbery, Vector3.zero, Quaternion.identity) as GameObject;
		}
	}
}
