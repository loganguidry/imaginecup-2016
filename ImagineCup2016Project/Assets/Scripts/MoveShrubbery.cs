using UnityEngine;
using System.Collections;

public class MoveShrubbery : MonoBehaviour
{
	public bool ignoreInstantiation = false;
	public bool randomSpeed = false;
	public float[] randomSpeeds = new float[2];
	public float moveSpeed;
	public float highestPosition;
	public float lowestPosition;
	public float lifetime = 10f;
	float spawnTime;

	void Start()
	{
		spawnTime = Time.time;
		float randomHeight = Random.Range(lowestPosition, highestPosition);

		// Set random speed
		if (randomSpeed)
			moveSpeed = Random.Range(randomSpeeds[0], randomSpeeds[1]);
	
		// Skip rest of Start()
		if (ignoreInstantiation)
		{
			transform.position = new Vector3(transform.position.x, randomHeight, transform.position.z);
			return;
		}

		// Create at right of screen
		transform.position = new Vector3((Camera.main.ScreenToWorldPoint(new Vector3(Camera.main.pixelWidth, 0, 0)) + new Vector3(Random.Range(1, 10), 0, 0)).x, randomHeight, 0);

		// Speed based on height for parallax
		if (!randomSpeed)
			moveSpeed = -randomHeight;

		// Sort
		GetComponent<SpriteRenderer>().sortingOrder = Mathf.FloorToInt(-randomHeight * 10 + 10);
	}

	void Update()
	{
		transform.Translate(new Vector3(-moveSpeed * Time.deltaTime, 0, 0));

		// Delete
		if (Time.time - spawnTime >= lifetime)
			Destroy(gameObject);
	}
}
