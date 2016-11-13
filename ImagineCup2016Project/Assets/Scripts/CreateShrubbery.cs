using UnityEngine;
using System.Collections;

public class CreateShrubbery : MonoBehaviour
{
	public float spawnShrubberyDelay;
	float lastSpawnShrubbery;
	public GameObject[] shrubberies = new GameObject[0];
	public float speedMultiplier;

	void Update()
	{
		// Spawn shrubbery
		if (Time.time - lastSpawnShrubbery >= spawnShrubberyDelay)
		{
			lastSpawnShrubbery = Time.time;

			// Choose a random shrubbery
			GameObject randomShrubbery = shrubberies[Random.Range(0, shrubberies.Length)];
			if (randomShrubbery.name.StartsWith("Cloud") && Random.Range(0, 10) > 2)
				return;

			// Create the shrubbery
			GameObject clonedShrubbery = Instantiate(randomShrubbery, Vector3.zero, Quaternion.identity) as GameObject;

			// Set speed multiplier
			clonedShrubbery.GetComponent<MoveShrubbery>().moveSpeed *= speedMultiplier;
		}
	}
}
